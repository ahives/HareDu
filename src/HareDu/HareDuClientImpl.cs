// Copyright 2012-2013 Albert L. Hives, Chris Patterson, Rajesh Gande, et al.
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

namespace HareDu
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using Internal;

    internal class HareDuClientImpl :
        HareDuClientBase,
        HareDuClient
    {
        public HareDuClientImpl(ClientInitArgsImpl args) :
            base(args)
        {
        }

        #region Helpers

        private Task<HttpResponseMessage> CreateQueueHelper(string node, string virtualHostName, string queueName,
                                                            Action<CreateQueueArgs> args,
                                                            CancellationToken cancellationToken =
                                                                default(CancellationToken))
        {
            queueName.CheckIfArgValid("queueName");
            virtualHostName.CheckIfArgValid("virtualHostName");
            args.CheckIfArgValid("args");

            CreateQueueArgsImpl queue;
            if (string.IsNullOrEmpty(node) || string.IsNullOrWhiteSpace(node))
            {
                queue = new CreateQueueArgsImpl();
            }
            else
            {
                node.CheckIfArgValid("node");
                queue = new CreateQueueArgsImpl {Node = node};
            }

            args(queue);
            string url = string.Format("queues/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), queueName);

            return cancellationToken == default(CancellationToken)
                       ? Put(url, queue)
                       : Put(url, queue, cancellationToken);
        }

        #endregion

        #region HTTP Client Management

        public void CancelPendingRequests()
        {
            Client.CancelPendingRequests();
        }

        #endregion

        #region Connectivity

        public Task<HttpResponseMessage> GetAllConnections(CancellationToken cancellationToken =
                                                               default(CancellationToken))
        {
            return cancellationToken == default(CancellationToken)
                       ? Get("api/connections")
                       : Get("api/connections", cancellationToken);
        }

        public Task<HttpResponseMessage> GetConnection(string connectionName,
                                                       CancellationToken cancellationToken =
                                                           default(CancellationToken))
        {
            connectionName.CheckIfArgValid("connectionName");

            string url = string.Format("api/connections/{0}", connectionName);

            return cancellationToken == default(CancellationToken)
                       ? Get(url)
                       : Get(url, cancellationToken);
        }

        public Task<HttpResponseMessage> GetAllChannels(CancellationToken cancellationToken =
                                                            default(CancellationToken))
        {
            return cancellationToken == default(CancellationToken)
                       ? Get("api/channels")
                       : Get("api/channels", cancellationToken);
        }

        public Task<HttpResponseMessage> GetChannel(string channelName,
                                                    CancellationToken cancellationToken =
                                                        default(CancellationToken))
        {
            channelName.CheckIfArgValid("channelName");

            string url = string.Format("api/channels/{0}", channelName);

            return cancellationToken == default(CancellationToken)
                       ? Get(url)
                       : Get(url, cancellationToken);
        }

        public Task<HttpResponseMessage> ExecuteAlivenessTest(string virtualHostName,
                                                              CancellationToken cancellationToken =
                                                                  default(CancellationToken))
        {
            virtualHostName.CheckIfArgValid("virtualHostName");

            string url = string.Format("api/aliveness-test/{0}", virtualHostName);

            return cancellationToken == default(CancellationToken) ? Get(url) : Get(url, cancellationToken);
        }

        #endregion

        #region Permissions

        public Task<HttpResponseMessage> GetIndividualUserPermissions(string virtualHostName, string userName,
                                                                      CancellationToken cancellationToken =
                                                                          default(CancellationToken))
        {
            virtualHostName.CheckIfArgValid("virtualHostName");
            userName.CheckIfArgValid("userName");

            string url = string.Format("api/permissions/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), userName);

            return cancellationToken == default(CancellationToken) ? Get(url) : Get(url, cancellationToken);
        }

        public Task<HttpResponseMessage> GetAlllUserPermissions(
            CancellationToken cancellationToken = new CancellationToken())
        {
            return cancellationToken == default(CancellationToken)
                       ? Get("api/permissions")
                       : Get("api/permissions", cancellationToken);
        }

        public Task<HttpResponseMessage> CreateUserPermissions(string virtualHostName, string userName,
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

        public Task<HttpResponseMessage> DeleteUserPermissions(string virtualHostName, string userName,
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

        public Task<HttpResponseMessage> GetAllUsers(CancellationToken cancellationToken =
                                                         default(CancellationToken))
        {
            return cancellationToken == default(CancellationToken)
                       ? Get("api/users")
                       : Get("api/users", cancellationToken);
        }

        public Task<HttpResponseMessage> GetIndividualUser(string userName, CancellationToken cancellationToken =
                                                                                default(CancellationToken))
        {
            userName.CheckIfArgValid("userName");

            string url = string.Format("api/users/{0}", userName);

            return cancellationToken == default(CancellationToken) ? Get(url) : Get(url, cancellationToken);
        }

        public Task<HttpResponseMessage> CreateUser(string userName, Action<UserArgs> args,
                                                    CancellationToken cancellationToken =
                                                        default(CancellationToken))
        {
            userName.CheckIfArgValid("userName");
            args.CheckIfArgValid("args");

            var user = new UserArgsImpl();
            args(user);
            string url = string.Format("api/users/{0}", userName);
            //Logger.Info(x => x("Putting {0} to {1}", JsonConvert.SerializeObject(user), url));

            return cancellationToken == default(CancellationToken) ? Put(url, user) : Put(url, user, cancellationToken);
        }

        public Task<HttpResponseMessage> DeleteUser(string userName, CancellationToken cancellationToken =
                                                                         default(CancellationToken))
        {
            userName.CheckIfArgValid("userName");

            string url = string.Format("api/users/{0}", userName);

            return cancellationToken == default(CancellationToken) ? Delete(url) : Delete(url, cancellationToken);
        }

        #endregion

        #region Virtual Hosts

        public Task<HttpResponseMessage> GetAllVirtualHosts(CancellationToken cancellationToken =
                                                                default(CancellationToken))
        {
            if (IsLoggerEnabled)
                Logger.Info(x => x("Returning all of the virtual hosts on the specified RabbitMQ server."));
            
            return cancellationToken == default(CancellationToken)
                       ? Get("api/vhosts")
                       : Get("api/vhosts", cancellationToken);
        }

        public Task<HttpResponseMessage> CreateVirtualHost(string virtualHostName,
                                                           CancellationToken cancellationToken =
                                                               default(CancellationToken))
        {
            virtualHostName.CheckIfArgValid("virtualHostName");

            string url = string.Format("api/vhosts/{0}", virtualHostName.SanitizeVirtualHostName());
            if (IsLoggerEnabled)
                Logger.Info(x => x("Creating a new virtual host on the specified RabbitMQ server called '{0}'", virtualHostName));

            return cancellationToken == default(CancellationToken)
                       ? Put(url, new StringContent(string.Empty))
                       : Put(url, new StringContent(string.Empty), cancellationToken);
        }

        public Task<HttpResponseMessage> DeleteVirtualHost(string virtualHostName,
                                                           CancellationToken cancellationToken =
                                                               default(CancellationToken))
        {
            if (virtualHostName.SanitizeVirtualHostName() == "2%f")
            {
                if (IsLoggerEnabled)
                    Logger.Error(x => x("Cannot delete the default virtual host."));
                throw new CannotDeleteVirtualHostException("Cannot delete the default virtual host.");
            }

            virtualHostName.CheckIfArgValid("virtualHostName");

            string url = string.Format("api/vhosts/{0}", virtualHostName.SanitizeVirtualHostName());
            if (IsLoggerEnabled)
                Logger.Info(x => x("Deleting a virtual host on the specified RabbitMQ server called '{0}'", virtualHostName));

            return cancellationToken == default(CancellationToken) ? Delete(url) : Delete(url, cancellationToken);
        }

        #endregion

        #region Queues

        public Task<HttpResponseMessage> GetAllQueues(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return cancellationToken == default(CancellationToken)
                       ? Get("api/queues")
                       : Get("api/queues", cancellationToken);
        }

        public Task<HttpResponseMessage> GetAllBindingsOnQueue(string virtualHostName, string queueName,
                                                               CancellationToken cancellationToken =
                                                                   default(CancellationToken))
        {
            virtualHostName.CheckIfArgValid("virtualHostName");
            queueName.CheckIfArgValid("queueName");

            string url = string.Format("api/queues/{0}/{1}/bindings", virtualHostName.SanitizeVirtualHostName(),
                                       queueName);

            return cancellationToken == default(CancellationToken) ? Get(url) : Get(url, cancellationToken);
        }

        public Task<HttpResponseMessage> CreateQueue(string virtualHostName, string queueName,
                                                     Action<CreateQueueArgs> args,
                                                     CancellationToken cancellationToken =
                                                         default(CancellationToken))
        {
            return CreateQueueHelper(null, virtualHostName, queueName, args, cancellationToken);
        }

        public Task<HttpResponseMessage> CreateQueue(string node, string virtualHostName, string queueName,
                                                     Action<CreateQueueArgs> args,
                                                     CancellationToken cancellationToken = default(CancellationToken))
        {
            return CreateQueueHelper(node, virtualHostName, queueName, args, cancellationToken);
        }

        public Task<HttpResponseMessage> BindQueueToExchange(string virtualHostName, string exchangeName,
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

        public Task<HttpResponseMessage> DeleteQueue(string virtualHostName, string queueName,
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

        public Task<HttpResponseMessage> GetAllExchanges(CancellationToken cancellationToken =
                                                             default(CancellationToken))
        {
            return cancellationToken == default(CancellationToken)
                       ? Get("api/exchanges")
                       : Get("api/exchanges", cancellationToken);
        }

        public Task<HttpResponseMessage> GetAllExchangesInVirtualHost(string virtualHostName,
                                                                      CancellationToken cancellationToken
                                                                          =
                                                                          default(CancellationToken))
        {
            virtualHostName.CheckIfArgValid("virtualHostName");

            string url = string.Format("api/exchanges/{0}", virtualHostName.SanitizeVirtualHostName());

            return cancellationToken == default(CancellationToken) ? Get(url) : Get(url, cancellationToken);
        }

        public Task<HttpResponseMessage> GetExchange(string virtualHostName, string exchangeName,
                                                     CancellationToken cancellationToken =
                                                         default(CancellationToken))
        {
            virtualHostName.CheckIfArgValid("virtualHostName");
            exchangeName.CheckIfArgValid("exchangeName");

            string url = string.Format("api/exchanges/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), exchangeName);

            return cancellationToken == default(CancellationToken) ? Get(url) : Get(url, cancellationToken);
        }

        public Task<HttpResponseMessage> GetAllBindingsOnExchange(string virtualHostName,
                                                                  string exchangeName,
                                                                  bool isSource,
                                                                  CancellationToken cancellationToken =
                                                                      default(CancellationToken))
        {
            virtualHostName.CheckIfArgValid("virtualHostName");
            exchangeName.CheckIfArgValid("exchangeName");

            string url = string.Format("api/exchanges/{0}/{1}/bindings/{2}",
                                       virtualHostName.SanitizeVirtualHostName(), exchangeName,
                                       isSource ? "source" : "destination");

            return cancellationToken == default(CancellationToken) ? Get(url) : Get(url, cancellationToken);
        }

        public Task<HttpResponseMessage> CreateExchange(string virtualHostName, string exchangeName,
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

        public Task<HttpResponseMessage> DeleteExchange(string virtualHostName, string exchangeName,
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