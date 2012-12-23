namespace HareDu.Model
{
    using Newtonsoft.Json;

    public class Connection
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("vhost")]
        public string VirtualHostName { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("port")]
        public int Port { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("channels")]
        public int Channels { get; set; }

        [JsonProperty("node")]
        public string Node { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("peer_address")]
        public string PeerAddress { get; set; }

        [JsonProperty("peer_port")]
        public int PeerPort { get; set; }

        [JsonProperty("ssl")]
        public bool IsRunningWithSSL { get; set; }

        [JsonProperty("ssl_protocol")]
        public string SslProtocol { get; set; }

        [JsonProperty("ssl_key_exchange")]
        public string SslKeyExchange { get; set; }

        [JsonProperty("ssl_cipher")]
        public string SslCipher { get; set; }

        [JsonProperty("ssl_hash")]
        public string SslHash { get; set; }

        [JsonProperty("auth_mechanism")]
        public string AuthenticationMechanismUsed { get; set; }

        [JsonProperty("peer_cert_issuer")]
        public string PeerCertificateIssuer { get; set; }

        [JsonProperty("protocol")]
        public string Protocol { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("timeout")]
        public int Timeout { get; set; }
    }
}