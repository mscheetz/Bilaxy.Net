// -----------------------------------------------------------------------------
// <copyright file="BilaxyDotNet" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/27/2019 2:43:46 PM" />
// -----------------------------------------------------------------------------

namespace Bilaxy.Net
{
    #region Usings

    using Bilaxy.Net.Contracts;
    using Bilaxy.Net.Core;
    using Bilaxy.Net.Repository;
    using FileRepository;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion Usings

    public class BilaxyDotNet : RepositoryBase, IBilaxyDotNet
    {
        private List<Asset> assets;

        /// <summary>
        /// Constructor for unsigned endpoints
        /// </summary>
        public BilaxyDotNet() : base()
        {
            LoadRepository();
        }

        /// <summary>
        /// Constructor for signed endpoints
        /// </summary>
        /// <param name="apiKey">Api Key</param>
        /// <param name="apiSecret">Api Secret</param>
        public BilaxyDotNet(string apiKey, string apiSecret) 
            : this(new ApiCredentials { ApiKey = apiKey, ApiSecret = apiSecret })
        {
        }

        /// <summary>
        /// Constructor for signed endpoints
        /// </summary>
        /// <param name="configPath">Path to file with api credentials</param>
        public BilaxyDotNet(string configPath) : base()
        {
            IFileRepository _fileRepo = new FileRepository();

            if (_fileRepo.FileExists(configPath))
            {
                var apiCredentials = _fileRepo.GetDataFromFile<ApiCredentials>(configPath);
                base.SetApiKey(apiCredentials);
                LoadRepository();
            }
            else
            {
                throw new Exception("Config file not found");
            }
        }

        /// <summary>
        /// Constructor for signed endpoints
        /// </summary>
        /// <param name="apiCredentials">Api Key information</param>
        public BilaxyDotNet(ApiCredentials apiCredentials) : base(apiCredentials)
        {
            LoadRepository();
        }

        private void LoadRepository()
        {
            assets = GetTradingPairs().Result;
        }

        #region Public Methods

        /// <summary>
        /// Get all currencies on the exchange
        /// </summary>
        /// <returns>Collection of Coin objects</returns>
        public async Task<List<Coin>> GetCurrencies()
        {
            var endpoint = @"/v1/coins/";

            var response = await base.GetRequest<List<Coin>>(endpoint);

            return response;
        }

        /// <summary>
        /// Get all trading pairs
        /// </summary>
        /// <param name="service"></param>
        /// <returns>Collection of trading pairs</returns>
        public async Task<List<Asset>> GetTradingPairs()
        {
            if (this.assets == null || this.assets.Count == 0)
            {
                var coins = await GetCurrencies();

                var pairs = coins.Select(c => new Asset(c.SymbolId, $@"{c.Symbol}/{c.Group}")).ToList();

                this.assets = pairs;
            }

            return this.assets;
        }

        /// <summary>
        /// Get ticker for a trading pair
        /// </summary>
        /// <param name="pairId">Id of trading pair</param>
        /// <returns>Ticker object</returns>
        public async Task<Ticker> GetTicker(int pairId)
        {
            var endpoint = $@"/v1/ticker?symbol={pairId}";

            var response = await base.GetRequest<Ticker>(endpoint);
            
            var asset = BilaxyHelper.GetAsset(pairId);
            response.Pair = asset.DashedPair;
            response.PairId = asset.AssetId;

            return response;
        }

        /// <summary>
        /// Get tickers for all trading pairs
        /// </summary>
        /// <returns>Collection of Ticker objects</returns>
        public async Task<List<Ticker>> GetTickers()
        {
            var endpoint = $@"/v1/tickers";

            var response = await base.GetRequest<List<Ticker>>(endpoint);

            foreach(var item in response)
            {
                var asset = BilaxyHelper.GetAsset(item.PairId);
                if(asset != null)
                    item.Pair = asset.DashedPair;
            }

            return response;
        }

        /// <summary>
        /// Get market depth
        /// </summary>
        /// <param name="pairId">Trading pair id</param>
        /// <param name="figures">Significant figures</param>
        /// <returns>Depth for trading pair</returns>
        public async Task<Depth> GetDepth(int pairId, Figures figures = Figures.Default)
        {
            var endpoint = $@"/v1/depth?symbol={pairId}&merge={(int)figures}";

            var response = await base.GetRequest<Depth>(endpoint);

            return response;
        }

        /// <summary>
        /// Get recent orders for a trading pair
        /// </summary>
        /// <param name="pairId">Trading pair id</param>
        /// <param name="records">Number of records to return</param>
        /// <returns>Collection of Order objects</returns>
        public async Task<List<Order>> GetPairOrders(int pairId, int records)
        {
            var endpoint = $@"/v1/orders?symbol={pairId}&size={records}";

            var response = await base.GetRequest<List<Order>>(endpoint);

            return response;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Get account balances
        /// </summary>
        /// <returns>Collection of Balance objects</returns>
        public async Task<List<Balance>> GetBalances()
        {
            var endpoint = @"/v1/balances";

            var response = await base.GetRequest<List<Balance>>(endpoint, true);

            return response;
        }

        /// <summary>
        /// Get a deposit address for a currency
        /// </summary>
        /// <param name="assetId">Currency id</param>
        /// <returns>String of deposit address</returns>
        public async Task<string> GetDepositAddress(int assetId)
        {
            var endpoint = @"/v1/coin_address";
            var parms = new SortedDictionary<string, object>();
            parms.Add("symbol", assetId);

            var response = await base.GetRequest<string>(endpoint, parms, true);

            return response;
        }

        /// <summary>
        /// Get account orders
        /// </summary>
        /// <param name="pairId">Trading pair id</param>
        /// <param name="fromDate">From date</param>
        /// <param name="orderType">Order type</param>
        /// <returns>Collection of Order objects</returns>
        public async Task<List<Order>> GetOrders(int pairId, long fromDate, OrderType orderType)
        {
            var endpoint = @"/v1/trade_list";
            var parms = new SortedDictionary<string, object>();
            parms.Add("symbol", pairId);
            if (fromDate > 0)
                parms.Add("since", fromDate);
            parms.Add("type", (int)orderType);

            var response = await base.GetRequest<List<Order>>(endpoint, parms, true);

            return response;
        }        

        /// <summary>
        /// Get an order
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <returns>Order object</returns>
        public async Task<List<Order>> GetOrder(int orderId)
        {
            var endpoint = @"/v1/trade_view";
            var parms = new SortedDictionary<string, object>();
            parms.Add("id", orderId);

            var response = await base.GetRequest<List<Order>>(endpoint, parms, true);

            return response;
        }

        /// <summary>
        /// Cancel an order
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <returns>Canceled order id</returns>
        public async Task<string> CancelOrder(int orderId)
        {
            var endpoint = @"/v1/cancel_trade";
            var parms = new SortedDictionary<string, object>();
            parms.Add("id", orderId);

            var response = await base.PostRequest<string>(endpoint, parms);

            return response;
        }

        /// <summary>
        /// Place an order
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <returns>Placed order id</returns>
        public async Task<string> PlaceOrder(int pairId, decimal quantity, decimal price, Side side)
        {
            var endpoint = @"/v1/trade";
            var parms = new SortedDictionary<string, object>();
            parms.Add("symbol", pairId);
            parms.Add("amount", quantity);
            parms.Add("price", price);
            parms.Add("type", side.ToString().ToLower());

            var response = await base.PostRequest<string>(endpoint, parms);

            return response;
        }

        #endregion Private Methods
    }
}