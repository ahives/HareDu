namespace HareDu.Model
{
    using Newtonsoft.Json;

    public class Queue
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("memory")]
        public string Memory { get; set; }
        [JsonProperty("idle_since")]
        public string IdleSince { get; set; }
        [JsonProperty("messages_ready")]
        public int MessagesReady { get; set; }
        [JsonProperty("messages_unacknowledged")]
        public string MessagesUnacknowledged { get; set; }
        [JsonProperty("messages")]
        public string Messages { get; set; }
        [JsonProperty("consumers")]
        public string Consumers { get; set; }
        [JsonProperty("vhost")]
        public string VirtualHostName { get; set; }
        [JsonProperty("durable")]
        public bool IsDurable { get; set; }
        [JsonProperty("node")]
        public string Node { get; set; }
    }
}