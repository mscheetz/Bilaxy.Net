// -----------------------------------------------------------------------------
// <copyright file="Depth" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/27/2019 2:48:53 PM" />
// -----------------------------------------------------------------------------

namespace Bilaxy.Net.Contracts
{
    #region Usings

    using Newtonsoft.Json;
    using System.Collections.Generic;

    #endregion Usings

    public class Depth
    {
        [JsonProperty(PropertyName = "asks")]
        public List<List<decimal>> Asks { get; set; }

        [JsonProperty(PropertyName = "bids")]
        public List<List<decimal>> Bids { get; set; }
    }
}
