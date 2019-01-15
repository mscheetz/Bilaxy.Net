using Bilaxy.Net.Contracts;
using Bilaxy.Net.Interfaces;
using RESTApiAccess;
using RESTApiAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilaxy.Net.Repository
{
    public class BilaxyRepository : IBilaxyRepository
    {
        private readonly IRESTRepository _repo;
        private string _apiKey;
        private string _apiSecret;
        private string _urlBase = "https://api.bilaxy.com";

        public BilaxyRepository(string apiKey, string apiSecret)
        {
            _repo = new RESTRepository();
            _apiKey = apiKey;
            _apiSecret = apiSecret;
        }

        #region Public Methods

        public async Task<Ticker> GetTicker(string pair)
        {
            return await GetTicker(GetId(pair));
        }

        public async Task<Ticker> GetTicker(int pairId)
        {
            var endpoint = @"/v1/ticker";
            var url = $"{_urlBase}{endpoint}?symbol={pairId}";

            var response = await _repo.GetApiStream<BilaxyResponse<Ticker>>(url);
            var ticker = response.Data;

            var asset = GetAsset(pairId);
            ticker.Pair = asset.DashedPair;
            ticker.PairId = asset.AssetId;

            return ticker;
        }

        public async Task<List<Ticker>> GetTickers(string pair)
        {
            return await GetTickers(GetId(pair));
        }

        public async Task<List<Ticker>> GetTickers(int pairId)
        {
            var endpoint = @"/v1/tickers";
            var url = $"{_urlBase}{endpoint}?symbol={pairId}";

            var response = await _repo.GetApiStream<BilaxyResponse<List<Ticker>>>(url);
            var tickers = response.Data;

            var asset = GetAsset(pairId);

            tickers.All(t => { t.Pair = asset.Pair; return true; });

            return tickers;
        }

        public async Task<Depth> GetDepth(string pair, Figures figures = Figures.Default)
        {
            return await GetDepth(GetId(pair), figures);
        }

        public async Task<Depth> GetDepth(int pairId, Figures figures = Figures.Default)
        {
            var endpoint = @"/v1/depth";
            var url = $"{_urlBase}{endpoint}?symbol={pairId}&merge={(int)figures}";

            var response = await _repo.GetApiStream<BilaxyResponse<Depth>>(url);

            return response.Data;
        }

        public async Task<List<Order>> GetOrders(string pair, int size = 100)
        {
            return await GetOrders(GetId(pair), size);
        }

        public async Task<List<Order>> GetOrders(int pairId, int size = 100)
        {
            var endpoint = @"/v1/orders";
            var url = $"{_urlBase}{endpoint}?symbol={pairId}&size={size}";

            var response = await _repo.GetApiStream<BilaxyResponse<List<Order>>>(url);

            return response.Data;
        }

        #endregion Public Methods


        private Asset GetAsset(string pair)
        {
            return Assets.Get()
                        .Where(a => a.Pair.Equals(pair) || a.DashedPair.Equals(pair))
                        .FirstOrDefault();
        }

        private Asset GetAsset(int pairId)
        {
            return Assets.Get()
                        .Where(a => a.AssetId == pairId)
                        .FirstOrDefault();
        }

        private int GetId(string pair)
        {
            var pairId = Assets.Get()
                                .Where(a => a.Pair.Equals(pair) || a.DashedPair.Equals(pair))
                                .Select(a => a.AssetId).FirstOrDefault();
            return pairId;
        }
    }
}
