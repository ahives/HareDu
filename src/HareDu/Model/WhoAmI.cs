using Newtonsoft.Json;

namespace HareDu.Model
{
    public class WhoAmI
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tags")]
        public string Tags { get; set; }

        [JsonProperty("auth_backend")]
        public string AuthBackend { get; set;  }
    }
}