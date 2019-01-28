// -----------------------------------------------------------------------------
// <copyright file="DepthDetail" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/27/2019 2:48:53 PM" />
// -----------------------------------------------------------------------------

namespace Bilaxy.Net.Contracts
{
    public class DepthDetail
    {
        public decimal Price { get; set; }

        public decimal Quantity { get; set; }

        public decimal TotalBase { get; set; }
    }
}
