namespace HareDu.Model
{
    using System;
    using Newtonsoft.Json;

    public class ChannelInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("consumer_count")]
        public int ConsumerCount { get; set; }
        [JsonProperty("prefetch_count")]
        public int PrefetchCount { get; set; }
        [JsonProperty("idle_since")]
        public DateTime IdleSince { get; set; }
        [JsonProperty("transactional")]
        public bool IsTransactional { get; set; }
        [JsonProperty("confirm")]
        public bool Confirm { get; set; }
        [JsonProperty("client_flow_blocked")]
        public bool IsClientFlowBlocked { get; set; }
        [JsonProperty("node")]
        public string Node { get; set; }
        //[JsonProperty("name")]
        //public string PeerAddress { get; set; }
        //[JsonProperty("name")]
        //public int PeerPort { get; set; }
        [JsonProperty("vhost")]
        public string VirtualHostName { get; set; }
        [JsonProperty("user")]
        public string User { get; set; }
        [JsonIgnore]
        public MessageStatsInfo MessageStats { get; set; }
        [JsonProperty("messages_unacknowledged")]
        public int Unacknowledged { get; set; }
        [JsonProperty("messages_unconfirmed")]
        public int Unconfirmed { get; set; }
        [JsonProperty("messages_uncommitted")]
        public int Uncommitted { get; set; }
        [JsonProperty("acks_uncommitted")]
        public int AcknowledgesUncommitted { get; set; }
    }
}