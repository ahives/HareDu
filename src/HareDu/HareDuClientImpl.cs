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
        public HareDuClientImpl(ClientInitParamsImpl @params) :
            base(@params)
        {
        }

        #region Helpers

        private Task<ModifyResponse> CreateQueueHelper(string node, string virtualHostName, string queueName,
                                                            Action<QueueCreateParams> args,
                                                            CancellationToken cancellationToken =
                                                                default(CancellationToken))
        {
            Arg.Validate(virtualHostName, "virtualHostName",
                () => LogError("CreateQueue method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(queueName, "queueName",
                () => LogError("CreateQueue method threw an ArgumentNullException exception because queue name was invalid (i.e. empty, null, or all whitespaces)"));

            if (args == null)
                throw new ArgumentNullException("args");

            QueueCreateParamsImpl queue;
            if (string.IsNullOrEmpty(node) || string.IsNullOrWhiteSpace(node))
            {
                queue = new QueueCreateParamsImpl();
            }
            else
            {
                Arg.Validate(node, "node",
                    () => LogError("CreateQueue method threw an ArgumentNullException exception because node name was invalid (i.e. empty, null, or all whitespaces)"));
                queue = new QueueCreateParamsImpl { Node = node };
            }

            args(queue);

            string url = string.Format("api/queues/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), queueName);

            LogInfo(string.IsNullOrEmpty(node)
                            ? string.Format(
                                "Sent request to RabbitMQ server to create queue '{0}' on virtual host '{1}'.", queueName, virtualHostName)
                            : string.Format(
                                "Sent request to RabbitMQ server to create queue '{0}' on virtual host '{1}' on node '{2}'.", queueName, virtualHostName, node));

            return Put(url, queue, cancellationToken).Response(cancellationToken);
        }

        #endregion

        #region HTTP Client Management

        public void CancelPendingRequests()
        {
            LogInfo("Cancel all pending requests.");

            Client.CancelPendingRequests();
        }

        #endregion

        #region Connections

        public Task<IEnumerable<Connection>> GetAllConnections(CancellationToken cancellationToken = default(CancellationToken))
        {
            LogInfo("Sent request to return all information pertaining to all connections on current RabbitMQ server.");

            string url = "api/connections";

            return Get(url, cancellationToken).Response<IEnumerable<Connection>>(cancellationToken);
        }

        public Task<Connection> GetConnection(string connectionName,
                                              CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(connectionName, "connectionName",
                () => LogError("GetConnection method threw an ArgumentNullException exception because connection name was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/connections/{0}", connectionName);

            LogInfo(string.Format("Sent request to return all information pertaining to connection '{0}' on current RabbitMQ server.", connectionName));

            return Get(url, cancellationToken).Response<Connection>(cancellationToken);
        }

        #endregion

        #region Channels

        public Task<IEnumerable<Channel>> GetAllChannels(CancellationToken cancellationToken = default(CancellationToken))
        {
            LogInfo("Sent request to return all information pertaining to all channels on current RabbitMQ server.");

            string url = "api/channels";

            return Get(url, cancellationToken).Response<IEnumerable<Channel>>(cancellationToken);
        }

        public Task<Channel> GetChannel(string channelName,
                                                    CancellationToken cancellationToken =
                                                        default(CancellationToken))
        {
            Arg.Validate(channelName, "channelName",
                () => LogError("GetChannel method threw an ArgumentNullException exception because channel name was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/channels/{0}", channelName);

            LogInfo(string.Format("Sent request to return all information pertaining to channel '{0}' on current RabbitMQ server.", channelName));

            return Get(url, cancellationToken).Response<Channel>(cancellationToken);
        }

        #endregion

        #region Aliveness Test

        public Task<AlivenessTestResponse> IsAlive(string virtualHostName,
                                           CancellationToken cancellationToken =
                                               default(CancellationToken))
        {
            Arg.Validate(virtualHostName, "virtualHostName",
                () => LogError("IsAlive method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/aliveness-test/{0}", virtualHostName);

            LogInfo(string.Format("Sent request to execute an aliveness test on virtual host '{0}' on current RabbitMQ server.", virtualHostName));

            return Get(url, cancellationToken)
                .ContinueWith(t =>
                                  {
                                      t.Result.EnsureSuccessStatusCode();
                                      var response = t.Result.Content.ReadAsAsync<AlivenessTestResponse>().Result;
                                      response.StatusCode = t.Result.StatusCode;
                                      response.ServerResponse = t.Result.ReasonPhrase;

                                      return response;
                                  }, cancellationToken, TaskContinuationOptions.OnlyOnRanToCompletion,
                              TaskScheduler.Current);
        }

        #endregion

        #region Permissions

        public Task<UserPermissions> GetUserPermissions(string virtualHostName, string userName,
                                                                      CancellationToken cancellationToken =
                                                                          default(CancellationToken))
        {
            Arg.Validate(virtualHostName, "virtualHostName",
                () => LogError("GetUserPermissions method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(userName, "userName",
                () => LogError("GetUserPermissions method threw an ArgumentNullException exception because username was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/permissions/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), userName);

            LogInfo(
                string.Format("Sent request to return user permission information pertaining to user '{0}' on virtual host '{1}' users on current RabbitMQ server.", userName, virtualHostName));

            return Get(url, cancellationToken).Response<UserPermissions>(cancellationToken);
        }

        public Task<IEnumerable<UserPermissions>> GetAlllUserPermissions(
            CancellationToken cancellationToken = new CancellationToken())
        {
            LogInfo("Sent request to return all user permission information pertaining to all users on current RabbitMQ server.");

            string url = "api/permissions";

            return Get(url, cancellationToken).Response<IEnumerable<UserPermissions>>(cancellationToken);
        }

        public Task<ModifyResponse> SetUserPermissions(string userName, string virtualHostName, Action<PermissionsCreateParams> args, CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(virtualHostName, "virtualHostName",
                () => LogError("SetUserPermissions method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(userName, "userName",
                () => LogError("SetUserPermissions method threw an ArgumentNullException exception because username was invalid (i.e. empty, null, or all whitespaces)"));

            if (args == null)
                throw new ArgumentNullException("args");

            var permissions = new PermissionsCreateParamsImpl();
            args(permissions);

            Arg.Validate(permissions.ConfigurePermissions, "configurePermissions",
                () => LogError("SetUserPermissions method threw an ArgumentNullException exception because configure permissions was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(permissions.WritePermissions, "writePermissions",
                () => LogError("SetUserPermissions method threw an ArgumentNullException exception because write permissions was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(permissions.ReadPermissions, "readPermissions",
                () => LogError("SetUserPermissions method threw an ArgumentNullException exception because read permissions was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/permissions/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), userName);

            LogInfo(string.Format("Sent request to the RabbitMQ server to set permissions for user '{0}' on virtual host '{1}'.", userName, virtualHostName));

            return Put(url, permissions, cancellationToken).Response(cancellationToken);
        }

        public Task<ModifyResponse> DeleteUserPermissions(string userName, string virtualHostName, CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(virtualHostName, "virtualHostName",
                () => LogError("DeleteUserPermissions method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(userName, "userName",
                () => LogError("DeleteUserPermissions method threw an ArgumentNullException exception because username was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/permissions/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), userName);

            return Delete(url, cancellationToken).Response(cancellationToken);
        }

        #endregion

        #region Users

        public Task<IEnumerable<User>> GetAllUsers(CancellationToken cancellationToken = default(CancellationToken))
        {
            LogInfo("Sent request to return all information pertaining to all users on current RabbitMQ server.");

            string url = "api/users";

            return Get(url, cancellationToken).Response<IEnumerable<User>>(cancellationToken);
        }

        public Task<User> GetUser(string userName, CancellationToken cancellationToken =
                                                                                default(CancellationToken))
        {
            Arg.Validate(userName, "userName",
                () => LogError("GetUser method threw an ArgumentNullException exception because username was invalid (i.e. empty, null, or all whitespaces)"));

            LogInfo(string.Format("Sent request to the RabbitMQ server to return information pertaining to user '{0}'.", userName));

            string url = string.Format("api/users/{0}", userName);

            return Get(url, cancellationToken).Response<User>(cancellationToken);
        }

        public Task<ModifyResponse> CreateUser(string userName, Action<UserCreateParams> args,
                                                    CancellationToken cancellationToken =
                                                        default(CancellationToken))
        {
            Arg.Validate(userName, "userName",
                () => LogError("CreateUser method threw an ArgumentNullException exception because username was invalid (i.e. empty, null, or all whitespaces)"));

            LogInfo(string.Format("Sent request to the RabbitMQ server to create user '{0}'.", userName));

            if (args == null)
                throw new ArgumentNullException("args");

            var user = new UserCreateParamsImpl();
            args(user);

            Arg.Validate(user.Password, "password",
                () => LogError("CreateUser method threw an ArgumentNullException exception because password was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/users/{0}", userName);

            return Put(url, user, cancellationToken).Response(cancellationToken);
        }

        public Task<ModifyResponse> DeleteUser(string userName, CancellationToken cancellationToken =
                                                                         default(CancellationToken))
        {
            Arg.Validate(userName, "userName",
                () => LogError("DeleteUser method threw an ArgumentNullException exception because username was invalid (i.e. empty, null, or all whitespaces)"));

            if (userName == InitParams.Username)
            {
                LogError("DeleteUser method threw a CannotDeleteSessionLoginUserException exception because attempted to delete the login user");
                throw new CannotDeleteSessionLoginUserException(
                    string.Format(
                        "Cannot delete user '{0}' because it is being used to send requests login to the current client session.", userName));
            }

            LogInfo(string.Format("Sent request to RabbitMQ server to delete user '{0}'.", userName));

            string url = string.Format("api/users/{0}", userName);

            return Delete(url, cancellationToken).Response(cancellationToken);
        }

        #endregion

        #region Virtual Hosts

        public Task<IEnumerable<VirtualHost>> GetAllVirtualHosts(CancellationToken cancellationToken =
                                                                default(CancellationToken))
        {
            LogInfo("Sent request to return all information on all virtual hosts on current RabbitMQ server.");

            string url = "api/vhosts";

            return Get(url, cancellationToken).Response<IEnumerable<VirtualHost>>(cancellationToken);
        }

        public Task<ModifyResponse> CreateVirtualHost(string virtualHostName,
                                                           CancellationToken cancellationToken =
                                                               default(CancellationToken))
        {
            Arg.Validate(virtualHostName, "virtualHostName",
                () => LogError("CreateVirtualHost method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/vhosts/{0}", virtualHostName.SanitizeVirtualHostName());

            LogInfo(string.Format("Sent request to RabbitMQ server to create virtual host '{0}'.", virtualHostName));

            return Put(url, new StringContent(string.Empty), cancellationToken).Response(cancellationToken);
        }

        public Task<ModifyResponse> DeleteVirtualHost(string virtualHostName,
                                                           CancellationToken cancellationToken =
                                                               default(CancellationToken))
        {
            if (virtualHostName.SanitizeVirtualHostName() == "2%f")
            {
                LogError("DeleteVirtualHost method threw a CannotDeleteVirtualHostException exception for attempting to delete the default virtual host.");
                throw new CannotDeleteVirtualHostException("Cannot delete the default virtual host.");
            }

            Arg.Validate(virtualHostName, "virtualHostName",
                () => LogError("DeleteVirtualHost method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/vhosts/{0}", virtualHostName.SanitizeVirtualHostName());

            LogInfo(string.Format("Sent request to RabbitMQ server to delete virtual host '{0}'.", virtualHostName));

            return Delete(url, cancellationToken).Response(cancellationToken);
        }

        #endregion

        #region Queues

        public Task<IEnumerable<Queue>> GetAllQueues(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            LogInfo("Sent request to return all information on all queues on all virtual hosts on current RabbitMQ server.");

            string url = "api/queues";

            return Get(url, cancellationToken).Response<IEnumerable<Queue>>(cancellationToken);
        }

        public Task<IEnumerable<Binding>> GetAllBindingsOnQueue(string queueName, string virtualHostName, CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(virtualHostName, "virtualHostName",
                () => LogError("GetAllBindingsOnQueue method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(queueName, "queueName",
                () => LogError("GetAllBindingsOnQueue method threw an ArgumentNullException exception because queue name was invalid (i.e. empty, null, or all whitespaces)"));

            LogInfo(string.Format("Sent request to RabbitMQ server to return all bindings on queue '{0}' belonging to virtual host '{1}'.", queueName, virtualHostName));

            string url = string.Format("api/queues/{0}/{1}/bindings", virtualHostName.SanitizeVirtualHostName(), queueName);

            return Get(url, cancellationToken).Response<IEnumerable<Binding>>(cancellationToken);
        }

        public Task<ModifyResponse> CreateQueue(string queueName, string virtualHostName, Action<QueueCreateParams> args, CancellationToken cancellationToken = default(CancellationToken))
        {
            return CreateQueueHelper(null, virtualHostName, queueName, args, cancellationToken);
        }

        public Task<ModifyResponse> CreateQueue(string queueName, string node, string virtualHostName, Action<QueueCreateParams> args, CancellationToken cancellationToken = default(CancellationToken))
        {
            return CreateQueueHelper(node, virtualHostName, queueName, args, cancellationToken);
        }

        public Task<ModifyResponse> BindQueueToExchange(string queueName, string exchangeName, string virtualHostName, Action<QueueBindParams> args, CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(virtualHostName, "virtualHostName",
                () => LogError("BindQueueToExchange method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(queueName, "queueName",
                () => LogError("BindQueueToExchange method threw an ArgumentNullException exception because queue name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(exchangeName, "exchangeName",
                () => LogError("BindQueueToExchange method threw an ArgumentNullException exception because exchange name was invalid (i.e. empty, null, or all whitespaces)"));

            if (args == null)
                throw new ArgumentNullException("args");

            var queueBinding = new QueueBindParamsImpl();
            args(queueBinding);

            Arg.Validate(queueBinding.RoutingKey, "routingKey",
                () => LogError("BindQueueToExchange method threw an ArgumentNullException exception because routing key was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/bindings/{0}/e/{1}/q/{2}", virtualHostName.SanitizeVirtualHostName(), exchangeName, queueName);

            LogInfo(string.Format("Sent request to RabbitMQ server to bind queue '{0}' to exchange '{1}' belonging to virtual host '{2}'.", queueName, exchangeName, virtualHostName));

            return Put(url, queueBinding, cancellationToken).Response(cancellationToken);
        }

        public Task<ModifyResponse> DeleteQueue(string queueName, string virtualHostName, CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(virtualHostName, "virtualHostName",
                () => LogError("DeleteQueue method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(queueName, "queueName",
                () => LogError("DeleteQueue method threw an ArgumentNullException exception because queue name was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/queues/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), queueName);

            LogInfo(string.Format("Sent request to RabbitMQ server to delete queue '{0}' from virtual host '{1}'.", queueName, virtualHostName));

            return Delete(url, cancellationToken).Response(cancellationToken);
        }

        #endregion

        #region Exchanges

        public Task<IEnumerable<Exchange>> GetAllExchanges(CancellationToken cancellationToken = default(CancellationToken))
        {
            LogInfo("Sent request to return all information pertaining to all exchanges on all virtual hosts on current RabbitMQ server.");

            string url = "api/exchanges";

            return Get(url, cancellationToken).Response<IEnumerable<Exchange>>(cancellationToken);
        }

        public Task<IEnumerable<Exchange>> GetAllExchangesInVirtualHost(string virtualHostName, CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(virtualHostName, "virtualHostName",
                () => LogError("GetAllExchangesInVirtualHost method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));

            LogInfo(string.Format("Sent request to RabbitMQ server to return all information pertaining to all exchanges belonging to virtual host '{0}'.", virtualHostName));

            string url = string.Format("api/exchanges/{0}", virtualHostName.SanitizeVirtualHostName());

            return Get(url, cancellationToken).Response<IEnumerable<Exchange>>(cancellationToken);
        }

        public Task<Exchange> GetExchange(string exchangeName, string virtualHostName, CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(virtualHostName, "virtualHostName",
                () => LogError("GetExchange method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(exchangeName, "exchangeName",
                () => LogError("GetExchange method threw an ArgumentNullException exception because exchange name was invalid (i.e. empty, null, or all whitespaces)"));

            LogInfo(string.Format("Sent request to RabbitMQ server to return information pertaining to exchange '{0}' belonging to virtual host '{1}'.", exchangeName, virtualHostName));

            string url = string.Format("api/exchanges/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), exchangeName);

            return Get(url, cancellationToken).Response<Exchange>(cancellationToken);
        }

        public Task<IEnumerable<Binding>> GetAllBindingsOnExchange(string exchangeName, string virtualHostName, bool isSource, CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(virtualHostName, "virtualHostName",
                () => LogError("GetAllBindingsOnExchange method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(exchangeName, "exchangeName",
                () => LogError("GetAllBindingsOnExchange method threw an ArgumentNullException exception because exchange name was invalid (i.e. empty, null, or all whitespaces)"));

            LogInfo(string.Format("Sent request to RabbitMQ server to return all the bindings for exchange '{0}' belonging to virtual host '{1}'.", exchangeName, virtualHostName));

            string url = string.Format("api/exchanges/{0}/{1}/bindings/{2}",
                                       virtualHostName.SanitizeVirtualHostName(), exchangeName,
                                       isSource ? "source" : "destination");

            return Get(url, cancellationToken).Response<IEnumerable<Binding>>(cancellationToken);
        }

        public Task<ModifyResponse> CreateExchange(string exchangeName, string virtualHostName, Action<ExchangeCreateParams> args = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(virtualHostName, "virtualHostName",
                () => LogError("CreateExchange method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(exchangeName, "exchangeName",
                () => LogError("CreateExchange method threw an ArgumentNullException exception because exchange name was invalid (i.e. empty, null, or all whitespaces)"));

            LogInfo(string.Format("Sent request to RabbitMQ server to create an exchange '{0}' within virtual host '{1}'.", exchangeName, virtualHostName));

            if (args == null)
                throw new ArgumentNullException("args");

            var exchange = new ExchangeCreateParamsImpl();
            args(exchange);

            Arg.Validate(exchange.RoutingType, "routingType",
                () => LogError("CreateExchange method threw an ArgumentNullException exception because routing type was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/exchanges/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), exchangeName);

            return Put(url, exchange, cancellationToken).Response(cancellationToken);
        }

        public Task<ModifyResponse> DeleteExchange(string exchangeName, string virtualHostName, CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(virtualHostName, "virtualHostName",
                () => LogError("DeleteExchange method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(exchangeName, "exchangeName",
                () => LogError("DeleteExchange method threw an ArgumentNullException exception because exchange name was invalid (i.e. empty, null, or all whitespaces)"));

            LogInfo(string.Format("Sent request to RabbitMQ server to delete exchange '{0}' from virtual host '{1}'.", exchangeName, virtualHostName));

            string url = string.Format("api/exchanges/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), exchangeName);

            return Delete(url, cancellationToken).Response(cancellationToken);
        }

        #endregion

        #region Nodes

        public Task<IEnumerable<Node>> GetAllNodesOnCluster(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            LogInfo("Sent request to return all information pertaining to all nodes on RabbitMQ cluster.");

            string url = "api/nodes";

            return Get(url, cancellationToken).Response<IEnumerable<Node>>(cancellationToken);
        }

        public Task<Node> GetNodeOnCluster(string nodeName,
                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(nodeName, "nodeName",
                () => LogError("GetNodeOnCluster method threw an ArgumentNullException exception because node name was invalid (i.e. empty, null, or all whitespaces)"));

            LogInfo("Sent request to return all information pertaining to all nodes on RabbitMQ cluster.");

            string url = string.Format("api/nodes/{0}", nodeName);

            return Get(url, cancellationToken).Response<Node>(cancellationToken);
        }

        #endregion

        #region Overview

        public Task<Overview> GetOverview(CancellationToken cancellationToken = default(CancellationToken))
        {
            LogInfo("Sent request to return general information pertaining to current RabbitMQ server.");

            string url = "api/overview";

            return Get(url, cancellationToken).Response<Overview>(cancellationToken);
        }

        #endregion
    }
}