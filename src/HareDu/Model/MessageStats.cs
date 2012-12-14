namespace HareDu.Model
{
    using Newtonsoft.Json;

    public class MessageStats
    {
        [JsonProperty("publish")]
        public int Published { get; set; }
        [JsonProperty("ack")]
        public int Acknowledged { get; set; }
        [JsonProperty("deliver")]
        public int Delivered { get; set; }
        [JsonProperty("deliver_get")]
        public int DeliveredOrGet { get; set; }
    }
}