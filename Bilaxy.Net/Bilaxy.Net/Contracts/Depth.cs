using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bilaxy.Net.Contracts
{
    public class Depth
    {
        [JsonProperty(PropertyName = "asks")]
        public DepthDetail Asks { get; set; }

        [JsonProperty(PropertyName = "bids")]
        public DepthDetail Bids { get; set; }
    }
}
