using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bilaxy.Net.Contracts
{
    public class Ticker
    {
        public string Pair { get; set; }

        [JsonProperty(PropertyName = "symbol")]
        public int PairId { get; set; }

        [JsonProperty(PropertyName = "high")]
        public decimal High { get; set; }

        [JsonProperty(PropertyName = "low")]
        public decimal Low { get; set; }

        [JsonProperty(PropertyName = "buy")]
        public decimal Buy { get; set; }

        [JsonProperty(PropertyName = "sell")]
        public decimal Sell { get; set; }

        [JsonProperty(PropertyName = "last")]
        public decimal Last { get; set; }

        [JsonProperty(PropertyName = "vol")]
        public decimal Volume { get; set; }
    }
}
