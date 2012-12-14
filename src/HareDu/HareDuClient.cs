namespace HareDu
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using Model;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class HareDuClient
    {
        private string Username { get; set; }
        private string Password { get; set; }
        private string HostUrl { get; set; }
        private int Port { get; set; }

        public HareDuClient(string hostUrl, int port, string username, string password)
        {
            Username = username;
            Password = password;
            HostUrl = hostUrl;
            Port = port;
        }

        private string BuildRestEndpoint(string url)
        {
            return string.Format("{0}:{1}/api/{2}", HostUrl, Port, url);
        }

        private void ForceCanonicalPathAndQuery(Uri uri)
        {
            var paq = uri.PathAndQuery;
            var flagsFieldInfo = typeof(Uri).GetField("m_Flags", BindingFlags.Instance | BindingFlags.NonPublic);
            var flags = (ulong)flagsFieldInfo.GetValue(uri);

            flags &= ~((ulong)0x30);
            flagsFieldInfo.SetValue(uri, flags);
        }

        private HttpWebRequest MakeRequestViaHttpGet(string url)
        {
            var endpoint = BuildRestEndpoint(url);
            var uri = new Uri(endpoint);
            ForceCanonicalPathAndQuery(uri);
            var request = WebRequest.Create(uri) as HttpWebRequest;

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

        //public IEnumerable<string> GetListOfVirtualHosts()
        //{
        //    var request = MakeRequestViaHttpGet("vhosts");
        //    string response = GetHttpResponseBody(request);
        //    var parser = JArray.Parse(response);

        //    return from x in parser.Children()["name"]
        //           select x.Value<string>();
        //}

        public IEnumerable<string> GetListOfAllQueuesInVirtualHost(string vhost)
        {
            var request = MakeRequestViaHttpGet(string.Format(@"queues/{0}", vhost.SanitizeVirtualHostName()));
            string response = GetHttpResponseBody(request);
            //var parser = JArray.Parse(response);

            return JsonConvert.DeserializeObject<IEnumerable<string>>(response);
            //return from x in parser.Children()["name"]
            //       select x.Value<string>();
            //throw new NotImplementedException();
        }

        public IEnumerable<QueueInfo> GetListOfAllQueues()
        {
            var request = MakeRequestViaHttpGet("queues");
            string response = GetHttpResponseBody(request);

            return JsonConvert.DeserializeObject<IEnumerable<QueueInfo>>(response);
        }

        public IEnumerable<ExchangeInfo> GetListOfAllExchanges()
        {
            var request = MakeRequestViaHttpGet("exchanges");
            string response = GetHttpResponseBody(request);

            return JsonConvert.DeserializeObject<IEnumerable<ExchangeInfo>>(response);
        }

        public IEnumerable<ConnectionInfo> GetListOfAllOpenConnections()
        {
            var request = MakeRequestViaHttpGet("connections");
            string response = GetHttpResponseBody(request);

            return JsonConvert.DeserializeObject<IEnumerable<ConnectionInfo>>(response);
        }

        public IEnumerable<ChannelInfo> GetListOfAllOpenChannels()
        {
            var request = MakeRequestViaHttpGet("connections");
            string response = GetHttpResponseBody(request);

            return JsonConvert.DeserializeObject<IEnumerable<ChannelInfo>>(response);
        }

        public IEnumerable<QueueBindingInfo> GetListOfAllBindingsOnQueue(string virtualHostName, string queueName)
        {
            var request = MakeRequestViaHttpGet(string.Format("queues/{0}/{1}/bindings", virtualHostName.SanitizeVirtualHostName(), queueName));
            string response = GetHttpResponseBody(request);

            return JsonConvert.DeserializeObject<IEnumerable<QueueBindingInfo>>(response);
        }
    }
}