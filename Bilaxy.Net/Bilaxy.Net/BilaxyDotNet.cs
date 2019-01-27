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
        /// <summary>
        /// Constructor for unsigned endpoints
        /// </summary>
        public BilaxyDotNet() : base()
        {
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
        }

        #region Public Methods


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
        /// <param name="pairIds">Trading pairs to query</param>
        /// <returns>Collection of Ticker objects</returns>
        public async Task<List<Ticker>> GetTickers(int[] pairIds)
        {
            var endpoint = $@"/v1/tickers";
            if (pairIds != null && pairIds.Length > 0)
            {
                var ids = string.Join(",", pairIds);
                endpoint += $@"?symbol={ids}";
            }

            var response = await base.GetRequest<List<Ticker>>(endpoint);

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
            var parms = new Dictionary<string, object>();
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
            var parms = new Dictionary<string, object>();
            parms.Add("symbol", pairId);
            if (fromDate > 0)
                parms.Add("since", fromDate);
            parms.Add("type", (int)orderType);

            var response = await base.GetRequest<List<Order>>(endpoint, parms, true);

            return response;
        }

        #endregion Private Methods
    }
}