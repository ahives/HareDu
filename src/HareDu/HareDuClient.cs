namespace HareDu
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using Newtonsoft.Json.Linq;

    public class HareDuClient
    {
        public HareDuClient(string hostUrl, int port, string username, string password)
        {
            Username = username;
            Password = password;
            HostUrl = hostUrl;
            Port = port;
        }

        private string Username { get; set; }
        private string Password { get; set; }
        private string HostUrl { get; set; }
        private int Port { get; set; }

        private string GetEndpoint(string url)
        {
            return string.Format("{0}:{1}/api/{2}", HostUrl, Port, url);
        }

        private HttpWebRequest MakeHttpGetRequest(string url)
        {
            string endpoint = GetEndpoint(url);
            var request = WebRequest.Create(endpoint) as HttpWebRequest;

            if (request == null)
            {
            }

            request.Method = "GET";
            request.ContentType = "application/json";
            request.Credentials = new NetworkCredential(Username, Password);

            return request;
        }

        private HttpWebRequest MakeHttpPostRequest(string url)
        {
            throw new NotImplementedException();
        }

        private string GetHttpResponse(HttpWebRequest request)
        {
            var response = request.GetResponse() as HttpWebResponse;
            using (Stream responseStream = response.GetResponseStream())
            {
                using (var reader = new StreamReader(responseStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public IEnumerable<string> GetVirtualHosts()
        {
            var request = MakeHttpGetRequest("vhosts");
            string response = GetHttpResponse(request);
            var parser = JArray.Parse(response);

            return from x in parser.Children()["name"]
                   select x.Value<string>();
        }

        public IEnumerable<ExchangeInfo> GetExchanges()
        {
            var request = MakeHttpGetRequest("exchanges");
            string response = GetHttpResponse(request);
            var parser = JArray.Parse(response);

            return from x in parser.Children()
                   select new ExchangeInfo
                              {
                                  Name = x["name"].Value<string>(),
                                  VirtualHost = x["vhost"].Value<string>(),
                                  Type = x["type"].Value<string>(),
                                  IsDurable = x["durable"].Value<bool>(),
                                  IsSetToAutoDelete = x["auto_delete"].Value<bool>(),
                                  IsInternal = x["internal"].Value<bool>()
                              };
        }

        public IEnumerable<ConnectionInfo> GetOpenConnections()
        {
            var request = MakeHttpGetRequest("connections");
            string response = GetHttpResponse(request);
            var parser = JArray.Parse(response);

            return from x in parser.Children()
                   select new ConnectionInfo
                              {
                                  Name = x["name"].Value<string>(),
                                  VirtualHost = x["vhost"].Value<string>(),
                                  Type = x["type"].Value<string>(),
                                  Timeout = x["timeout"].Value<int>(),
                                  Port = x["port"].Value<int>(),
                                  State = x["state"].Value<string>(),
                                  Channels = x["channels"].Value<int>(),
                                  Node = x["node"].Value<string>(),
                                  Address = x["address"].Value<string>(),
                                  PeerAddress = x["peer_address"].Value<string>(),
                                  PeerPort = x["peer_port"].Value<int>(),
                                  IsRunningWithSSL = x["ssl"].Value<bool>(),
                                  SslProtocol = x["ssl_protocol"].Value<string>(),
                                  SslKeyExchange = x["ssl_key_exchange"].Value<string>(),
                                  SslCipher = x["ssl_cipher"].Value<string>(),
                                  SslHash = x["ssl_hash"].Value<string>(),
                                  AuthenticationMechanismUsed = x["auth_mechanism"].Value<string>(),
                                  PeerCertificateIssuer = x["peer_cert_issuer"].Value<string>(),
                                  Protocol = x["protocol"].Value<string>(),
                                  User = x["user"].Value<string>(),
                              };
        }
    }
}