// Copyright 2013-2014 Albert L. Hives, Chris Patterson, et al.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace HareDu.Model
{
    using Newtonsoft.Json;

    public class Connection :
        HareDuModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("vhost")]
        public string VirtualHost { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("port")]
        public int Port { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("channels")]
        public int ChannelsOpen { get; set; }

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