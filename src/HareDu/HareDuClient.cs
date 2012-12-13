namespace HareDu
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using Model;
    using Newtonsoft.Json;
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

        private string BuildRestEndpoint(string url)
        {
            return string.Format("{0}:{1}/api/{2}", HostUrl, Port, url);
        }

        private HttpWebRequest MakeRequestViaHttpGet(string url)
        {
            string endpoint = BuildRestEndpoint(url);
            var request = WebRequest.Create(endpoint) as HttpWebRequest;

            if (request == null)
            {
            }

            request.Method = "GET";
            request.ContentType = "application/json";
            request.Credentials = new NetworkCredential(Username, Password);

            return request;
        }

        private HttpWebRequest MakeRequestViaHttpPut(string url)
        {
            throw new NotImplementedException();
        }

        private HttpWebRequest MakeRequestViaHttpPost(string url)
        {
            throw new NotImplementedException();
        }

        private HttpWebRequest MakeRequestViaHttpDelete(string url)
        {
            throw new NotImplementedException();
        }

        private string GetHttpResponseBody(HttpWebRequest request)
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

        public IEnumerable<string> GetListOfVirtualHosts()
        {
            var request = MakeRequestViaHttpGet("vhosts");
            string response = GetHttpResponseBody(request);
            var parser = JArray.Parse(response);

            return from x in parser.Children()["name"]
                   select x.Value<string>();
        }

        public IEnumerable<string> GetListOfQueuesInVirtualHost(string vhost)
        {
            var request = MakeRequestViaHttpGet(string.Format(@"queues/{0}", vhost.Sanitize()));
            string response = GetHttpResponseBody(request);
            var parser = JArray.Parse(response);

            //return from x in parser.Children()["name"]
            //       select x.Value<string>();
            throw new NotImplementedException();
        }

        public IEnumerable<ExchangeInfo> GetInfoOnExchanges()
        {
            var request = MakeRequestViaHttpGet("exchanges");
            string response = GetHttpResponseBody(request);

            return JsonConvert.DeserializeObject<IEnumerable<ExchangeInfo>>(response);
        }

        public IEnumerable<ConnectionInfo> GetInfoOnOpenConnections()
        {
            var request = MakeRequestViaHttpGet("connections");
            string response = GetHttpResponseBody(request);
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
                                  User = x["user"].Value<string>()
                              };
        }

        // This is not working because the LINQ to JSON is all bad. Need to figure out how to get at the data appropriately.
        public IEnumerable<ChannelInfo> GetInfoOnOpenChannels()
        {
            var request = MakeRequestViaHttpGet("connections");
            string response = GetHttpResponseBody(request);
            var parser = JArray.Parse(response);

            return from x in parser.Children()
                   select new ChannelInfo
                              {
                                  Name = x["name"].GetValue<string>(),
                                  VirtualHost = x["vhost"].GetValue<string>(),
                                  //IdleSince = DateTime.Parse(x["idle_since"].GetValue<string>()),
                                  ConsumerCount = x["consumer_count"].GetValue<int>(),
                                  PrefetchCount = x["prefetch_count"].GetValue<int>(),
                                  IsTransactional = x["transactional"].GetValue<bool>(),
                                  MessageStats = new MessageStatsInfo
                                                     {
                                                         Published = x["message_stats"].Select(m => m["publish"].GetValue<int>()).FirstOrDefault(),
                                                         //Acknowledged = x["message_stats"]["ack"].GetValue<int>(),
                                                         //Delivered = x["message_stats"]["deliver"] != null ? x["message_stats"]["deliver"].Value<int>() : 0,
                                                         //DeliveredOrGet = x["message_stats"]["deliver_get"] != null ? x["message_stats"]["deliver_get"].Value<int>() : 0,
                                                         //Unacknowledged = x["messages_unacknowledged"].Value<int>(),
                                                         //Unconfirmed = x["messages_unconfirmed"].Value<int>(),
                                                         //Uncommitted = x["messages_uncommitted"].Value<int>(),
                                                         //AcknowledgesUncommitted = x["acks_uncommitted"].Value<int>()
                                                     },
                                  //Node = x["node"].Value<string>(),
                                  //Confirm = x["confirm"].Value<bool>(),
                                  //PeerAddress = x["connection_details"]["peer_address"].Value<string>(),
                                  //PeerPort = x["connection_details"]["peer_port"].Value<int>(),
                                  //IsClientFlowBlocked = x["client_flow_blocked"].Value<bool>(),
                                  //User = x["user"].Value<string>()
                              };
        }

        public IEnumerable<string> GetListOfAllQueues()
        {
            var request = MakeRequestViaHttpGet("queues");
            string response = GetHttpResponseBody(request);
            var parser = JArray.Parse(response);

            return from x in parser.Children()["name"]
                   select x.Value<string>();
        }

        public void GetListOfAllBindingsOnQueue()
        {
            ///api/queues/vhost/queue/bindings
        }
    }
}