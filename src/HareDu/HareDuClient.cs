namespace HareDu
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using Model;

    public class HareDuClient :
        HareDuBase
    {
        public HareDuClient(string hostUrl, int port, string username, string password) :
            base(hostUrl, port, username, password)
        {
        }

        public Task<HttpResponseMessage> GetListOfVirtualHosts()
        {
            //var request = BuildHttpGetRequest("vhosts");
            //string response = GetHttpResponseBody(request);
            //var parser = JArray.Parse(response);

            //return from x in parser.Children()["name"]
            //       select x.Value<string>();
            return Get("vhosts");
            //return queues.Where(x => x.VirtualHostName == virtualHostName).Select(x => x.Name);
        }

        public Task<HttpResponseMessage> GetListOfAllOpenConnections()
        {
            return Get("api/connections");
        }

        public Task<HttpResponseMessage> GetListOfAllOpenChannels()
        {
            return Get("api/channels");
        }

        #region Virtual Hosts

        public Task<HttpResponseMessage> CreateVirtualHost(string virtualHostName,
                                                           CancellationToken cancellationToken =
                                                               default(CancellationToken))
        {
            if (string.IsNullOrEmpty(virtualHostName) || string.IsNullOrWhiteSpace(virtualHostName))
                throw new ArgumentNullException("virtualHostName");

            string url = string.Format("api/vhosts/{0}", virtualHostName.SanitizeVirtualHostName());

            return cancellationToken == default(CancellationToken)
                       ? Put(url, new StringContent(string.Empty))
                       : Put(url, new StringContent(string.Empty), cancellationToken);
        }

        public Task<HttpResponseMessage> DeleteVirtualHost(string virtualHostName, CancellationToken cancellationToken =
                                                                                       default(CancellationToken))
        {
            if (string.IsNullOrEmpty(virtualHostName) || string.IsNullOrWhiteSpace(virtualHostName))
                throw new ArgumentNullException("virtualHostName");

            if (virtualHostName.SanitizeVirtualHostName() == "2%f")
            {
            }

            string url = string.Format("api/vhosts/{0}", virtualHostName.SanitizeVirtualHostName());

            return cancellationToken == default(CancellationToken) ? Delete(url) : Delete(url, cancellationToken);
        }

        #endregion

        #region Queues

        public virtual Task<HttpResponseMessage> GetListOfAllQueues(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return cancellationToken == default(CancellationToken)
                       ? Get("api/queues")
                       : Get("api/queues", cancellationToken);
        }

        public virtual Task<HttpResponseMessage> GetListOfAllBindingsOnQueue(string virtualHostName, string queueName,
                                                                             CancellationToken cancellationToken =
                                                                                 default(CancellationToken))
        {
            string url = string.Format("api/queues/{0}/{1}/bindings", virtualHostName.SanitizeVirtualHostName(),
                                       queueName);

            return cancellationToken == default(CancellationToken) ? Get(url) : Get(url, cancellationToken);
        }

        public virtual Task<HttpResponseMessage> CreateQueue(string virtualHostName, string queueName,
                                                             Action<CreateQueueArgs> args,
                                                             CancellationToken cancellationToken =
                                                                 default(CancellationToken))
        {
            if (string.IsNullOrEmpty(queueName) || string.IsNullOrWhiteSpace(queueName))
                throw new ArgumentNullException("queueName");

            if (string.IsNullOrEmpty(virtualHostName) || string.IsNullOrWhiteSpace(virtualHostName))
                throw new ArgumentNullException("virtualHostName");

            if (args == null)
                throw new ArgumentNullException("args");

            var queue = new CreateQueueArgsImpl();
            args(queue);

            string url = string.Format("api/queues/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), queueName);

            return cancellationToken == default(CancellationToken)
                       ? Put(url, queue)
                       : Put(url, queue, cancellationToken);
        }

        public virtual Task<HttpResponseMessage> CreateQueue(string virtualHostName, string node, string queueName,
                                                             Action<CreateQueueArgs> args,
                                                             CancellationToken cancellationToken =
                                                                 default(CancellationToken))
        {
            if (string.IsNullOrEmpty(queueName) || string.IsNullOrWhiteSpace(queueName))
                throw new ArgumentNullException("queueName");

            if (string.IsNullOrEmpty(virtualHostName) || string.IsNullOrWhiteSpace(virtualHostName))
                throw new ArgumentNullException("virtualHostName");

            if (string.IsNullOrEmpty(node) || string.IsNullOrWhiteSpace(node))
                throw new ArgumentNullException("node");

            var queue = new CreateQueueArgsImpl {Node = node};
            if (args != null)
                args(queue);

            string url = string.Format("api/queues/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), queueName);

            return cancellationToken == default(CancellationToken)
                       ? Put(url, queue)
                       : Put(url, queue, cancellationToken);
        }

        public virtual Task<HttpResponseMessage> BindQueueToExchange(string virtualHostName, string exchangeName,
                                                                     string queueName,
                                                                     Action<BindQueueArgs> args = null,
                                                                     CancellationToken cancellationToken =
                                                                         default(CancellationToken))
        {
            if (string.IsNullOrEmpty(queueName) || string.IsNullOrWhiteSpace(queueName))
                throw new ArgumentNullException("exchangeName");

            if (string.IsNullOrEmpty(exchangeName) || string.IsNullOrWhiteSpace(exchangeName))
                throw new ArgumentNullException("queueName");

            if (string.IsNullOrEmpty(virtualHostName) || string.IsNullOrWhiteSpace(virtualHostName))
                throw new ArgumentNullException("virtualHostName");

            var queueBinding = new BindQueueArgsImpl();
            if (args != null)
                args(queueBinding);

            string url = string.Format("api/bindings/{0}/e/{1}/q/{2}", virtualHostName.SanitizeVirtualHostName(),
                                       exchangeName, queueName);

            return cancellationToken == default(CancellationToken)
                       ? Post(url, queueBinding)
                       : Post(url, queueBinding, cancellationToken);
        }

        public virtual Task<HttpResponseMessage> DeleteQueue(string virtualHostName, string queueName,
                                                             CancellationToken cancellationToken =
                                                                 default(CancellationToken))
        {
            if (string.IsNullOrEmpty(queueName) || string.IsNullOrWhiteSpace(queueName))
                throw new ArgumentNullException("queueName");

            if (string.IsNullOrEmpty(virtualHostName) || string.IsNullOrWhiteSpace(virtualHostName))
                throw new ArgumentNullException("virtualHostName");

            string url = string.Format("api/queues/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), queueName);

            return cancellationToken == default(CancellationToken) ? Delete(url) : Delete(url, cancellationToken);
        }

        #endregion

        #region Exchanges

        public Task<HttpResponseMessage> GetListOfAllExchanges(CancellationToken cancellationToken =
                                                                   default(CancellationToken))
        {
            return cancellationToken == default(CancellationToken)
                       ? Get("api/exchanges")
                       : Get("api/exchanges", cancellationToken);
        }

        public Task<HttpResponseMessage> GetListOfAllExchangesInVirtualHost(string virtualHostName,
                                                                            CancellationToken cancellationToken =
                                                                                default(CancellationToken))
        {
            string url = string.Format(@"api/exchanges/{0}", virtualHostName.SanitizeVirtualHostName());

            return cancellationToken == default(CancellationToken) ? Get(url) : Get(url, cancellationToken);
        }

        public Task<HttpResponseMessage> GetExchange(string virtualHostName, string exchangeName,
                                                     CancellationToken cancellationToken =
                                                         default(CancellationToken))
        {
            string url = string.Format(@"api/exchanges/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), exchangeName);

            return cancellationToken == default(CancellationToken) ? Get(url) : Get(url, cancellationToken);
        }

        public Task<HttpResponseMessage> GetListOfAllBindingsOnExchange(string virtualHostName, string exchangeName,
                                                                        bool exchangeAsSource,
                                                                        CancellationToken cancellationToken =
                                                                            default(CancellationToken))
        {
            string url = string.Format("api/exchanges/{0}/{1}/bindings/{2}",
                                       virtualHostName.SanitizeVirtualHostName(), exchangeName,
                                       exchangeAsSource ? "source" : "destination");

            return cancellationToken == default(CancellationToken) ? Get(url) : Get(url, cancellationToken);
        }

        public Task<HttpResponseMessage> CreateExchange(string virtualHostName, string exchangeName,
                                                        Action<CreateExchangeArgs> args = null,
                                                        CancellationToken cancellationToken =
                                                            default(CancellationToken))
        {
            if (string.IsNullOrEmpty(exchangeName) || string.IsNullOrWhiteSpace(exchangeName))
                throw new ArgumentNullException("exchangeName");

            if (string.IsNullOrEmpty(virtualHostName) || string.IsNullOrWhiteSpace(virtualHostName))
                throw new ArgumentNullException("virtualHostName");

            var exchange = new CreateExchangeArgsImpl();
            if (args != null)
                args(exchange);

            string url = string.Format("api/exchanges/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), exchangeName);

            return cancellationToken == default(CancellationToken)
                       ? Put(url, exchange)
                       : Put(url, exchange, cancellationToken);
        }

        public Task<HttpResponseMessage> DeleteExchange(string virtualHostName, string exchangeName,
                                                        CancellationToken cancellationToken =
                                                            default(CancellationToken))
        {
            if (string.IsNullOrEmpty(exchangeName) || string.IsNullOrWhiteSpace(exchangeName))
                throw new ArgumentNullException("exchangeName");

            if (string.IsNullOrEmpty(virtualHostName) || string.IsNullOrWhiteSpace(virtualHostName))
                throw new ArgumentNullException("virtualHostName");

            string url = string.Format("api/exchanges/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), exchangeName);

            return cancellationToken == default(CancellationToken) ? Delete(url) : Delete(url, cancellationToken);
        }

        #endregion
    }
}