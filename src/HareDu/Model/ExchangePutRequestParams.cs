namespace HareDu.Model
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class ExchangePutRequestParams
    {
        [JsonIgnore]
        public string ExchangeName { get; set; }
        [JsonIgnore]
        public string VirtualHostName { get; set; }
        [JsonProperty(PropertyName = "type", Order = 1)]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "auto_delete", Order = 2)]
        public bool IsAutoDelete { get; set; }
        [JsonProperty(PropertyName = "durable", Order = 3)]
        public bool IsDurable { get; set; }
        [JsonProperty(PropertyName = "internal", Order = 4)]
        public bool Internal { get; set; }
        [JsonProperty(PropertyName = "arguments", Order = 5, Required = Required.Default)]
        public List<string> Arguments { get; set; }

        public ExchangePutRequestParams()
        {
            Arguments = new List<string>();
        }
    }
}