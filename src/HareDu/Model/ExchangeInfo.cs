namespace HareDu.Model
{
    using Newtonsoft.Json;

    public class ExchangeInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("vhost")]
        public string VirtualHost { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("durable")]
        public bool IsDurable { get; set; }
        [JsonProperty("auto_delete")]
        public bool IsSetToAutoDelete { get; set; }
        [JsonProperty("internal")]
        public bool IsInternal { get; set; }
    }
}