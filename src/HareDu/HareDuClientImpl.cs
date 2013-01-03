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
    using System.Net;
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

        private AsyncResponse CreateQueueHelper(string node, string virtualHostName, string queueName,
                                                            Action<CreateQueueArgs> args,
                                                            CancellationToken cancellationToken =
                                                                default(CancellationToken))
        {
            queueName.CheckIfMethodParamIsValid("queueName");
            virtualHostName.CheckIfMethodParamIsValid("virtualHostName");
            args.CheckIfMethodParamIsValid("args");

            CreateQueueArgsImpl queue;
            if (string.IsNullOrEmpty(node) || string.IsNullOrWhiteSpace(node))
            {
                queue = new CreateQueueArgsImpl();
            }
            else
            {
                node.CheckIfMethodParamIsValid("node");
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

            var asyncTask = cancellationToken == default(CancellationToken)
                       ? Put(url, queue)
                       : Put(url, queue, cancellationToken);

            //return new AsyncResponseImpl(asyncTask);
            return null;
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

        public Task<HttpResponseMessage> GetAllConnections(CancellationToken cancellationToken =
                                                               default(CancellationToken))
        {
            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to return all information pertaining to all connections on current RabbitMQ server."));

            return cancellationToken == default(CancellationToken)
                       ? Get("api/connections")
                       : Get("api/connections", cancellationToken);
        }

        public Task<HttpResponseMessage> GetConnection(string connectionName,
                                                       CancellationToken cancellationToken =
                                                           default(CancellationToken))
        {
            connectionName.CheckIfMethodParamIsValid("connectionName");

            string url = string.Format("api/connections/{0}", connectionName);

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to return all information pertaining to connection '{0}' on current RabbitMQ server.", connectionName));

            return cancellationToken == default(CancellationToken)
                       ? Get(url)
                       : Get(url, cancellationToken);
        }

        public Task<HttpResponseMessage> GetAllChannels(CancellationToken cancellationToken =
                                                            default(CancellationToken))
        {
            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to return all information pertaining to all channels on current RabbitMQ server."));

            return cancellationToken == default(CancellationToken)
                       ? Get("api/channels")
                       : Get("api/channels", cancellationToken);
        }

        public Task<HttpResponseMessage> GetChannel(string channelName,
                                                    CancellationToken cancellationToken =
                                                        default(CancellationToken))
        {
            channelName.CheckIfMethodParamIsValid("channelName");

            string url = string.Format("api/channels/{0}", channelName);

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to return all information pertaining to channel '{0}' on current RabbitMQ server.", channelName));

            return cancellationToken == default(CancellationToken)
                       ? Get(url)
                       : Get(url, cancellationToken);
        }

        public Task<HttpResponseMessage> ExecuteAlivenessTest(string virtualHostName,
                                                              CancellationToken cancellationToken =
                                                                  default(CancellationToken))
        {
            virtualHostName.CheckIfMethodParamIsValid("virtualHostName");

            string url = string.Format("api/aliveness-test/{0}", virtualHostName);

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to execute an aliveness test on virtual host '{0}' on current RabbitMQ server.", virtualHostName));

            return cancellationToken == default(CancellationToken) ? Get(url) : Get(url, cancellationToken);
        }

        #endregion

        #region Permissions

        public Task<HttpResponseMessage> GetIndividualUserPermissions(string virtualHostName, string userName,
                                                                      CancellationToken cancellationToken =
                                                                          default(CancellationToken))
        {
            virtualHostName.CheckIfMethodParamIsValid("virtualHostName");
            userName.CheckIfMethodParamIsValid("userName");

            string url = string.Format("api/permissions/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), userName);

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to return user permission information pertaining to user '{0}' on virtual host '{1}' users on current RabbitMQ server.", userName, virtualHostName));

            return cancellationToken == default(CancellationToken) ? Get(url) : Get(url, cancellationToken);
        }

        public Task<HttpResponseMessage> GetAlllUserPermissions(
            CancellationToken cancellationToken = new CancellationToken())
        {
            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to return all user permission information pertaining to all users on current RabbitMQ server."));

            return cancellationToken == default(CancellationToken)
                       ? Get("api/permissions")
                       : Get("api/permissions", cancellationToken);
        }

        public Task<HttpResponseMessage> CreateUserPermissions(string userName, string virtualHostName, Action<UserPermissionsArgs> args, CancellationToken cancellationToken = default(CancellationToken))
        {
            virtualHostName.CheckIfMethodParamIsValid("virtualHostName");
            userName.CheckIfMethodParamIsValid("userName");
            args.CheckIfMethodParamIsValid("args");

            var permissions = new UserPermissionsArgsImpl();
            args(permissions);
            string url = string.Format("api/permissions/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), userName);

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to the RabbitMQ server to create permissions for user '{0}' on virtual host '{1}'.", userName, virtualHostName));

            return cancellationToken == default(CancellationToken)
                       ? Put(url, permissions)
                       : Put(url, permissions, cancellationToken);
        }

        public Task<HttpResponseMessage> DeleteUserPermissions(string userName, string virtualHostName, CancellationToken cancellationToken = default(CancellationToken))
        {
            virtualHostName.CheckIfMethodParamIsValid("virtualHostName");
            userName.CheckIfMethodParamIsValid("userName");

            string url = string.Format("api/permissions/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), userName);

            return cancellationToken == default(CancellationToken) ? Delete(url) : Delete(url, cancellationToken);
        }

        #endregion

        #region Users

        public AsyncResponse<IEnumerable<User>> GetAllUsers2(CancellationToken cancellationToken =
                                                         default(CancellationToken))
        {
            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to return all information pertaining to all users on current RabbitMQ server."));

             var asyncTask = cancellationToken == default(CancellationToken)
                       ? Get("api/users")
                       : Get("api/users", cancellationToken);
             return new AsyncResponseImpl<IEnumerable<User>>(asyncTask);
        }

        public Task<HttpResponseMessage> GetAllUsers(CancellationToken cancellationToken =
                                                         default(CancellationToken))
        {
            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to return all information pertaining to all users on current RabbitMQ server."));

            return cancellationToken == default(CancellationToken)
                       ? Get("api/users")
                       : Get("api/users", cancellationToken);
        }

        public Task<HttpResponseMessage> GetIndividualUser(string userName, CancellationToken cancellationToken =
                                                                                default(CancellationToken))
        {
            userName.CheckIfMethodParamIsValid("userName");

            string url = string.Format("api/users/{0}", userName);

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to the RabbitMQ server to return information pertaining to user '{0}'.", userName));

            return cancellationToken == default(CancellationToken) ? Get(url) : Get(url, cancellationToken);
        }

        public Task<HttpResponseMessage> CreateUser(string userName, Action<UserArgs> args,
                                                    CancellationToken cancellationToken =
                                                        default(CancellationToken))
        {
            userName.CheckIfMethodParamIsValid("userName");
            args.CheckIfMethodParamIsValid("args");

            var user = new UserArgsImpl();
            args(user);
            string url = string.Format("api/users/{0}", userName);
            
            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to the RabbitMQ server to create user '{0}'.", userName));

            return cancellationToken == default(CancellationToken) ? Put(url, user) : Put(url, user, cancellationToken);
        }

        public Task<HttpResponseMessage> DeleteUser(string userName, CancellationToken cancellationToken =
                                                                         default(CancellationToken))
        {
            userName.CheckIfMethodParamIsValid("userName");

            if (userName == InitArgs.Username)
            {
                if (IsLoggingEnabled)
                    Logger.Info(x => x("Sent request to RabbitMQ server to delete user '{0}'.", userName));
                throw new CannotDeleteSessionLoginUserException(
                    string.Format(
                        "Cannot delete user '{0}' because it is being used to send requests login to the current client session.", userName));
            }

            string url = string.Format("api/users/{0}", userName);
            
            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to RabbitMQ server to delete user '{0}'.", userName));

            return cancellationToken == default(CancellationToken) ? Delete(url) : Delete(url, cancellationToken);
        }

        #endregion

        #region Virtual Hosts

        public Task<IEnumerable<VirtualHost>> GetAllVirtualHosts(CancellationToken cancellationToken =
                                                                default(CancellationToken))
        {
            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to return all information on all virtual hosts on current RabbitMQ server."));

            return Get("api/vhosts", cancellationToken)
                .ContinueWith(t =>
                                  {
                                      t.Result.EnsureSuccessStatusCode();
                                      return t.Result.Content.ReadAsAsync<IEnumerable<VirtualHost>>();
                                  }, cancellationToken, TaskContinuationOptions.OnlyOnRanToCompletion,
                              TaskScheduler.Current)
                .Unwrap();
        }

        public Task<AsyncResponse> CreateVirtualHost(string virtualHostName,
                                                           CancellationToken cancellationToken =
                                                               default(CancellationToken))
        {
            virtualHostName.CheckIfMethodParamIsValid("virtualHostName");

            string url = string.Format("api/vhosts/{0}", virtualHostName.SanitizeVirtualHostName());

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to RabbitMQ server to create virtual host '{0}'.", virtualHostName));

            return Put(url, new StringContent(string.Empty), cancellationToken)
                .ContinueWith(t =>
                                  {
                                      AsyncResponse response = new AsyncResponseImpl(t.Result.ReasonPhrase, t.Result.StatusCode);
                                      return response;
                                  },
                              cancellationToken, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.Current);
        }

        public AsyncResponse DeleteVirtualHost(string virtualHostName,
                                                           CancellationToken cancellationToken =
                                                               default(CancellationToken))
        {
            if (virtualHostName.SanitizeVirtualHostName() == "2%f")
            {
                if (IsLoggingEnabled)
                    Logger.Error(x => x("Cannot delete the default virtual host."));
                throw new CannotDeleteVirtualHostException("Cannot delete the default virtual host.");
            }

            virtualHostName.CheckIfMethodParamIsValid("virtualHostName");

            string url = string.Format("api/vhosts/{0}", virtualHostName.SanitizeVirtualHostName());

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to RabbitMQ server to delete virtual host '{0}'.", virtualHostName));

            var asyncTask = cancellationToken == default(CancellationToken) ? Delete(url) : Delete(url, cancellationToken);

            //return new AsyncResponseImpl(asyncTask);
            return null;
        }

        #endregion

        #region Queues

        public AsyncResponse<IEnumerable<Queue>> GetAllQueues(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to return all information on all queues on all virtual hosts on current RabbitMQ server."));

            var asyncTask = cancellationToken == default(CancellationToken)
                       ? Get("api/queues")
                       : Get("api/queues", cancellationToken);

            return new AsyncResponseImpl<IEnumerable<Queue>>(asyncTask);
        }

        public AsyncResponse<IEnumerable<Binding>> GetAllBindingsOnQueue(string queueName, string virtualHostName, CancellationToken cancellationToken = default(CancellationToken))
        {
            virtualHostName.CheckIfMethodParamIsValid("virtualHostName");
            queueName.CheckIfMethodParamIsValid("queueName");

            string url = string.Format("api/queues/{0}/{1}/bindings", virtualHostName.SanitizeVirtualHostName(), queueName);

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to RabbitMQ server to return all bindings on queue '{0}' belonging to virtual host '{1}'.", queueName, virtualHostName));

            var asyncTask = cancellationToken == default(CancellationToken) ? Get(url) : Get(url, cancellationToken);

            return new AsyncResponseImpl<IEnumerable<Binding>>(asyncTask);
        }

        public AsyncResponse CreateQueue(string virtualHostName, string queueName,
                                                     Action<CreateQueueArgs> args,
                                                     CancellationToken cancellationToken =
                                                         default(CancellationToken))
        {
            return CreateQueueHelper(null, virtualHostName, queueName, args, cancellationToken);
        }

        public AsyncResponse CreateQueue(string node, string virtualHostName, string queueName,
                                                     Action<CreateQueueArgs> args,
                                                     CancellationToken cancellationToken = default(CancellationToken))
        {
            return CreateQueueHelper(node, virtualHostName, queueName, args, cancellationToken);
        }

        public AsyncResponse BindQueueToExchange(string queueName, string exchangeName, string virtualHostName, Action<BindQueueArgs> args, CancellationToken cancellationToken = default(CancellationToken))
        {
            queueName.CheckIfMethodParamIsValid("queueName");
            virtualHostName.CheckIfMethodParamIsValid("virtualHostName");
            exchangeName.CheckIfMethodParamIsValid("exchangeName");
            args.CheckIfMethodParamIsValid("args");

            var queueBinding = new BindQueueArgsImpl();
            args(queueBinding);
            string url = string.Format("api/bindings/{0}/e/{1}/q/{2}", virtualHostName.SanitizeVirtualHostName(),
                                       exchangeName, queueName);

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to RabbitMQ server to bind queue '{0}' to exchange '{1}' belonging to virtual host '{2}'.", queueName, exchangeName, virtualHostName));

            var asyncTask = cancellationToken == default(CancellationToken)
                       ? Post(url, queueBinding)
                       : Post(url, queueBinding, cancellationToken);

            //return new AsyncResponseImpl(asyncTask);
            return null;
        }

        public AsyncResponse DeleteQueue(string virtualHostName, string queueName,
                                                     CancellationToken cancellationToken =
                                                         default(CancellationToken))
        {
            queueName.CheckIfMethodParamIsValid("queueName");
            virtualHostName.CheckIfMethodParamIsValid("virtualHostName");

            string url = string.Format("api/queues/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), queueName);

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to RabbitMQ server to delete queue '{0}' from virtual host '{1}'.", queueName, virtualHostName));

            var asyncTask = cancellationToken == default(CancellationToken) ? Delete(url) : Delete(url, cancellationToken);

            //return new AsyncResponseImpl(asyncTask);
            return null;
        }

        #endregion

        #region Exchanges

        public AsyncResponse<IEnumerable<Exchange>> GetAllExchanges(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to return all information pertaining to all exchanges on all virtual hosts on current RabbitMQ server."));

            var asyncTask = cancellationToken == default(CancellationToken)
                       ? Get("api/exchanges")
                       : Get("api/exchanges", cancellationToken);

            return new AsyncResponseImpl<IEnumerable<Exchange>>(asyncTask);
        }

        public AsyncResponse<IEnumerable<Exchange>> GetAllExchangesInVirtualHost(string virtualHostName, CancellationToken cancellationToken = default(CancellationToken))
        {
            virtualHostName.CheckIfMethodParamIsValid("virtualHostName");

            string url = string.Format("api/exchanges/{0}", virtualHostName.SanitizeVirtualHostName());

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to RabbitMQ server to return all information pertaining to all exchanges belonging to virtual host '{0}'.", virtualHostName));

            var asyncTask = cancellationToken == default(CancellationToken) ? Get(url) : Get(url, cancellationToken);

            return new AsyncResponseImpl<IEnumerable<Exchange>>(asyncTask);
        }

        public AsyncResponse<Exchange> GetExchange(string exchangeName, string virtualHostName, CancellationToken cancellationToken = default(CancellationToken))
        {
            virtualHostName.CheckIfMethodParamIsValid("virtualHostName");
            exchangeName.CheckIfMethodParamIsValid("exchangeName");

            string url = string.Format("api/exchanges/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), exchangeName);

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to RabbitMQ server to return information pertaining to exchange '{0}' belonging to virtual host '{1}'.", exchangeName, virtualHostName));

            var asyncTask = cancellationToken == default(CancellationToken) ? Get(url) : Get(url, cancellationToken);

            return new AsyncResponseImpl<Exchange>(asyncTask);
        }

        public AsyncResponse<IEnumerable<Binding>> GetAllBindingsOnExchange(string exchangeName, string virtualHostName, bool isSource, CancellationToken cancellationToken = default(CancellationToken))
        {
            virtualHostName.CheckIfMethodParamIsValid("virtualHostName");
            exchangeName.CheckIfMethodParamIsValid("exchangeName");

            string url = string.Format("api/exchanges/{0}/{1}/bindings/{2}",
                                       virtualHostName.SanitizeVirtualHostName(), exchangeName,
                                       isSource ? "source" : "destination");

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to RabbitMQ server to return all the bindings for exchange '{0}' belonging to virtual host '{1}'.", exchangeName, virtualHostName));

            var asyncTask = cancellationToken == default(CancellationToken) ? Get(url) : Get(url, cancellationToken);

            return new AsyncResponseImpl<IEnumerable<Binding>>(asyncTask);
        }

        public AsyncResponse CreateExchange(string exchangeName, string virtualHostName, Action<CreateExchangeArgs> args = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            virtualHostName.CheckIfMethodParamIsValid("virtualHostName");
            exchangeName.CheckIfMethodParamIsValid("exchangeName");

            var exchange = new CreateExchangeArgsImpl();
            if (args != null)
                args(exchange);
            string url = string.Format("api/exchanges/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), exchangeName);

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to RabbitMQ server to create an exchange '{0}' within virtual host '{1}'.", exchangeName, virtualHostName));

            var asyncTask = cancellationToken == default(CancellationToken)
                       ? Put(url, exchange)
                       : Put(url, exchange, cancellationToken);

            return new AsyncResponseImpl<Exchange>(asyncTask);
        }

        //public AsyncResponse DeleteExchange(string exchangeName, string virtualHostName, CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    virtualHostName.CheckIfMethodParamIsValid("virtualHostName");
        //    exchangeName.CheckIfMethodParamIsValid("exchangeName");

        //    string url = string.Format("api/exchanges/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), exchangeName);

        //    if (IsLoggingEnabled)
        //        Logger.Info(x => x("Sent request to RabbitMQ server to delete exchange '{0}' from virtual host '{1}'.", exchangeName, virtualHostName));

        //    var asyncTask = cancellationToken == default(CancellationToken) ? Delete(url) : Delete(url, cancellationToken);

        //    return new AsyncResponseImpl<Exchange>(asyncTask);
        //}

        public Task<AsyncResponse> DeleteExchange(string exchangeName, string virtualHostName, CancellationToken cancellationToken = default(CancellationToken))
        {
            virtualHostName.CheckIfMethodParamIsValid("virtualHostName");
            exchangeName.CheckIfMethodParamIsValid("exchangeName");

            string url = string.Format("api/exchanges/{0}/{1}", virtualHostName.SanitizeVirtualHostName(), exchangeName);

            if (IsLoggingEnabled)
                Logger.Info(x => x("Sent request to RabbitMQ server to delete exchange '{0}' from virtual host '{1}'.", exchangeName, virtualHostName));

            var asyncTask = cancellationToken == default(CancellationToken) ? Delete(url) : Delete(url, cancellationToken);

            //return new Task<AsyncResponse>(() => new AsyncResponseImpl(asyncTask));
            return null;
        }

        #endregion
    }

    internal class AsyncResponseImpl<T> :
        AsyncResponseImpl,
        AsyncResponse<T>
    {
        private Task<HttpResponseMessage> AsyncTask { get; set; }

        public AsyncResponseImpl(Task<HttpResponseMessage> asyncTask) :
            //base(asyncTask)
            base(string.Empty, HttpStatusCode.OK)
        {
            AsyncTask = asyncTask;
        }

        public T GetResponse()
        {
            AsyncTask.Result.EnsureSuccessStatusCode();
            var task = AsyncTask.Result.Content.ReadAsAsync<T>().ContinueWith(x => x.Result);

            return task.Result;
        }
    }

    internal class AsyncResponseImpl :
        AsyncResponse
    {
        private Task<HttpResponseMessage> AsyncTask { get; set; }

        public AsyncResponseImpl(string serverResponse, HttpStatusCode statusCode)
        {
            ServerResponse = serverResponse;
            StatusCode = statusCode;
        }

        public Task<HttpResponseMessage> GetAsyncTask()
        {
            return AsyncTask;
        }

        public HttpResponseMessage GetHttpResponseMessage()
        {
            return GetAsyncTask().Result;
        }

        public string ServerResponse { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
    }

    public interface AsyncResponse<T> :
        AsyncResponse
    {
        T GetResponse();
    }

    //public interface AsyncResponse
    //{
    //    Task<HttpResponseMessage> GetAsyncTask();
    //    HttpResponseMessage GetHttpResponseMessage();
    //}

    public interface AsyncResponse
    {
        string ServerResponse { get; }
        HttpStatusCode StatusCode { get; }
    }
}