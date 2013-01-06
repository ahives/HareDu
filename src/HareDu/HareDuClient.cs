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
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using Model;

    public interface HareDuClient
    {
        /// <summary>
        /// 
        /// </summary>
        void CancelPendingRequests();

        /// <summary>
        /// Returns a list of open connections to the RabbitMQ server.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<Connection>> GetAllConnections(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Connection> GetConnection(string connectionName,
                                       CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<Channel>> GetAllChannels(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="channelName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Channel> GetChannel(string channelName, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualHostName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<HealthCheckResponse> IsAlive(string virtualHostName,
                                          CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualHostName"></param>
        /// <param name="userName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<UserPermissions> GetIndividualUserPermissions(string virtualHostName, string userName,
                                                           CancellationToken cancellationToken =
                                                               default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<UserPermissions>> GetAlllUserPermissions(
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="virtualHostName"></param>
        /// <param name="args"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<AsyncResponse> CreateUserPermissions(string userName, string virtualHostName,
                                                  Action<UserPermissionsArgs> args,
                                                  CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="virtualHostName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<AsyncResponse> DeleteUserPermissions(string userName, string virtualHostName,
                                                  CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<User>> GetAllUsers(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<User> GetIndividualUser(string userName, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="args"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<AsyncResponse> CreateUser(string userName, Action<UserArgs> args,
                                       CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<AsyncResponse> DeleteUser(string userName, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<VirtualHost>> GetAllVirtualHosts(
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualHostName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<AsyncResponse> CreateVirtualHost(string virtualHostName,
                                              CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualHostName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<AsyncResponse> DeleteVirtualHost(string virtualHostName,
                                              CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<Queue>> GetAllQueues(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="virtualHostName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<Binding>> GetAllBindingsOnQueue(string queueName, string virtualHostName,
                                                         CancellationToken cancellationToken =
                                                             default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="virtualHostName"></param>
        /// <param name="args"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<AsyncResponse> CreateQueue(string queueName, string virtualHostName, Action<CreateQueueArgs> args,
                                        CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="node"></param>
        /// <param name="virtualHostName"></param>
        /// <param name="args"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<AsyncResponse> CreateQueue(string queueName, string node, string virtualHostName,
                                        Action<CreateQueueArgs> args,
                                        CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="exchangeName"></param>
        /// <param name="virtualHostName"></param>
        /// <param name="args"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<AsyncResponse> BindQueueToExchange(string queueName, string exchangeName, string virtualHostName,
                                                Action<BindQueueArgs> args,
                                                CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualHostName"></param>
        /// <param name="queueName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<AsyncResponse> DeleteQueue(string virtualHostName, string queueName,
                                        CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<Exchange>> GetAllExchanges(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualHostName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<Exchange>> GetAllExchangesInVirtualHost(string virtualHostName,
                                                                 CancellationToken cancellationToken =
                                                                     default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exchangeName"></param>
        /// <param name="virtualHostName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Exchange> GetExchange(string exchangeName, string virtualHostName,
                                   CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exchangeName"></param>
        /// <param name="virtualHostName"></param>
        /// <param name="isSource"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<Binding>> GetAllBindingsOnExchange(string exchangeName, string virtualHostName, bool isSource,
                                                            CancellationToken cancellationToken =
                                                                default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exchangeName"></param>
        /// <param name="virtualHostName"></param>
        /// <param name="args"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<AsyncResponse> CreateExchange(string exchangeName, string virtualHostName,
                                           Action<CreateExchangeArgs> args = null,
                                           CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exchangeName"></param>
        /// <param name="virtualHostName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<AsyncResponse> DeleteExchange(string exchangeName, string virtualHostName,
                                           CancellationToken cancellationToken = default(CancellationToken));
    }
}