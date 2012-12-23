namespace HareDu.Internal
{
    using HareDu.Contracts;
    using Newtonsoft.Json;

    public class UserArgsImpl :
        UserArgs
    {
        [JsonProperty(PropertyName = "password", Order = 1)]
        public string Password { get; set; }
        [JsonProperty(PropertyName = "tags", Order = 2)]
        public string Tags { get; set; }

        public void WithPassword(string password)
        {
            password.CheckIfArgValid("password");
            Password = password;
        }

        public void WithTag(string tags)
        {
            Tags = tags;
        }
    }
}