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
        Task<AlivenessTestResponse> IsAlive(string virtualHostName,
                                          CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualHostName"></param>
        /// <param name="userName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<UserPermissions> GetUserPermissions(string virtualHostName, string userName,
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
        Task<ModifyResponse> SetUserPermissions(string userName, string virtualHostName,
                                                  Action<PermissionsCreateParams> args,
                                                  CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="virtualHostName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ModifyResponse> DeleteUserPermissions(string userName, string virtualHostName,
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
        Task<User> GetUser(string userName, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Creates a user.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="args"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ModifyResponse> CreateUser(string userName, Action<UserCreateParams> args,
                                       CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete the specified user.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ModifyResponse> DeleteUser(string userName, CancellationToken cancellationToken = default(CancellationToken));

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
        Task<ModifyResponse> CreateVirtualHost(string virtualHostName,
                                              CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Deletes the specified virtual host.
        /// </summary>
        /// <param name="virtualHostName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ModifyResponse> DeleteVirtualHost(string virtualHostName,
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
        /// Creates a queue within the specified virtual host on the current node.
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="virtualHostName"></param>
        /// <param name="args"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ModifyResponse> CreateQueue(string queueName, string virtualHostName, Action<QueueCreateParams> args,
                                        CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Creates a queue within the specified virtual host on the specified node.
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="node"></param>
        /// <param name="virtualHostName"></param>
        /// <param name="args"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ModifyResponse> CreateQueue(string queueName, string node, string virtualHostName,
                                        Action<QueueCreateParams> args,
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
        Task<ModifyResponse> BindQueueToExchange(string queueName, string exchangeName, string virtualHostName,
                                                Action<QueueBindParams> args,
                                                CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Deletes the specified queue form the specified virtual host.
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="virtualHostName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ModifyResponse> DeleteQueue(string queueName, string virtualHostName, CancellationToken cancellationToken = default(CancellationToken));

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
        /// Creates an exchange on the specified virtual host.
        /// </summary>
        /// <param name="exchangeName"></param>
        /// <param name="virtualHostName"></param>
        /// <param name="args"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ModifyResponse> CreateExchange(string exchangeName, string virtualHostName,
                                           Action<ExchangeCreateParams> args = null,
                                           CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Deletes an exchange from the specified virtual host.
        /// </summary>
        /// <param name="exchangeName"></param>
        /// <param name="virtualHostName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ModifyResponse> DeleteExchange(string exchangeName, string virtualHostName,
                                           CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<Node>> GetAllNodesOnCluster(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Returns the information pertaining to a specific node in the RabbitMQ cluster.
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Node> GetNodeOnCluster(string nodeName, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Overview> GetOverview(CancellationToken cancellationToken = default(CancellationToken));
    }
}