namespace HareDu
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class QueueBindingsPostRequestParams
    {
        [JsonIgnore]
        public string ExchangeName { get; set; }
        [JsonIgnore]
        public string VirtualHostName { get; set; }
        [JsonIgnore]
        public string QueueName { get; set; }
        [JsonProperty(PropertyName = "routing_key", Order = 1)]
        public string RoutingKey { get; set; }
        [JsonProperty(PropertyName = "arguments", Order = 2, Required = Required.Default)]
        public List<string> Arguments { get; set; }

        public QueueBindingsPostRequestParams()
        {
            Arguments = new List<string>();
        }
    }
}