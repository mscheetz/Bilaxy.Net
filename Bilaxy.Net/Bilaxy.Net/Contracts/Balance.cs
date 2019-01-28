// -----------------------------------------------------------------------------
// <copyright file="Balance" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/27/2019 2:48:53 PM" />
// -----------------------------------------------------------------------------

namespace Bilaxy.Net.Contracts
{
    #region Usings

    using Newtonsoft.Json;

    #endregion Usings

    public class Balance
    {
        [JsonProperty(PropertyName = "symbol")]
        public int AssetId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Asset { get; set; }

        [JsonProperty(PropertyName = "balance")]
        public decimal Total { get; set; }

        [JsonProperty(PropertyName = "frozen")]
        public decimal Frozen { get; set; }
    }
}
