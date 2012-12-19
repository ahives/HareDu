namespace HareDu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Model;

    public class HareDuClient
    {
        protected HttpClient Client { get; set; }

        public HareDuClient(string hostUrl, int port, string username, string password)
        {
            Client = new HttpClient(new HttpClientHandler
                                        {
                                            Credentials = new NetworkCredential(username, password)
                                        }) {BaseAddress = new Uri(string.Format("{0}:{1}/", hostUrl, port))};
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        //private string BuildRestEndpoint(string url)
        //{
        //    return string.Format("{0}:{1}/api/{2}", HostUrl, Port, url);
        //}

        //private void ForceCanonicalPathAndQuery(Uri uri)
        //{
        //    var paq = uri.PathAndQuery;
        //    var flagsFieldInfo = typeof(Uri).GetField("m_Flags", BindingFlags.Instance | BindingFlags.NonPublic);
        //    var flags = (ulong)flagsFieldInfo.GetValue(uri);

        //    flags &= ~((ulong)0x30);
        //    flagsFieldInfo.SetValue(uri, flags);
        //}

        //private HttpWebRequest BuildHttpRequest(string url)
        //{
        //    var endpoint = BuildRestEndpoint(url);
        //    var uri = new Uri(endpoint);
        //    ForceCanonicalPathAndQuery(uri);
        //    var request = WebRequest.Create(uri) as HttpWebRequest;

        //    if (request == null)
        //    {
        //    }

        //    request.Method = "GET";
        //    request.ContentType = "application/json";
        //    request.Credentials = new NetworkCredential(Username, Password);

        //    return request;
        //}

        //private HttpWebRequest BuildHttpGetRequest(string url)
        //{
        //    var endpoint = BuildRestEndpoint(url);
        //    var uri = new Uri(endpoint);
        //    ForceCanonicalPathAndQuery(uri);
        //    var request = WebRequest.Create(uri) as HttpWebRequest;

        //    if (request == null)
        //    {
        //    }

        //    request.Method = "GET";
        //    request.ContentType = "application/json";
        //    request.Credentials = new NetworkCredential(Username, Password);

        //    return request;
        //}

        //private HttpWebRequest BuildHttpPutRequest(string url, long contentLength)
        //{
        //    var endpoint = BuildRestEndpoint(url);
        //    var uri = new Uri(endpoint);
        //    ForceCanonicalPathAndQuery(uri);
        //    var request = WebRequest.Create(uri) as HttpWebRequest;

        //    if (request == null)
        //    {
        //    }

        //    request.Method = "PUT";
        //    request.ContentType = "application/json";
        //    request.Credentials = new NetworkCredential(Username, Password);
        //    request.ContentLength = contentLength;

        //    return request;
        //}

        //private HttpWebRequest BuildHttpPostRequest(string url, long contentLength)
        //{
        //    var endpoint = BuildRestEndpoint(url);
        //    var uri = new Uri(endpoint);
        //    ForceCanonicalPathAndQuery(uri);
        //    var request = WebRequest.Create(uri) as HttpWebRequest;

        //    if (request == null)
        //    {
        //    }

        //    request.Method = "POST";
        //    request.ContentType = "application/json";
        //    request.Credentials = new NetworkCredential(Username, Password);
        //    request.ContentLength = contentLength;

        //    return request;
        //}

        //private HttpWebRequest BuildHttpDeleteRequest(string url)
        //{
        //    var endpoint = BuildRestEndpoint(url);
        //    var uri = new Uri(endpoint);
        //    ForceCanonicalPathAndQuery(uri);
        //    var request = WebRequest.Create(uri) as HttpWebRequest;

        //    if (request == null)
        //    {
        //    }

        //    request.Method = "DELETE";
        //    request.ContentType = "application/json";
        //    request.Credentials = new NetworkCredential(Username, Password);

        //    return request;
        //}

        //private string GetHttpResponseBody(HttpWebRequest request)
        //{
        //    var response = request.GetResponse() as HttpWebResponse;
        //    using (Stream responseStream = response.GetResponseStream())
        //        using (var reader = new StreamReader(responseStream))
        //        {
        //            return reader.ReadToEnd();
        //        }
        //}

        /// <summary>
        /// this method is to add workaound for isssue using forword shlash ('/') in uri
        /// default vhost in RabbitMQ is named as '/' but '/' is uri charter so RabbitMQ suggest to uses '%2f' encoded character while passing default host name in URI
        /// but System.URI class replaces this encoded char with '/' which changes symantics fo URI. 
        /// This method is to overide the default System.Uri behaviour 
        /// </summary>
        private void LeaveDotsAndSlashesEscaped()
        {
            var getSyntaxMethod =
                typeof(UriParser).GetMethod("GetSyntax", BindingFlags.Static | BindingFlags.NonPublic);
            if (getSyntaxMethod == null)
            {
                throw new MissingMethodException("UriParser", "GetSyntax");
            }

            var uriParser = getSyntaxMethod.Invoke(null, new object[] { "http" });

            var setUpdatableFlagsMethod =
                uriParser.GetType().GetMethod("SetUpdatableFlags", BindingFlags.Instance | BindingFlags.NonPublic);
            if (setUpdatableFlagsMethod == null)
            {
                throw new MissingMethodException("UriParser", "SetUpdatableFlags");
            }

            setUpdatableFlagsMethod.Invoke(uriParser, new object[] { 0 });
        }
        private T Get<T>(string path)
        protected T Get<T>(string path)

        private T Get<T>(string path)
        {
            //if defaut vhost name is pressetnt then use LeaveDotsAndSlashesEscaped to prevent encoded charater getting overwrittern
            if (path.Contains("/%2f"))
                LeaveDotsAndSlashesEscaped();

            var response = Client.GetAsync(path).Result;
            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<T>().Result;
        }

        protected void Delete(string url)
        {
            var response = Client.DeleteAsync(url).Result;
            response.EnsureSuccessStatusCode();
        }

        protected void Put<T>(string url, T value)
        {
            var response = Client.PutAsJsonAsync(url, value).Result;
            response.EnsureSuccessStatusCode();
        }

        protected void Post<T>(string url, T value)
        {
            var response = Client.PostAsJsonAsync(url, value).Result;
            response.EnsureSuccessStatusCode();
        }

        public IEnumerable<string> GetListOfVirtualHosts()
        {
            //var request = BuildHttpGetRequest("vhosts");
            //string response = GetHttpResponseBody(request);
            //var parser = JArray.Parse(response);

            //return from x in parser.Children()["name"]
            //       select x.Value<string>();
            return Get<IEnumerable<string>>("vhosts");
            //return queues.Where(x => x.VirtualHostName == vhost).Select(x => x.Name);
        }

        public IEnumerable<string> GetListOfAllQueuesInVirtualHost(string vhost)
        {
            var queues = Get<IEnumerable<Queue>>(string.Format(@"api/queues/{0}", vhost.SanitizeVirtualHostName()));
            return queues.Where(x => x.VirtualHostName == vhost).Select(x => x.Name);
        }

        public IEnumerable<Queue> GetListOfAllQueues()
        {
            return Get<IEnumerable<Queue>>("api/queues");
        }

        public IEnumerable<Exchange> GetListOfAllExchanges()
        {
            return Get<IEnumerable<Exchange>>("api/exchanges");
        }

        public IEnumerable<Connection> GetListOfAllOpenConnections()
        {
            return Get<IEnumerable<Connection>>("api/connections");
        }

        public IEnumerable<Channel> GetListOfAllOpenChannels()
        {
            return Get<IEnumerable<Channel>>("api/channels");
        }

        public IEnumerable<Binding> GetListOfAllBindingsOnQueue(string virtualHostName, string queueName)
        {
            return Get<IEnumerable<QueueBinding>>(string.Format("api/queues/{0}/{1}/bindings", virtualHostName.SanitizeVirtualHostName(), queueName));
            return
                Get<IEnumerable<QueueBinding>>(string.Format("api/queues/{0}/{1}/bindings",
                                                             virtualHostName.SanitizeVirtualHostName(), queueName));
            return Get<IEnumerable<Binding>>(string.Format("api/queues/{0}/{1}/bindings", virtualHostName.SanitizeVirtualHostName(), queueName));
        }

        public void CreateQueue(QueueRequestOperationParams queue)
        {
            Put(string.Format("api/queues/{0}/{1}", queue.VirtualHostName.SanitizeVirtualHostName(), queue.QueueName),
                queue);
        }

        public void CreateExchange(ExchangePutRequestParams exchange)
        {
            Put(string.Format("api/exchanges/{0}/{1}", exchange.VirtualHostName.SanitizeVirtualHostName(),
                              exchange.ExchangeName), exchange);
        }

        private bool Put<T>(string path, T value)
        {
           // var uri = new Uri(string.Format("{0}/{1}", Client.BaseAddress.PathAndQuery, path));
            var uri = new Uri(string.Format("http://localhost/{0}", path));
            var response = Client.PutAsJsonAsync(uri.PathAndQuery, value).Result;
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }


        public void CreateExchange(ExchangePutRequestParams exchange)
        {
            Put(string.Format("api/exchanges/{0}/{1}", exchange.VirtualHostName.SanitizeVirtualHostName(),
                              exchange.ExchangeName), exchange);
        }

        
        private bool Put<T>(string path, T value)
        {
            if (path.Contains("/%2f"))
                LeaveDotsAndSlashesEscaped();
           // var uri = new Uri(string.Format("{0}/{1}", Client.BaseAddress.PathAndQuery, path));
            var uri = new Uri(string.Format("http://localhost/{0}", path));
            var response = Client.PutAsJsonAsync(uri.PathAndQuery, value).Result;
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }

        public void CreateQueueBindings(QueueBindingsPostRequestParams queueBinding)
        {
            queueBinding.RoutingKey = queueBinding.RoutingKey ?? string.Empty;
            Put(
                string.Format("api/bindings/{0}/e/{1}/q/{2}", queueBinding.VirtualHostName.SanitizeVirtualHostName(),
                              queueBinding.ExchangeName, queueBinding.QueueName), queueBinding);
        }

        public void DeleteQueue(QueueRequestOperationParams queue)
        {
            Delete(string.Format("api/queues/{0}/{1}", queue.VirtualHostName.SanitizeVirtualHostName(), queue.QueueName));
        }

        private bool Delete(string url)
        {
            var response = Client.DeleteAsync(url).Result;
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }

        private bool Delete(string url)
        {
            if (url.Contains("/%2f"))
                LeaveDotsAndSlashesEscaped();

            var response = Client.DeleteAsync(url).Result;
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }

        #region exchanges

        public IEnumerable<Exchange> GetListOfAllExchangesInVirtualHost(string vhost)
        {
            return Get<IEnumerable<Exchange>>(string.Format(@"api/exchanges/{0}", vhost.SanitizeVirtualHostName()));
        }

        public Exchange GetExchange(string vhost, string exchangeName)
        {
            return Get<Exchange>(string.Format(@"api/exchanges/{0}/{1}", vhost.SanitizeVirtualHostName(), exchangeName));
        }
       
        public void CreateExchange(ExchangePutRequestParams exchange)
        {
            Put(string.Format("api/exchanges/{0}/{1}", exchange.VirtualHostName.SanitizeVirtualHostName(),
                              exchange.ExchangeName), exchange);
        }

        public void DeleteExchange(string vhost, string exchangeName)
        {
            Delete(string.Format("api/exchanges/{0}/{1}", vhost.SanitizeVirtualHostName(), exchangeName));
        }

        public IEnumerable<Binding> GetListOfAllBindingsOnExchange(string vhost, string exchangeName, bool exchangeAsSource)
        {
            string uri = string.Format(@"api/exchanges/{0}/{1}/bindings/", vhost.SanitizeVirtualHostName(), exchangeName);
            if (exchangeAsSource)
                uri += "source";
            else
                uri += "destination";

            return Get<IEnumerable<Binding>>(uri);
        }

        #endregion
    }
}
