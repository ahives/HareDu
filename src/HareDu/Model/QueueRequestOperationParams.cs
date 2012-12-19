namespace HareDu.Model
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class QueueRequestOperationParams
    {
        [JsonIgnore]
        public string QueueName { get; set; }
        [JsonIgnore]
        public string VirtualHostName { get; set; }
        [JsonProperty(PropertyName = "durable", Order = 2)]
        public bool IsDurable { get; set; }
        [JsonProperty(PropertyName = "auto_delete", Order = 1)]
        public bool IsAutoDelete { get; set; }
        [JsonProperty(PropertyName = "arguments", Order = 3, Required = Required.Default)]
        public List<string> Arguments { get; set; }
        [JsonProperty(PropertyName = "node", Order = 4, Required = Required.Default)]
        public string Node { get; set; }

        public QueueRequestOperationParams()
        {
            Arguments = new List<string>();
        }
    }
}