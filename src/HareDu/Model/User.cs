namespace HareDu.Model
{
    using Newtonsoft.Json;

    public class User
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("password_hash")]
        public string PasswordHash { get; set; }

        [JsonProperty("tags")]
        public string Tags { get; set; }
    }
}