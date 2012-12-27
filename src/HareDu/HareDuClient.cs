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

    public interface HareDuClient
    {
        void CancelPendingRequests();

        /// <summary>
        /// Returns a list of open connections to the RabbitMQ server.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetListOfAllOpenConnections(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetOpenConnection(string connectionName, CancellationToken cancellationToken = default(CancellationToken));

        Task<HttpResponseMessage> GetListOfAllOpenChannels(CancellationToken cancellationToken =
                                                                               default(CancellationToken));

        Task<HttpResponseMessage> GetOpenChannel(string channelName,
                                                                 CancellationToken cancellationToken =
                                                                     default(CancellationToken));

        Task<HttpResponseMessage> SendEKG(string virtualHostName, CancellationToken cancellationToken =
                                                                                      default(CancellationToken));

        Task<HttpResponseMessage> GetUserPermissions(string virtualHostName, string userName,
                                                                     CancellationToken cancellationToken =
                                                                         default(CancellationToken));

        Task<HttpResponseMessage> CreateUserPermissions(string virtualHostName, string userName,
                                                                        Action<UserPermissionsArgs> args,
                                                                        CancellationToken cancellationToken =
                                                                            default(CancellationToken));

        Task<HttpResponseMessage> DeleteUserPermissions(string virtualHostName, string userName,
                                                                        CancellationToken cancellationToken =
                                                                            default(CancellationToken));

        Task<HttpResponseMessage> GetUsers(CancellationToken cancellationToken =
                                                               default(CancellationToken));

        Task<HttpResponseMessage> GetUser(string userName, CancellationToken cancellationToken =
                                                                               default(CancellationToken));

        Task<HttpResponseMessage> CreateUser(string userName, Action<UserArgs> args,
                                                             CancellationToken cancellationToken =
                                                                 default(CancellationToken));

        Task<HttpResponseMessage> DeleteUser(string userName, CancellationToken cancellationToken =
                                                                                  default(CancellationToken));

        Task<HttpResponseMessage> GetListOfAllVirtualHosts(CancellationToken cancellationToken =
                                                                               default(CancellationToken));

        Task<HttpResponseMessage> CreateVirtualHost(string virtualHostName,
                                                                    CancellationToken cancellationToken =
                                                                        default(CancellationToken));

        Task<HttpResponseMessage> DeleteVirtualHost(string virtualHostName,
                                                                    CancellationToken cancellationToken =
                                                                        default(CancellationToken));

        Task<HttpResponseMessage> GetListOfAllQueues(
            CancellationToken cancellationToken = default(CancellationToken));

        Task<HttpResponseMessage> GetListOfAllBindingsOnQueue(string virtualHostName, string queueName,
                                                                              CancellationToken cancellationToken =
                                                                                  default(CancellationToken));

        Task<HttpResponseMessage> CreateQueue(string virtualHostName, string queueName,
                                                              Action<CreateQueueArgs> args,
                                                              CancellationToken cancellationToken =
                                                                  default(CancellationToken));

        Task<HttpResponseMessage> CreateQueue(string node, string virtualHostName, string queueName, Action<CreateQueueArgs> args, CancellationToken cancellationToken = default(CancellationToken));

        Task<HttpResponseMessage> BindQueueToExchange(string virtualHostName, string exchangeName,
                                                                      string queueName,
                                                                      Action<BindQueueArgs> args,
                                                                      CancellationToken cancellationToken =
                                                                          default(CancellationToken));

        Task<HttpResponseMessage> DeleteQueue(string virtualHostName, string queueName,
                                                              CancellationToken cancellationToken =
                                                                  default(CancellationToken));

        Task<HttpResponseMessage> GetListOfAllExchanges(CancellationToken cancellationToken =
                                                                            default(CancellationToken));

        Task<HttpResponseMessage> GetListOfAllExchangesInVirtualHost(string virtualHostName,
                                                                                     CancellationToken cancellationToken
                                                                                         =
                                                                                         default(CancellationToken));

        Task<HttpResponseMessage> GetExchange(string virtualHostName, string exchangeName,
                                                              CancellationToken cancellationToken =
                                                                  default(CancellationToken));

        Task<HttpResponseMessage> GetListOfAllBindingsOnExchange(string virtualHostName,
                                                                                 string exchangeName,
                                                                                 bool exchangeAsSource,
                                                                                 CancellationToken cancellationToken =
                                                                                     default(CancellationToken));

        Task<HttpResponseMessage> CreateExchange(string virtualHostName, string exchangeName,
                                                                 Action<CreateExchangeArgs> args = null,
                                                                 CancellationToken cancellationToken =
                                                                     default(CancellationToken));

        Task<HttpResponseMessage> DeleteExchange(string virtualHostName, string exchangeName,
                                                                 CancellationToken cancellationToken =
                                                                     default(CancellationToken));
    }
}