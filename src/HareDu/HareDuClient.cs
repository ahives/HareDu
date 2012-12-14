namespace HareDu
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Text;
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

        private HttpWebRequest BuildHttpGetRequest(string url)
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

        private HttpWebRequest BuildHttpPutRequest(string url, long contentLength)
        {
            var endpoint = BuildRestEndpoint(url);
            var uri = new Uri(endpoint);
            ForceCanonicalPathAndQuery(uri);
            var request = WebRequest.Create(uri) as HttpWebRequest;

            if (request == null)
            {
            }

            request.Method = "PUT";
            request.ContentType = "application/json";
            request.Credentials = new NetworkCredential(Username, Password);
            request.ContentLength = contentLength;

            return request;
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
        //    var request = BuildHttpGetRequest("vhosts");
        //    string response = GetHttpResponseBody(request);
        //    var parser = JArray.Parse(response);

        //    return from x in parser.Children()["name"]
        //           select x.Value<string>();
        //}

        public IEnumerable<string> GetListOfAllQueuesInVirtualHost(string vhost)
        {
            var request = BuildHttpGetRequest(string.Format(@"queues/{0}", vhost.SanitizeVirtualHostName()));
            string response = GetHttpResponseBody(request);
            //var parser = JArray.Parse(response);

            return JsonConvert.DeserializeObject<IEnumerable<string>>(response);
            //return from x in parser.Children()["name"]
            //       select x.Value<string>();
            //throw new NotImplementedException();
        }

        public IEnumerable<QueueInfo> GetListOfAllQueues()
        {
            var request = BuildHttpGetRequest("queues");
            string response = GetHttpResponseBody(request);

            return JsonConvert.DeserializeObject<IEnumerable<QueueInfo>>(response);
        }

        public IEnumerable<ExchangeInfo> GetListOfAllExchanges()
        {
            var request = BuildHttpGetRequest("exchanges");
            string response = GetHttpResponseBody(request);

            return JsonConvert.DeserializeObject<IEnumerable<ExchangeInfo>>(response);
        }

        public IEnumerable<ConnectionInfo> GetListOfAllOpenConnections()
        {
            var request = BuildHttpGetRequest("connections");
            string response = GetHttpResponseBody(request);

            return JsonConvert.DeserializeObject<IEnumerable<ConnectionInfo>>(response);
        }

        public IEnumerable<ChannelInfo> GetListOfAllOpenChannels()
        {
            var request = BuildHttpGetRequest("connections");
            string response = GetHttpResponseBody(request);

            return JsonConvert.DeserializeObject<IEnumerable<ChannelInfo>>(response);
        }

        public IEnumerable<QueueBindingInfo> GetListOfAllBindingsOnQueue(string virtualHostName, string queueName)
        {
            var request = BuildHttpGetRequest(string.Format("queues/{0}/{1}/bindings", virtualHostName.SanitizeVirtualHostName(), queueName));
            string response = GetHttpResponseBody(request);

            return JsonConvert.DeserializeObject<IEnumerable<QueueBindingInfo>>(response);
        }

        public string CreateQueue(QueuePutRequestParams queue)
        {
            var json = JsonConvert.SerializeObject(queue);
            var requestBody = Encoding.UTF8.GetBytes(json);
            var request =
                BuildHttpPutRequest(
                    string.Format("/api/queues/{0}/{1}", queue.VirtualHostName.SanitizeVirtualHostName(), queue.Name),
                    requestBody.Length);

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(requestBody, 0, requestBody.Length);
            }
            return GetHttpResponseBody(request);
        }
    }
}