namespace HareDu.Model
{
    using Newtonsoft.Json;

    public class VirtualHost
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tracing")]
        public bool Tracing { get; set; }
    }
}