using Newtonsoft.Json;

namespace HareDu.Model
{
    public class Permission
    {
        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("vhost")]
        public string VirtualHost { get; set; }

        [JsonProperty("configure")]
        public string Configure { get; set; }

        [JsonProperty("write")]
        public string Write { get; set; }

        [JsonProperty("read")]
        public string Read { get; set; }
    }
}