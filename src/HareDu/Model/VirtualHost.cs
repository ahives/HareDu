using Newtonsoft.Json;

namespace HareDu.Model
{
    public class VirtualHost
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tracing")]
        public string Tracing { get; set; }
    }
}