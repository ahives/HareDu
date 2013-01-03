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
        Task<HttpResponseMessage> GetAllConnections(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetConnection(string connectionName, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetAllChannels(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="channelName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetChannel(string channelName, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualHostName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> ExecuteAlivenessTest(string virtualHostName, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualHostName"></param>
        /// <param name="userName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetIndividualUserPermissions(string virtualHostName, string userName, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetAlllUserPermissions(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="virtualHostName"></param>
        /// <param name="args"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> CreateUserPermissions(string userName, string virtualHostName, Action<UserPermissionsArgs> args, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="virtualHostName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> DeleteUserPermissions(string userName, string virtualHostName, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetAllUsers(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetIndividualUser(string userName, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="args"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> CreateUser(string userName, Action<UserArgs> args, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> DeleteUser(string userName, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<VirtualHost>> GetAllVirtualHosts(CancellationToken cancellationToken =
                                                                             default(CancellationToken));
        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualHostName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<AsyncResponse> CreateVirtualHost(string virtualHostName, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualHostName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        AsyncResponse DeleteVirtualHost(string virtualHostName, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        AsyncResponse<IEnumerable<Queue>> GetAllQueues(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="virtualHostName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        AsyncResponse<IEnumerable<Binding>> GetAllBindingsOnQueue(string queueName, string virtualHostName, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualHostName"></param>
        /// <param name="queueName"></param>
        /// <param name="args"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        AsyncResponse CreateQueue(string virtualHostName, string queueName, Action<CreateQueueArgs> args, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="virtualHostName"></param>
        /// <param name="queueName"></param>
        /// <param name="args"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        AsyncResponse CreateQueue(string node, string virtualHostName, string queueName, Action<CreateQueueArgs> args, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="exchangeName"></param>
        /// <param name="virtualHostName"></param>
        /// <param name="args"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        AsyncResponse BindQueueToExchange(string queueName, string exchangeName, string virtualHostName, Action<BindQueueArgs> args, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualHostName"></param>
        /// <param name="queueName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        AsyncResponse DeleteQueue(string virtualHostName, string queueName, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        AsyncResponse<IEnumerable<Exchange>> GetAllExchanges(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualHostName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        AsyncResponse<IEnumerable<Exchange>> GetAllExchangesInVirtualHost(string virtualHostName, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exchangeName"></param>
        /// <param name="virtualHostName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        AsyncResponse<Exchange> GetExchange(string exchangeName, string virtualHostName, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exchangeName"></param>
        /// <param name="virtualHostName"></param>
        /// <param name="isSource"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        AsyncResponse<IEnumerable<Binding>> GetAllBindingsOnExchange(string exchangeName, string virtualHostName, bool isSource, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exchangeName"></param>
        /// <param name="virtualHostName"></param>
        /// <param name="args"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        AsyncResponse CreateExchange(string exchangeName, string virtualHostName, Action<CreateExchangeArgs> args = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exchangeName"></param>
        /// <param name="virtualHostName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<AsyncResponse> DeleteExchange(string exchangeName, string virtualHostName, CancellationToken cancellationToken = default(CancellationToken));
    }
}