// -----------------------------------------------------------------------------
// <copyright file="Coin" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/27/2019 7:56:14 PM" />
// -----------------------------------------------------------------------------

namespace Bilaxy.Net.Contracts
{
    #region Usings

    using Newtonsoft.Json;

    #endregion Usings

    public class Coin
    {
        #region Properties

        [JsonProperty(PropertyName = "symbol")]
        public int SymbolId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Symbol { get; set; }

        [JsonProperty(PropertyName = "priceDecimals")]
        public int Precision { get; set; }

        [JsonProperty(PropertyName = "group")]
        public string Group { get; set; }

        #endregion Properties
    }
}