using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bilaxy.Net.Contracts
{
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
