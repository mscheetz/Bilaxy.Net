using Bilaxy.Net.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bilaxy.Net.Interfaces
{
    public interface IBilaxyRepository
    {
        Task<Ticker> GetTicker(string pair);

        Task<Ticker> GetTicker(int pairId);

        Task<List<Ticker>> GetTickers(string pair);

        Task<List<Ticker>> GetTickers(int pairId);

        Task<Depth> GetDepth(string pair, Figures figures = Figures.Default);

        Task<Depth> GetDepth(int pairId, Figures figures = Figures.Default);

        Task<List<Order>> GetOrders(string pair, int size = 100);

        Task<List<Order>> GetOrders(int pairId, int size = 100);

        Task<string> GetDepositAddress(string symbol);

        Task<string> GetDepositAddress(int assetId);
    }
}
