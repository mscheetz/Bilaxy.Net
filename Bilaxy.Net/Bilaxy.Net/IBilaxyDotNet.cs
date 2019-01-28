// -----------------------------------------------------------------------------
// <copyright file="IBilaxyDotNet" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/27/2019 2:43:59 PM" />
// -----------------------------------------------------------------------------

namespace Bilaxy.Net
{
    #region Usings

    using Bilaxy.Net.Contracts;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    #endregion Usings

    public interface IBilaxyDotNet
    {
        /// <summary>
        /// Get all currencies on the exchange
        /// </summary>
        /// <returns>Collection of Coin objects</returns>
        Task<List<Coin>> GetCurrencies();
        
        /// <summary>
        /// Get all trading pairs
        /// </summary>
        /// <param name="service"></param>
        /// <returns>Collection of trading pairs</returns>
        Task<List<Asset>> GetTradingPairs();

        /// <summary>
        /// Get ticker for a trading pair
        /// </summary>
        /// <param name="pairId">Id of trading pair</param>
        /// <returns>Ticker object</returns>
        Task<Ticker> GetTicker(int pairId);

        /// <summary>
        /// Get tickers for all trading pairs
        /// </summary>
        /// <returns>Collection of Ticker objects</returns>
        Task<List<Ticker>> GetTickers();

        /// <summary>
        /// Get market depth
        /// </summary>
        /// <param name="pairId">Trading pair id</param>
        /// <param name="figures">Significant figures</param>
        /// <returns>Depth for trading pair</returns>
        Task<Depth> GetDepth(int pairId, Figures figures = Figures.Default);

        /// <summary>
        /// Get recent orders
        /// </summary>
        /// <param name="pairId">Trading pair id</param>
        /// <param name="records">Number of records to return</param>
        /// <returns>Collection of Order objects</returns>
        Task<List<Order>> GetPairOrders(int pairId, int size);

        /// <summary>
        /// Get account balances
        /// </summary>
        /// <returns>Collection of Balance objects</returns>
        Task<List<Balance>> GetBalances();

        /// <summary>
        /// Get a deposit address for a currency
        /// </summary>
        /// <param name="assetId">Currency id</param>
        /// <returns>String of deposit address</returns>
        Task<string> GetDepositAddress(int assetId);

        /// <summary>
        /// Get account orders
        /// </summary>
        /// <param name="pairId">Trading pair id</param>
        /// <param name="fromDate">From date</param>
        /// <param name="orderType">Order type</param>
        /// <returns>Collection of Order objects</returns>
        Task<List<Order>> GetOrders(int pairId, long fromDate, OrderType orderType);

        /// <summary>
        /// Cancel an order
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <returns>Canceled order id</returns>
        Task<string> CancelOrder(int orderId);

        /// <summary>
        /// Place an order
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <returns>Placed order id</returns>
        Task<string> PlaceOrder(int pairId, decimal quantity, decimal price, Side side);
    }
}