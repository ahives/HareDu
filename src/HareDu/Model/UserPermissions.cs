namespace HareDu.Model
{
    using Newtonsoft.Json;

    public class UserPermissions
    {
        [JsonProperty("user")]
        public string UserName { get; set; }

        [JsonProperty("vhost")]
        public string VirtualHostName { get; set; }

        [JsonProperty("configure")]
        public string ConfigurePermissions { get; set; }

        [JsonProperty("write")]
        public string WritePermissions { get; set; }

        [JsonProperty("read")]
        public string ReadPermissions { get; set; }
    }
}