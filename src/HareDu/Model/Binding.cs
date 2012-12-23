namespace HareDu.Model
{
    using Newtonsoft.Json;

    public class Binding
    {
        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("destination")]
        public string Destination { get; set; }

        [JsonProperty("destination_type")]
        public string DestinationType { get; set; }

        [JsonProperty("routing_key")]
        public string RoutingKey { get; set; }

        [JsonProperty("vhost")]
        public string VirtualHostName { get; set; }

        [JsonProperty("properties_key")]
        public string PropertiesKey { get; set; }
    }
}