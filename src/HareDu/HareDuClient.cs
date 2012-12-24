namespace HareDu
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using Internal;

    public class HareDuClient :
        HareDuBase
    {
        public HareDuClient(string hostUrl, int port, string username, string password) :
            base(hostUrl, port, username, password)
        {
        }

        #region api/overview

        public Task<HttpResponseMessage> Overview()
        {
            return Get("api/overview");
        }

        #endregion

        #region api/whoami

        public Task<HttpResponseMessage> WhoAmI()
        {
            return Get("api/whoami");
        }

        #endregion

        #region Permissions

        public Task<HttpResponseMessage> Permissions()
        {
            return Get("api/permissions");
        }

        #endregion

        #region Connectivity

        //old
        public Task<HttpResponseMessage> GetListOfAllOpenConnections()
        {
            return Get("api/connections");
        }

        public Task<HttpResponseMessage> GetListOfAllOpenConnections(CancellationToken cancellationToken =
                                                                         default(CancellationToken))
        {
            return cancellationToken == default(CancellationToken)
                       ? Get("api/connections")
                       : Get("api/connections", cancellationToken);
        }

        public Task<HttpResponseMessage> GetListOfAllOpenChannels(CancellationToken cancellationToken =
                                                                      default(CancellationToken))
        {
            return cancellationToken == default(CancellationToken)
                       ? Get("api/channels")
                       : Get("api/channels", cancellationToken);
        }

        public virtual Task<HttpResponseMessage> SendEKG(string virtualHostName, CancellationToken cancellationToken =
                                                                                     default(CancellationToken))
        {
            virtualHostName.CheckIfArgValid("virtualHostName");

            string url = string.Format("api/aliveness-test/{0}", virtualHostName);

            return cancellationToken == default(CancellationToken) ? Get(url) : Get(url, cancellationToken);
        }

        #endregion

        #region Permissions

        public virtual Task<HttpResponseMessage> GetUserPermissions(string virtualHostName, string userName,
                                                                    CancellationToken cancellationToken =
                                                                        default(CancellationToken))
        {
            virtualHostName.CheckIfArgValid("virtualHostName");
            userName.CheckIfArgValid("userName");

            string url = string.Format("api/permissions/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), userName);

            return cancellationToken == default(CancellationToken) ? Get(url) : Get(url, cancellationToken);
        }

        public virtual Task<HttpResponseMessage> CreateUserPermissions(string virtualHostName, string userName,
                                                                       Action<UserPermissionsArgs> args,
                                                                       CancellationToken cancellationToken =
                                                                           default(CancellationToken))
        {
            virtualHostName.CheckIfArgValid("virtualHostName");
            userName.CheckIfArgValid("userName");
            args.CheckIfArgValid("args");

            var permissions = new UserPermissionsArgsImpl();
            args(permissions);
            string url = string.Format("api/permissions/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), userName);

            return cancellationToken == default(CancellationToken)
                       ? Put(url, permissions)
                       : Put(url, permissions, cancellationToken);
        }

        public virtual Task<HttpResponseMessage> DeleteUserPermissions(string virtualHostName, string userName,
                                                                       CancellationToken cancellationToken =
                                                                           default(CancellationToken))
        {
            virtualHostName.CheckIfArgValid("virtualHostName");
            userName.CheckIfArgValid("userName");

            string url = string.Format("api/permissions/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), userName);

            return cancellationToken == default(CancellationToken) ? Delete(url) : Delete(url, cancellationToken);
        }

        #endregion

        #region Users

        public virtual Task<HttpResponseMessage> GetUsers(CancellationToken cancellationToken =
                                                              default(CancellationToken))
        {
            return cancellationToken == default(CancellationToken)
                       ? Get("api/users")
                       : Get("api/users", cancellationToken);
        }

        public virtual Task<HttpResponseMessage> GetUser(string userName, CancellationToken cancellationToken =
                                                                              default(CancellationToken))
        {
            userName.CheckIfArgValid("userName");

            string url = string.Format("api/users/{0}", userName);

            return cancellationToken == default(CancellationToken) ? Get(url) : Get(url, cancellationToken);
        }

        public virtual Task<HttpResponseMessage> CreateUser(string userName, Action<UserArgs> args,
                                                            CancellationToken cancellationToken =
                                                                default(CancellationToken))
        {
            userName.CheckIfArgValid("userName");
            args.CheckIfArgValid("args");

            var user = new UserArgsImpl();
            args(user);
            string url = string.Format("api/users/{0}", userName);

            return cancellationToken == default(CancellationToken) ? Put(url, user) : Put(url, user, cancellationToken);
        }

        public virtual Task<HttpResponseMessage> DeleteUser(string userName, CancellationToken cancellationToken =
                                                                                 default(CancellationToken))
        {
            userName.CheckIfArgValid("userName");

            string url = string.Format("api/users/{0}", userName);

            return cancellationToken == default(CancellationToken) ? Delete(url) : Delete(url, cancellationToken);
        }

        #endregion

        #region Virtual Hosts

        public Task<HttpResponseMessage> GetListOfVirtualHosts(CancellationToken cancellationToken =
                                                                   default(CancellationToken))
        {
            return cancellationToken == default(CancellationToken)
                       ? Get("api/vhosts")
                       : Get("api/vhosts", cancellationToken);
        }

        public virtual Task<HttpResponseMessage> CreateVirtualHost(string virtualHostName,
                                                                   CancellationToken cancellationToken =
                                                                       default(CancellationToken))
        {
            virtualHostName.CheckIfArgValid("virtualHostName");

            string url = string.Format("api/vhosts/{0}", virtualHostName.SanitizeVirtualHostName());

            return cancellationToken == default(CancellationToken)
                       ? Put(url, new StringContent(string.Empty))
                       : Put(url, new StringContent(string.Empty), cancellationToken);
        }

        public virtual Task<HttpResponseMessage> DeleteVirtualHost(string virtualHostName,
                                                                   CancellationToken cancellationToken =
                                                                       default(CancellationToken))
        {
            virtualHostName.CheckIfArgValid("virtualHostName");

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
            virtualHostName.CheckIfArgValid("virtualHostName");
            queueName.CheckIfArgValid("queueName");

            string url = string.Format("api/queues/{0}/{1}/bindings", virtualHostName.SanitizeVirtualHostName(),
                                       queueName);

            return cancellationToken == default(CancellationToken) ? Get(url) : Get(url, cancellationToken);
        }

        public virtual Task<HttpResponseMessage> CreateQueue(string virtualHostName, string queueName,
                                                             Action<CreateQueueArgs> args,
                                                             CancellationToken cancellationToken =
                                                                 default(CancellationToken))
        {
            queueName.CheckIfArgValid("queueName");
            virtualHostName.CheckIfArgValid("virtualHostName");
            args.CheckIfArgValid("args");

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
            queueName.CheckIfArgValid("queueName");
            virtualHostName.CheckIfArgValid("virtualHostName");
            node.CheckIfArgValid("node");
            args.CheckIfArgValid("args");

            var queue = new CreateQueueArgsImpl {Node = node};
            args(queue);
            string url = string.Format("api/queues/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), queueName);

            return cancellationToken == default(CancellationToken)
                       ? Put(url, queue)
                       : Put(url, queue, cancellationToken);
        }

        public virtual Task<HttpResponseMessage> BindQueueToExchange(string virtualHostName, string exchangeName,
                                                                     string queueName,
                                                                     Action<BindQueueArgs> args,
                                                                     CancellationToken cancellationToken =
                                                                         default(CancellationToken))
        {
            queueName.CheckIfArgValid("queueName");
            virtualHostName.CheckIfArgValid("virtualHostName");
            exchangeName.CheckIfArgValid("exchangeName");
            args.CheckIfArgValid("args");

            var queueBinding = new BindQueueArgsImpl();
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
            queueName.CheckIfArgValid("queueName");
            virtualHostName.CheckIfArgValid("virtualHostName");

            string url = string.Format("api/queues/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), queueName);

            return cancellationToken == default(CancellationToken) ? Delete(url) : Delete(url, cancellationToken);
        }

#endregion

        #region Exchanges

        public virtual Task<HttpResponseMessage> GetListOfAllExchanges(CancellationToken cancellationToken =
                                                                           default(CancellationToken))
        {
            return cancellationToken == default(CancellationToken)
                       ? Get("api/exchanges")
                       : Get("api/exchanges", cancellationToken);
        }

        public virtual Task<HttpResponseMessage> GetListOfAllExchangesInVirtualHost(string virtualHostName,
                                                                                    CancellationToken cancellationToken
                                                                                        =
                                                                                        default(CancellationToken))
        {
            virtualHostName.CheckIfArgValid("virtualHostName");

            string url = string.Format(@"api/exchanges/{0}", virtualHostName.SanitizeVirtualHostName());

            return cancellationToken == default(CancellationToken) ? Get(url) : Get(url, cancellationToken);
        }

        public virtual Task<HttpResponseMessage> GetExchange(string virtualHostName, string exchangeName,
                                                             CancellationToken cancellationToken =
                                                                 default(CancellationToken))
        {
            virtualHostName.CheckIfArgValid("virtualHostName");
            exchangeName.CheckIfArgValid("exchangeName");

            string url = string.Format(@"api/exchanges/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), exchangeName);

            return cancellationToken == default(CancellationToken) ? Get(url) : Get(url, cancellationToken);
        }

        public virtual Task<HttpResponseMessage> GetListOfAllBindingsOnExchange(string virtualHostName,
                                                                                string exchangeName,
                                                                                bool exchangeAsSource,
                                                                                CancellationToken cancellationToken =
                                                                                    default(CancellationToken))
        {
            virtualHostName.CheckIfArgValid("virtualHostName");
            exchangeName.CheckIfArgValid("exchangeName");

            string url = string.Format("api/exchanges/{0}/{1}/bindings/{2}",
                                       virtualHostName.SanitizeVirtualHostName(), exchangeName,
                                       exchangeAsSource ? "source" : "destination");

            return cancellationToken == default(CancellationToken) ? Get(url) : Get(url, cancellationToken);
        }

        public virtual Task<HttpResponseMessage> CreateExchange(string virtualHostName, string exchangeName,
                                                                Action<CreateExchangeArgs> args = null,
                                                                CancellationToken cancellationToken =
                                                                    default(CancellationToken))
        {
            virtualHostName.CheckIfArgValid("virtualHostName");
            exchangeName.CheckIfArgValid("exchangeName");

            var exchange = new CreateExchangeArgsImpl();
            if (args != null)
                args(exchange);
            string url = string.Format("api/exchanges/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), exchangeName);

            return cancellationToken == default(CancellationToken)
                       ? Put(url, exchange)
                       : Put(url, exchange, cancellationToken);
        }

        public virtual Task<HttpResponseMessage> DeleteExchange(string virtualHostName, string exchangeName,
                                                                CancellationToken cancellationToken =
                                                                    default(CancellationToken))
        {
            virtualHostName.CheckIfArgValid("virtualHostName");
            exchangeName.CheckIfArgValid("exchangeName");

            string url = string.Format("api/exchanges/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), exchangeName);

            return cancellationToken == default(CancellationToken) ? Delete(url) : Delete(url, cancellationToken);
        }

        #endregion
    }
}