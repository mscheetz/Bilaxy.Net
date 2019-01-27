using System;
using System.Collections.Generic;
using System.Text;

namespace Bilaxy.Net.Contracts
{
    public enum Figures
    {
        Default = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4
    }

    public enum Side
    {
        buy,
        sell
    }

    public enum OrderType
    {
        AllOrders = 0,
        Pending = 1
    }
}
