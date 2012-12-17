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

        private HttpWebRequest BuildHttpPostRequest(string url, long contentLength)
        {
            var endpoint = BuildRestEndpoint(url);
            var uri = new Uri(endpoint);
            ForceCanonicalPathAndQuery(uri);
            var request = WebRequest.Create(uri) as HttpWebRequest;

            if (request == null)
            {
            }

            request.Method = "POST";
            request.ContentType = "application/json";
            request.Credentials = new NetworkCredential(Username, Password);
            request.ContentLength = contentLength;

            return request;
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

        public IEnumerable<Queue> GetListOfAllQueues()
        {
            var request = BuildHttpGetRequest("queues");
            string response = GetHttpResponseBody(request);

            return JsonConvert.DeserializeObject<IEnumerable<Queue>>(response);
        }

        public IEnumerable<Exchange> GetListOfAllExchanges()
        {
            var request = BuildHttpGetRequest("exchanges");
            string response = GetHttpResponseBody(request);

            return JsonConvert.DeserializeObject<IEnumerable<Exchange>>(response);
        }

        public IEnumerable<Connection> GetListOfAllOpenConnections()
        {
            var request = BuildHttpGetRequest("connections");
            string response = GetHttpResponseBody(request);

            return JsonConvert.DeserializeObject<IEnumerable<Connection>>(response);
        }

        public IEnumerable<Channel> GetListOfAllOpenChannels()
        {
            var request = BuildHttpGetRequest("connections");
            string response = GetHttpResponseBody(request);

            return JsonConvert.DeserializeObject<IEnumerable<Channel>>(response);
        }

        public IEnumerable<QueueBinding> GetListOfAllBindingsOnQueue(string virtualHostName, string queueName)
        {
            var request = BuildHttpGetRequest(string.Format("queues/{0}/{1}/bindings", virtualHostName.SanitizeVirtualHostName(), queueName));
            string response = GetHttpResponseBody(request);

            return JsonConvert.DeserializeObject<IEnumerable<QueueBinding>>(response);
        }

        public void CreateQueue(QueuePutRequestParams queue)
        {
            var json = JsonConvert.SerializeObject(queue);
            var requestBody = Encoding.UTF8.GetBytes(json);
            var request =
                BuildHttpPutRequest(
                    string.Format("queues/{0}/{1}", queue.VirtualHostName.SanitizeVirtualHostName(), queue.QueueName),
                    requestBody.Length);

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(requestBody, 0, requestBody.Length);
            }
        }

        public void CreateExchange(ExchangePutRequestParams exchange)
        {
            var json = JsonConvert.SerializeObject(exchange);
            var requestBody = Encoding.UTF8.GetBytes(json);
            var request =
                BuildHttpPutRequest(
                    string.Format("exchanges/{0}/{1}", exchange.VirtualHostName.SanitizeVirtualHostName(), exchange.ExchangeName),
                    requestBody.Length);

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(requestBody, 0, requestBody.Length);
            }
        }

        public void CreateQueueBindings(QueueBindingsPostRequestParams queueBinding)
        {
            queueBinding.RoutingKey = queueBinding.RoutingKey ?? string.Empty;
            var json = JsonConvert.SerializeObject(queueBinding);
            var requestBody = Encoding.UTF8.GetBytes(json);
            var request =
                BuildHttpPostRequest(
                    string.Format("bindings/{0}/e/{1}/q/{2}", queueBinding.VirtualHostName.SanitizeVirtualHostName(), queueBinding.ExchangeName, queueBinding.QueueName),
                    requestBody.Length);

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(requestBody, 0, requestBody.Length);
            }
        }
    }
}