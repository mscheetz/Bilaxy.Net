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
        /// Get a trading pair
        /// </summary>
        /// <param name="pair">Pair to find</param>
        /// <returns>Asset of pair</returns>
        public static async Task<Asset> GetTradingPair(this IBilaxyDotNet service, string pair)
        {
            var tradingPairs = await service.GetTradingPairs();

            var asset = tradingPairs.Where(c => c.Pair.Equals(pair) || c.DashedPair.Equals(pair)).FirstOrDefault();

            return asset;
        }

        /// <summary>
        /// Get ticker for a trading pair
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <returns>Ticker object</returns>
        public static async Task<Ticker> GetTicker(this IBilaxyDotNet service, string pair)
        {
            var asset = await GetTradingPair(service, pair);

            return await service.GetTicker(asset.AssetId);
        }

        /// <summary>
        /// Get market depth
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <param name="figures">Significant figures</param>
        /// <returns>Depth for trading pair</returns>
        public static async Task<Depth> GetDepth(this IBilaxyDotNet service, string pair, Figures figures = Figures.Default)
        {
            var asset = await GetTradingPair(service, pair);

            return await service.GetDepth(asset.AssetId, figures);
        }

        /// <summary>
        /// Get market depth converted
        /// </summary>
        /// <param name="pairId">Trading pair id</param>
        /// <param name="figures">Significant figures</param>
        /// <returns>DepthConverted for trading pair</returns>
        public static async Task<DepthConverted> GetDepthConverted(this IBilaxyDotNet service, int pairId, Figures figures = Figures.Default)
        {
            var results = await service.GetDepth(pairId, figures);

            var asks = new List<DepthDetail>();

            foreach (var ask in results.Asks)
            {
                var detail = new DepthDetail
                {
                    Price = ask[0],
                    Quantity = ask[1],
                    TotalBase = ask[2]
                };
                asks.Add(detail);
            }
            var bids = new List<DepthDetail>();

            foreach (var bid in results.Bids)
            {
                var detail = new DepthDetail
                {
                    Price = bid[0],
                    Quantity = bid[1],
                    TotalBase = bid[2]
                };
                bids.Add(detail);
            }

            return new DepthConverted(asks, bids);
        }

        /// <summary>
        /// Get market depth converted
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <param name="figures">Significant figures</param>
        /// <returns>Depth for trading pair</returns>
        public static async Task<DepthConverted> GetDepthConverted(this IBilaxyDotNet service, string pair, Figures figures = Figures.Default)
        {
            var asset = await GetTradingPair(service, pair);

            return await GetDepthConverted(service, asset.AssetId, figures);
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
            var asset = await GetTradingPair(service, pair);

            return await service.GetPairOrders(asset.AssetId, records);
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
            var currencies = await service.GetTradingPairs();
            var currency = currencies.Where(c => c.Pair.StartsWith(symbol) || c.DashedPair.StartsWith(symbol)).FirstOrDefault();

            if (currency != null)
                return await service.GetDepositAddress(currency.AssetId);
            else
                throw new Exception("Currency does not exist");
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
        /// <param name="pair">Trading pair</param>
        /// <returns>Collection of Order objects</returns>
        public static async Task<List<Order>> GetOrders(this IBilaxyDotNet service, string pair)
        {
            var asset = await GetTradingPair(service, pair);

            return await service.GetOrders(asset.AssetId, 0, OrderType.AllOrders);
        }

        /// <summary>
        /// Get open orders
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <param name="orderType">OrderType</param>
        /// <returns>Collection of Order objects</returns>
        public static async Task<List<Order>> GetOrders(this IBilaxyDotNet service, string pair, OrderType orderType)
        {
            var asset = await GetTradingPair(service, pair);

            return await service.GetOrders(asset.AssetId, 0, orderType);
        }

        /// <summary>
        /// Get open orders
        /// </summary>
        /// <param name="pair">Trading pair/param>
        /// <param name="fromDate">From date</param>
        /// <returns>Collection of Order objects</returns>
        public static async Task<List<Order>> GetOrders(this IBilaxyDotNet service, string pair, long fromDate)
        {
            var asset = await GetTradingPair(service, pair);

            return await service.GetOrders(asset.AssetId, fromDate, OrderType.AllOrders);
        }

        /// <summary>
        /// Get open orders
        /// </summary>
        /// <param name="pair">Trading pair/param>
        /// <param name="fromDate">From date</param>
        /// <param name="orderType">OrderType</param>
        /// <returns>Collection of Order objects</returns>
        public static async Task<List<Order>> GetOrders(this IBilaxyDotNet service, string pair, long fromDate, OrderType orderType)
        {
            var asset = await GetTradingPair(service, pair);

            return await service.GetOrders(asset.AssetId, fromDate, orderType);
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
        /// <param name="fromDate">From date</param>
        /// <param name="orderType">OrderType</param>
        /// <returns>Collection of Order objects</returns>
        public static async Task<List<Order>> GetOrders(this IBilaxyDotNet service, int pairId, DateTime fromDate, OrderType orderType)
        {
            var unixTime = dtHelper.LocalToUnixTime(fromDate);

            return await service.GetOrders(pairId, unixTime, orderType);
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
            var asset = await GetTradingPair(service, pair);

            return await service.GetOrders(asset.AssetId, 0, OrderType.Pending);
        }

        /// <summary>
        /// Get open orders
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <param name="fromDate">From date</param>
        /// <returns>Collection of Order objects</returns>
        public static async Task<List<Order>> GetOpenOrders(this IBilaxyDotNet service, string pair, DateTime fromDate)
        {
            var asset = await GetTradingPair(service, pair);
            var unixTime = dtHelper.LocalToUnixTime(fromDate);

            return await service.GetOrders(asset.AssetId, unixTime, OrderType.Pending);
        }

        /// <summary>
        /// Get open orders
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <param name="fromDate">From date</param>
        /// <returns>Collection of Order objects</returns>
        public static async Task<List<Order>> GetOpenOrders(this IBilaxyDotNet service, string pair, long fromDate)
        {
            var asset = await GetTradingPair(service, pair);

            return await service.GetOrders(asset.AssetId, fromDate, OrderType.Pending);
        }

        /// <summary>
        /// Place a limit order
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <param name="price">Order price</param>
        /// <param name="quantity">Order quantity</param>
        /// <param name="side">Order side</param>
        /// <returns>New order id</returns>
        public static async Task<string> LimitOrder(this IBilaxyDotNet service, string pair, decimal price, decimal quantity, Side side)
        {
            var asset = await GetTradingPair(service, pair);

            return await service.PlaceOrder(asset.AssetId, quantity, price, side);
        }

        /// <summary>
        /// Place a market order
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <param name="quantity">Order quantity</param>
        /// <param name="side">Order side</param>
        /// <returns>New order id</returns>
        public static async Task<string> MarketOrder(this IBilaxyDotNet service, string pair, decimal price, decimal quantity, Side side)
        {
            var asset = await GetTradingPair(service, pair);
            var ticker = await service.GetTicker(asset.AssetId);

            return await service.PlaceOrder(asset.AssetId, quantity, ticker.Last, side);
        }

        #endregion Secure Api
    }
}