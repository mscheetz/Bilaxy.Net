// -----------------------------------------------------------------------------
// <copyright file="DepthConverted" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/27/2019 7:22:22 PM" />
// -----------------------------------------------------------------------------

namespace Bilaxy.Net.Contracts
{
    #region Usings

    using System.Collections.Generic;

    #endregion Usings

    public class DepthConverted
    {
        #region Properties

        public List<DepthDetail> Asks { get; set; }

        public List<DepthDetail> Bids { get; set; }

        #endregion Properties

        public DepthConverted(List<DepthDetail> asks, List<DepthDetail> bids)
        {
            Asks = asks;
            Bids = bids;
        }

    }
}