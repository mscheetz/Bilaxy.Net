// -----------------------------------------------------------------------------
// <copyright file="Order" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/27/2019 2:48:53 PM" />
// -----------------------------------------------------------------------------

namespace Bilaxy.Net.Contracts
{
    #region Usings

    using Newtonsoft.Json;

    #endregion Usings

    public class Order
    {
        [JsonProperty(PropertyName = "date")]
        public long Date { get; set; }

        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }

        [JsonProperty(PropertyName = "count")]
        public decimal Count { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "type")]
        public Side Type { get; set; }
    }
}
