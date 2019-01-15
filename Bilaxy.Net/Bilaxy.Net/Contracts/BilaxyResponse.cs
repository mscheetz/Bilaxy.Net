using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bilaxy.Net.Contracts
{
    public class BilaxyResponse<T>
    {
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }

        [JsonProperty(PropertyName = "data")]
        public T Data { get; set; }
    }
}
