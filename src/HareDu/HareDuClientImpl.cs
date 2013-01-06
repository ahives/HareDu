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
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using Internal;
    using Model;

    internal class HareDuClientImpl :
        HareDuClientBase,
        HareDuClient
    {
        public HareDuClientImpl(ClientInitArgsImpl args) :
            base(args)
        {
        }

        #region Helpers

        private Task<AsyncResponse> CreateQueueHelper(string node, string virtualHostName, string queueName,
                                                            Action<CreateQueueArgs> args,
                                                            CancellationToken cancellationToken =
                                                                default(CancellationToken))
        {
            Arg.Validate(queueName, "queueName");
            Arg.Validate(virtualHostName, "virtualHostName");
            Arg.Validate(args, "args");

            CreateQueueArgsImpl queue;
            if (string.IsNullOrEmpty(node) || string.IsNullOrWhiteSpace(node))
            {
                queue = new CreateQueueArgsImpl();
            }
            else
            {
                Arg.Validate(node, "node");
                queue = new CreateQueueArgsImpl {Node = node};
            }

            args(queue);
            string url = string.Format("queues/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), queueName);

            if (IsLoggingEnabled)
            {
                var msg = string.IsNullOrEmpty(node)
                              ? string.Format(
                                  "Sent request to RabbitMQ server to create queue '{0}' on virtual host '{1}'.", queueName, virtualHostName)
                              : string.Format(
                                  "Sent request to RabbitMQ server to create queue '{0}' on virtual host '{1}' on node '{2}'.", queueName, virtualHostName, node);

                if (IsLoggingEnabled)
                    Logger.Info(x => x(msg));
            }

            return Put(url, queue, cancellationToken).Response(cancellationToken);
        }

        #endregion

        #region HTTP Client Management

        public void CancelPendingRequests()
        {
            if (IsLoggingEnabled)
                Logger.Info(x => x("Cancel all pending requests."));

            Client.CancelPendingRequests();
        }

        #endregion

        #region Connectivity

        public Task<IEnumerable<Connection>> GetAllConnections(CancellationToken cancellationToken =
                                                               default(CancellationToken))
        {
            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to return all information pertaining to all connections on current RabbitMQ server."));

            string url = "api/connections";

            return Get(url, cancellationToken).Response<IEnumerable<Connection>>(cancellationToken);
        }

        public Task<Connection> GetConnection(string connectionName,
                                                       CancellationToken cancellationToken =
                                                           default(CancellationToken))
        {
            Arg.Validate(connectionName, "connectionName");

            string url = string.Format("api/connections/{0}", connectionName);

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to return all information pertaining to connection '{0}' on current RabbitMQ server.", connectionName));

            return Get(url, cancellationToken).Response<Connection>(cancellationToken);
        }

        public Task<IEnumerable<Channel>> GetAllChannels(CancellationToken cancellationToken =
                                                            default(CancellationToken))
        {
            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to return all information pertaining to all channels on current RabbitMQ server."));

            string url = "api/channels";

            return Get(url, cancellationToken).Response<IEnumerable<Channel>>(cancellationToken);
        }

        public Task<Channel> GetChannel(string channelName,
                                                    CancellationToken cancellationToken =
                                                        default(CancellationToken))
        {
            Arg.Validate(channelName, "channelName");

            string url = string.Format("api/channels/{0}", channelName);

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to return all information pertaining to channel '{0}' on current RabbitMQ server.", channelName));

            return Get(url, cancellationToken).Response<Channel>(cancellationToken);
        }

        #endregion

        #region Health Monitoring

        public Task<HealthCheckResponse> IsAlive(string virtualHostName,
                                           CancellationToken cancellationToken =
                                               default(CancellationToken))
        {
            Arg.Validate(virtualHostName, "virtualHostName");

            string url = string.Format("api/aliveness-test/{0}", virtualHostName);

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to execute an aliveness test on virtual host '{0}' on current RabbitMQ server.", virtualHostName));

            return Get(url, cancellationToken)
                .ContinueWith(t =>
                                  {
                                      t.Result.EnsureSuccessStatusCode();
                                      var response = t.Result.Content.ReadAsAsync<HealthCheckResponse>().Result;
                                      response.StatusCode = t.Result.StatusCode;
                                      response.ServerResponse = t.Result.ReasonPhrase;

                                      return response;
                                  }, cancellationToken, TaskContinuationOptions.OnlyOnRanToCompletion,
                              TaskScheduler.Current);
        }

        #endregion

        #region Permissions

        public Task<UserPermissions> GetIndividualUserPermissions(string virtualHostName, string userName,
                                                                      CancellationToken cancellationToken =
                                                                          default(CancellationToken))
        {
            Arg.Validate(virtualHostName, "virtualHostName");
            Arg.Validate(userName, "userName");

            string url = string.Format("api/permissions/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), userName);

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to return user permission information pertaining to user '{0}' on virtual host '{1}' users on current RabbitMQ server.", userName, virtualHostName));

            return Get(url, cancellationToken).Response<UserPermissions>(cancellationToken);
        }

        public Task<IEnumerable<UserPermissions>> GetAlllUserPermissions(
            CancellationToken cancellationToken = new CancellationToken())
        {
            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to return all user permission information pertaining to all users on current RabbitMQ server."));

            string url = "api/permissions";

            return Get(url, cancellationToken).Response<IEnumerable<UserPermissions>>(cancellationToken);
        }

        public Task<AsyncResponse> CreateUserPermissions(string userName, string virtualHostName, Action<UserPermissionsArgs> args, CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(virtualHostName, "virtualHostName");
            Arg.Validate(userName, "userName");
            Arg.Validate(args, "args");

            var permissions = new UserPermissionsArgsImpl();
            args(permissions);
            string url = string.Format("api/permissions/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), userName);

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to the RabbitMQ server to create permissions for user '{0}' on virtual host '{1}'.", userName, virtualHostName));

            return Put(url, permissions, cancellationToken).Response(cancellationToken);
        }

        public Task<AsyncResponse> DeleteUserPermissions(string userName, string virtualHostName, CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(virtualHostName, "virtualHostName");
            Arg.Validate(userName, "userName");

            string url = string.Format("api/permissions/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), userName);

            return Delete(url, cancellationToken).Response(cancellationToken);
        }

        #endregion

        #region Users

        public Task<IEnumerable<User>> GetAllUsers(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to return all information pertaining to all users on current RabbitMQ server."));

            string url = "api/users";

            return Get(url, cancellationToken).Response<IEnumerable<User>>(cancellationToken);
        }

        public Task<User> GetIndividualUser(string userName, CancellationToken cancellationToken =
                                                                                default(CancellationToken))
        {
            Arg.Validate(userName, "userName");

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to the RabbitMQ server to return information pertaining to user '{0}'.", userName));

            string url = string.Format("api/users/{0}", userName);

            return Get(url, cancellationToken).Response<User>(cancellationToken);
        }

        public Task<AsyncResponse> CreateUser(string userName, Action<UserArgs> args,
                                                    CancellationToken cancellationToken =
                                                        default(CancellationToken))
        {
            Arg.Validate(userName, "userName");
            Arg.Validate(args, "args");

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to the RabbitMQ server to create user '{0}'.", userName));

            var user = new UserArgsImpl();
            args(user);
            string url = string.Format("api/users/{0}", userName);

            return Put(url, user, cancellationToken).Response(cancellationToken);
        }

        public Task<AsyncResponse> DeleteUser(string userName, CancellationToken cancellationToken =
                                                                         default(CancellationToken))
        {
            Arg.Validate(userName, "userName");

            if (userName == InitArgs.Username)
            {
                if (IsLoggingEnabled)
                    Logger.Info(x => x("Sent request to RabbitMQ server to delete user '{0}'.", userName));
                throw new CannotDeleteSessionLoginUserException(
                    string.Format(
                        "Cannot delete user '{0}' because it is being used to send requests login to the current client session.", userName));
            }

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to RabbitMQ server to delete user '{0}'.", userName));

            string url = string.Format("api/users/{0}", userName);

            return Delete(url, cancellationToken).Response(cancellationToken);
        }

        #endregion

        #region Virtual Hosts

        public Task<IEnumerable<VirtualHost>> GetAllVirtualHosts(CancellationToken cancellationToken =
                                                                default(CancellationToken))
        {
            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to return all information on all virtual hosts on current RabbitMQ server."));

            string url = "api/vhosts";

            return Get(url, cancellationToken).Response<IEnumerable<VirtualHost>>(cancellationToken);
        }

        public Task<AsyncResponse> CreateVirtualHost(string virtualHostName,
                                                           CancellationToken cancellationToken =
                                                               default(CancellationToken))
        {
            Arg.Validate(virtualHostName, "virtualHostName");

            string url = string.Format("api/vhosts/{0}", virtualHostName.SanitizeVirtualHostName());

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to RabbitMQ server to create virtual host '{0}'.", virtualHostName));

            return Put(url, new StringContent(string.Empty), cancellationToken).Response(cancellationToken);
        }

        public Task<AsyncResponse> DeleteVirtualHost(string virtualHostName,
                                                           CancellationToken cancellationToken =
                                                               default(CancellationToken))
        {
            if (virtualHostName.SanitizeVirtualHostName() == "2%f")
            {
                if (IsLoggingEnabled)
                    Logger.Error(x => x("Cannot delete the default virtual host."));
                throw new CannotDeleteVirtualHostException("Cannot delete the default virtual host.");
            }

            Arg.Validate(virtualHostName, "virtualHostName");

            string url = string.Format("api/vhosts/{0}", virtualHostName.SanitizeVirtualHostName());

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to RabbitMQ server to delete virtual host '{0}'.", virtualHostName));

            return Delete(url, cancellationToken).Response(cancellationToken);
        }

        #endregion

        #region Queues

        public Task<IEnumerable<Queue>> GetAllQueues(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to return all information on all queues on all virtual hosts on current RabbitMQ server."));

            string url = "api/queues";

            return Get(url, cancellationToken).Response<IEnumerable<Queue>>(cancellationToken);
        }

        public Task<IEnumerable<Binding>> GetAllBindingsOnQueue(string queueName, string virtualHostName, CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(virtualHostName, "virtualHostName");
            Arg.Validate(queueName, "queueName");

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to RabbitMQ server to return all bindings on queue '{0}' belonging to virtual host '{1}'.", queueName, virtualHostName));

            string url = string.Format("api/queues/{0}/{1}/bindings", virtualHostName.SanitizeVirtualHostName(), queueName);

            return Get(url, cancellationToken).Response<IEnumerable<Binding>>(cancellationToken);
        }

        public Task<AsyncResponse> CreateQueue(string queueName, string virtualHostName, Action<CreateQueueArgs> args, CancellationToken cancellationToken = default(CancellationToken))
        {
            return CreateQueueHelper(null, virtualHostName, queueName, args, cancellationToken);
        }

        public Task<AsyncResponse> CreateQueue(string queueName, string node, string virtualHostName, Action<CreateQueueArgs> args, CancellationToken cancellationToken = default(CancellationToken))
        {
            return CreateQueueHelper(node, virtualHostName, queueName, args, cancellationToken);
        }

        public Task<AsyncResponse> BindQueueToExchange(string queueName, string exchangeName, string virtualHostName, Action<BindQueueArgs> args, CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(queueName, "queueName");
            Arg.Validate(virtualHostName, "virtualHostName");
            Arg.Validate(exchangeName, "exchangeName");
            Arg.Validate(args, "args");

            var queueBinding = new BindQueueArgsImpl();
            args(queueBinding);
            string url = string.Format("api/bindings/{0}/e/{1}/q/{2}", virtualHostName.SanitizeVirtualHostName(),
                                       exchangeName, queueName);

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to RabbitMQ server to bind queue '{0}' to exchange '{1}' belonging to virtual host '{2}'.", queueName, exchangeName, virtualHostName));

            return Put(url, queueBinding, cancellationToken).Response(cancellationToken);
        }

        public Task<AsyncResponse> DeleteQueue(string virtualHostName, string queueName,
                                                     CancellationToken cancellationToken =
                                                         default(CancellationToken))
        {
            Arg.Validate(queueName, "queueName");
            Arg.Validate(virtualHostName, "virtualHostName");

            string url = string.Format("api/queues/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), queueName);

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to RabbitMQ server to delete queue '{0}' from virtual host '{1}'.", queueName, virtualHostName));

            return Delete(url, cancellationToken).Response(cancellationToken);
        }

        #endregion

        #region Exchanges

        public Task<IEnumerable<Exchange>> GetAllExchanges(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to return all information pertaining to all exchanges on all virtual hosts on current RabbitMQ server."));

            string url = "api/exchanges";

            return Get(url, cancellationToken).Response<IEnumerable<Exchange>>(cancellationToken);
        }

        public Task<IEnumerable<Exchange>> GetAllExchangesInVirtualHost(string virtualHostName, CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(virtualHostName, "virtualHostName");

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to RabbitMQ server to return all information pertaining to all exchanges belonging to virtual host '{0}'.", virtualHostName));

            string url = string.Format("api/exchanges/{0}", virtualHostName.SanitizeVirtualHostName());

            return Get(url, cancellationToken).Response<IEnumerable<Exchange>>(cancellationToken);
        }

        public Task<Exchange> GetExchange(string exchangeName, string virtualHostName, CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(virtualHostName, "virtualHostName");
            Arg.Validate(exchangeName, "exchangeName");

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to RabbitMQ server to return information pertaining to exchange '{0}' belonging to virtual host '{1}'.", exchangeName, virtualHostName));

            string url = string.Format("api/exchanges/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), exchangeName);

            return Get(url, cancellationToken).Response<Exchange>(cancellationToken);
        }

        public Task<IEnumerable<Binding>> GetAllBindingsOnExchange(string exchangeName, string virtualHostName, bool isSource, CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(virtualHostName, "virtualHostName");
            Arg.Validate(exchangeName, "exchangeName");

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to RabbitMQ server to return all the bindings for exchange '{0}' belonging to virtual host '{1}'.", exchangeName, virtualHostName));

            string url = string.Format("api/exchanges/{0}/{1}/bindings/{2}",
                                       virtualHostName.SanitizeVirtualHostName(), exchangeName,
                                       isSource ? "source" : "destination");

            return Get(url, cancellationToken).Response<IEnumerable<Binding>>(cancellationToken);
        }

        public Task<AsyncResponse> CreateExchange(string exchangeName, string virtualHostName, Action<CreateExchangeArgs> args = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(virtualHostName, "virtualHostName");
            Arg.Validate(exchangeName, "exchangeName");

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to RabbitMQ server to create an exchange '{0}' within virtual host '{1}'.", exchangeName, virtualHostName));

            var exchange = new CreateExchangeArgsImpl();
            if (args != null)
                args(exchange);
            string url = string.Format("api/exchanges/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), exchangeName);

            return Put(url, exchange, cancellationToken).Response(cancellationToken);
        }

        public Task<AsyncResponse> DeleteExchange(string exchangeName, string virtualHostName, CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(virtualHostName, "virtualHostName");
            Arg.Validate(exchangeName, "exchangeName");

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to RabbitMQ server to delete exchange '{0}' from virtual host '{1}'.", exchangeName, virtualHostName));

            string url = string.Format("api/exchanges/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), exchangeName);

            return Delete(url, cancellationToken).Response(cancellationToken);
        }

        #endregion
    }
}