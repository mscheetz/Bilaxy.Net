// -----------------------------------------------------------------------------
// <copyright file="IBilaxyDotNetExtension" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/27/2019 2:44:10 PM" />
// -----------------------------------------------------------------------------

namespace Bilaxy.Net
{
    #region Usings

    using Bilaxy.Net.Contracts;
    using Bilaxy.Net.Core;
    using DateTimeHelpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    #endregion Usings

    public static class IBilaxyDotNetExtension
    {
        private static readonly DateTimeHelper dtHelper = new DateTimeHelper();

        #region Public Api

        /// <summary>
        /// Get ticker for a trading pair
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <returns>Ticker object</returns>
        public static async Task<Ticker> GetTicker(this IBilaxyDotNet service, string pair)
        {
            var pairId = BilaxyHelper.GetId(pair);

            return await service.GetTicker(pairId);
        }

        /// <summary>
        /// Get tickers for all trading pairs
        /// </summary>
        /// <returns>Collection of Ticker objects</returns>
        public static async Task<List<Ticker>> GetTickers(this IBilaxyDotNet service)
        {
            return await service.GetTickers(null);
        }

        /// <summary>
        /// Get tickers for all trading pairs
        /// </summary>
        /// <param name="pairs">Trading pairs to query</param>
        /// <returns>Collection of Ticker objects</returns>
        public static async Task<List<Ticker>> GetTickers(this IBilaxyDotNet service, string[] pairs)
        {
            var pairIds = new List<int>();
            foreach (var pair in pairs)
            {
                pairIds.Add(BilaxyHelper.GetId(pair));
            }
            
            return await service.GetTickers(pairIds.ToArray());
        }

        /// <summary>
        /// Get market depth
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <param name="figures">Significant figures</param>
        /// <returns>Depth for trading pair</returns>
        public static async Task<Depth> GetDepth(this IBilaxyDotNet service, string pair, Figures figures = Figures.Default)
        {
            var pairId = BilaxyHelper.GetId(pair);

            return await service.GetDepth(pairId, figures);
        }

        /// <summary>
        /// Get recent orders
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <param name="records">Number of records to return</param>
        /// <returns>Collection of Order objects</returns>
        public static async Task<List<Order>> GetPairOrders(this IBilaxyDotNet service, string pair)
        {
            return await GetPairOrders(service, pair, 100);
        }

        /// <summary>
        /// Get recent orders
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <param name="records">Number of records to return</param>
        /// <returns>Collection of Order objects</returns>
        public static async Task<List<Order>> GetPairOrders(this IBilaxyDotNet service, string pair, int records)
        {
            var pairId = BilaxyHelper.GetId(pair);

            return await service.GetPairOrders(pairId, records);
        }

        /// <summary>
        /// Get recent orders
        /// </summary>
        /// <param name="pairId">Trading pair</param>
        /// <param name="records">Number of records to return</param>
        /// <returns>Collection of Order objects</returns>
        public static async Task<List<Order>> GetPairOrders(this IBilaxyDotNet service, int pairId)
        {
            return await service.GetPairOrders(pairId, 100);
        }

        #endregion Public Api

        #region Secure Api

        /// <summary>
        /// Get a deposit address for a currency
        /// </summary>
        /// <param name="symbol">Currency symbol</param>
        /// <returns>String of deposit address</returns>
        public static async Task<string> GetDepositAddress(this IBilaxyDotNet service, string symbol)
        {
            var symbolId = BilaxyHelper.GetId(symbol);

            return await service.GetDepositAddress(symbolId);
        }

        /// <summary>
        /// Get open orders
        /// </summary>
        /// <param name="pairId">Trading pair id</param>
        /// <returns>Collection of Order objects</returns>
        public static async Task<List<Order>> GetOrders(this IBilaxyDotNet service, int pairId)
        {
            return await service.GetOrders(pairId, 0, OrderType.AllOrders);
        }

        /// <summary>
        /// Get open orders
        /// </summary>
        /// <param name="pairId">Trading pair id</param>
        /// <param name="fromDate">From date</param>
        /// <returns>Collection of Order objects</returns>
        public static async Task<List<Order>> GetOrders(this IBilaxyDotNet service, int pairId, long fromDate)
        {
            return await service.GetOrders(pairId, fromDate, OrderType.AllOrders);
        }

        /// <summary>
        /// Get open orders
        /// </summary>
        /// <param name="pairId">Trading pair id</param>
        /// <param name="fromDate">From date</param>
        /// <returns>Collection of Order objects</returns>
        public static async Task<List<Order>> GetOrders(this IBilaxyDotNet service, int pairId, DateTime fromDate)
        {
            var unixTime = dtHelper.LocalToUnixTime(fromDate);

            return await service.GetOrders(pairId, unixTime, OrderType.AllOrders);
        }

        /// <summary>
        /// Get open orders
        /// </summary>
        /// <param name="pairId">Trading pair id</param>
        /// <returns>Collection of Order objects</returns>
        public static async Task<List<Order>> GetOpenOrders(this IBilaxyDotNet service, int pairId)
        {

            return await service.GetOrders(pairId, 0, OrderType.Pending);
        }

        /// <summary>
        /// Get open orders
        /// </summary>
        /// <param name="pairId">Trading pair id</param>
        /// <param name="fromDate">From date</param>
        /// <returns>Collection of Order objects</returns>
        public static async Task<List<Order>> GetOpenOrders(this IBilaxyDotNet service, int pairId, DateTime fromDate)
        {
            var unixTime = dtHelper.LocalToUnixTime(fromDate);

            return await service.GetOrders(pairId, unixTime, OrderType.Pending);
        }

        /// <summary>
        /// Get open orders
        /// </summary>
        /// <param name="pairId">Trading pair id</param>
        /// <param name="fromDate">From date</param>
        /// <returns>Collection of Order objects</returns>
        public static async Task<List<Order>> GetOpenOrders(this IBilaxyDotNet service, int pairId, long fromDate)
        {
            return await service.GetOrders(pairId, fromDate, OrderType.Pending);
        }

        /// <summary>
        /// Get open orders
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <returns>Collection of Order objects</returns>
        public static async Task<List<Order>> GetOpenOrders(this IBilaxyDotNet service, string pair)
        {
            var pairId = BilaxyHelper.GetId(pair);

            return await service.GetOrders(pairId, 0, OrderType.Pending);
        }

        /// <summary>
        /// Get open orders
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <param name="fromDate">From date</param>
        /// <returns>Collection of Order objects</returns>
        public static async Task<List<Order>> GetOpenOrders(this IBilaxyDotNet service, string pair, DateTime fromDate)
        {
            var pairId = BilaxyHelper.GetId(pair);
            var unixTime = dtHelper.LocalToUnixTime(fromDate);

            return await service.GetOrders(pairId, unixTime, OrderType.Pending);
        }

        /// <summary>
        /// Get open orders
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <param name="fromDate">From date</param>
        /// <returns>Collection of Order objects</returns>
        public static async Task<List<Order>> GetOpenOrders(this IBilaxyDotNet service, string pair, long fromDate)
        {
            var pairId = BilaxyHelper.GetId(pair);

            return await service.GetOrders(pairId, fromDate, OrderType.Pending);
        }

        #endregion Secure Api
    }
}