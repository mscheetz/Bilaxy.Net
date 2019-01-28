// -----------------------------------------------------------------------------
// <copyright file="Enums" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/27/2019 2:48:53 PM" />
// -----------------------------------------------------------------------------

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
