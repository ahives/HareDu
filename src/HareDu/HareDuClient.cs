namespace HareDu
{
    using System;
    using System.Net.Http;
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

        #region Connections

        public Task<HttpResponseMessage> GetListOfAllOpenConnections()
        {
            return Get("api/connections");
        }

        #endregion


        #region Channels

        public Task<HttpResponseMessage> GetListOfAllOpenChannels()
        {
            return Get("api/channels");
        }

        #endregion


        #region Virtual Hosts

        public Task<HttpResponseMessage> GetListOfVirtualHosts()
        {
            return Get("api/vhosts");
        }

        public Task<HttpResponseMessage> CreateVirtualHost(string virtualHostName)
        {
            if (string.IsNullOrEmpty(virtualHostName) || string.IsNullOrWhiteSpace(virtualHostName))
                throw new ArgumentNullException("virtualHostName");

            return Put(string.Format("api/vhosts/{0}", virtualHostName.SanitizeVirtualHostName()),
                       new StringContent(string.Empty));
        }

        public Task<HttpResponseMessage> DeleteVirtualHost(string virtualHostName)
        {
            if (string.IsNullOrEmpty(virtualHostName) || string.IsNullOrWhiteSpace(virtualHostName))
                throw new ArgumentNullException("virtualHostName");

            if (virtualHostName.SanitizeVirtualHostName() == "2%f")
            {
            }

            return Delete(string.Format("api/vhosts/{0}", virtualHostName.SanitizeVirtualHostName()));
        }

        #endregion

        #region Queues

        public Task<HttpResponseMessage> GetListOfAllQueues()
        {
            return Get("api/queues");
        }

        public Task<HttpResponseMessage> GetListOfAllBindingsOnQueue(string virtualHostName, string queueName)
        {
            return
                Get(string.Format("api/queues/{0}/{1}/bindings", virtualHostName.SanitizeVirtualHostName(), queueName));
        }

        public Task<HttpResponseMessage> CreateQueue(string virtualHostName, string queueName,
                                                     Action<CreateQueueCmd> cmdParams)
        {
            if (string.IsNullOrEmpty(queueName) || string.IsNullOrWhiteSpace(queueName))
                throw new ArgumentNullException("queueName");

            if (string.IsNullOrEmpty(virtualHostName) || string.IsNullOrWhiteSpace(virtualHostName))
                throw new ArgumentNullException("virtualHostName");

            var queue = new CreateQueueCmdImpl();
            cmdParams(queue);

            return Put(string.Format("api/queues/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), queueName), queue);
        }

        public Task<HttpResponseMessage> CreateQueue(string virtualHostName, string node, string queueName,
                                                     Action<CreateQueueCmd> cmdParams)
        {
            if (string.IsNullOrEmpty(queueName) || string.IsNullOrWhiteSpace(queueName))
                throw new ArgumentNullException("queueName");

            if (string.IsNullOrEmpty(virtualHostName) || string.IsNullOrWhiteSpace(virtualHostName))
                throw new ArgumentNullException("virtualHostName");

            if (string.IsNullOrEmpty(node) || string.IsNullOrWhiteSpace(node))
                throw new ArgumentNullException("node");

            var queue = new CreateQueueCmdImpl {Node = node};
            cmdParams(queue);

            return Put(string.Format("api/queues/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), queueName), queue);
        }

        public Task<HttpResponseMessage> BindQueueToExchange(string virtualHostName, string exchangeName,
                                                             string queueName,
                                                             Action<BindQueueCmd> cmdParams)
        {
            if (string.IsNullOrEmpty(queueName) || string.IsNullOrWhiteSpace(queueName))
                throw new ArgumentNullException("exchangeName");

            if (string.IsNullOrEmpty(exchangeName) || string.IsNullOrWhiteSpace(exchangeName))
                throw new ArgumentNullException("queueName");

            if (string.IsNullOrEmpty(virtualHostName) || string.IsNullOrWhiteSpace(virtualHostName))
                throw new ArgumentNullException("virtualHostName");

            var queueBinding = new BindQueueCmdImpl();
            cmdParams(queueBinding);

            return Post(
                string.Format("api/bindings/{0}/e/{1}/q/{2}", virtualHostName.SanitizeVirtualHostName(),
                              exchangeName, queueName), queueBinding);
        }

        public Task<HttpResponseMessage> BindQueueToExchange(string virtualHostName, string exchangeName,
                                                             string queueName)
        {
            if (string.IsNullOrEmpty(queueName) || string.IsNullOrWhiteSpace(queueName))
                throw new ArgumentNullException("queueName");

            if (string.IsNullOrEmpty(exchangeName) || string.IsNullOrWhiteSpace(exchangeName))
                throw new ArgumentNullException("exchangeName");

            if (string.IsNullOrEmpty(virtualHostName) || string.IsNullOrWhiteSpace(virtualHostName))
                throw new ArgumentNullException("virtualHostName");

            var queueBinding = new BindQueueCmdImpl();

            return Post(
                string.Format("api/bindings/{0}/e/{1}/q/{2}", virtualHostName.SanitizeVirtualHostName(),
                              exchangeName, queueName), queueBinding);
        }

        public Task<HttpResponseMessage> DeleteQueue(string virtualHostName, string queueName)
        {
            if (string.IsNullOrEmpty(queueName) || string.IsNullOrWhiteSpace(queueName))
                throw new ArgumentNullException("queueName");

            if (string.IsNullOrEmpty(virtualHostName) || string.IsNullOrWhiteSpace(virtualHostName))
                throw new ArgumentNullException("virtualHostName");

            return Delete(string.Format("api/queues/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), queueName));
        }

        #endregion

        #region Exchanges

        public Task<HttpResponseMessage> GetListOfAllExchanges()
        {
            return Get("api/exchanges");
        }

        public Task<HttpResponseMessage> GetListOfAllExchangesInVirtualHost(string vhost)
        {
            return Get(string.Format(@"api/exchanges/{0}", vhost.SanitizeVirtualHostName()));
        }

        public Task<HttpResponseMessage> GetExchange(string vhost, string exchangeName)
        {
            return Get(string.Format(@"api/exchanges/{0}/{1}", vhost.SanitizeVirtualHostName(), exchangeName));
        }

        public Task<HttpResponseMessage> GetListOfAllBindingsOnExchange(string virtualHostName, string exchangeName,
                                                                        bool exchangeAsSource)
        {
            return
                Get(string.Format("api/exchanges/{0}/{1}/bindings/{2}",
                                  virtualHostName.SanitizeVirtualHostName(), exchangeName,
                                  exchangeAsSource ? "source" : "destination"));
        }

        public Task<HttpResponseMessage> CreateExchange(string virtualHostName, string exchangeName,
                                                        Action<CreateExchangeCmd> cmdParams)
        {
            if (string.IsNullOrEmpty(exchangeName) || string.IsNullOrWhiteSpace(exchangeName))
                throw new ArgumentNullException("exchangeName");

            if (string.IsNullOrEmpty(virtualHostName) || string.IsNullOrWhiteSpace(virtualHostName))
                throw new ArgumentNullException("virtualHostName");

            var exchange = new CreateExchangeCmdImpl();
            cmdParams(exchange);

            return Put(string.Format("api/exchanges/{0}/{1}", virtualHostName.SanitizeVirtualHostName(),
                                     exchangeName), exchange);
        }

        public Task<HttpResponseMessage> DeleteExchange(string virtualHostName, string exchangeName)
        {
            if (string.IsNullOrEmpty(exchangeName) || string.IsNullOrWhiteSpace(exchangeName))
                throw new ArgumentNullException("exchangeName");

            if (string.IsNullOrEmpty(virtualHostName) || string.IsNullOrWhiteSpace(virtualHostName))
                throw new ArgumentNullException("virtualHostName");

            return
                Delete(string.Format("api/exchanges/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), exchangeName));
        }

        #endregion
    }
}