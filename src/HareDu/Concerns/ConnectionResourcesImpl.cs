// Copyright 2013-2014 Albert L. Hives, Chris Patterson, et al.
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

namespace HareDu.Concerns
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Async;
    using Common.Logging;
    using Model;

    internal class ConnectionResourcesImpl :
        HareDuResourcesBase,
        ConnectionResources
    {
        public ConnectionResourcesImpl(HttpClient client, ILog logger) :
            base(client, logger)
        {
            Channel = new ChannelClientImpl(client, logger);
        }

        public ChannelClient Channel { get; private set; }

        public Task<IEnumerable<Connection>> GetAll(CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            string url = "api/connections";

            LogInfo("Sent request to return all information pertaining to all connections on current RabbitMQ server.");

            return base.Get(url, cancellationToken).As<IEnumerable<Connection>>(cancellationToken);
        }

        public Task<Connection> Get(string connectionName,
                                    CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            string url = string.Format("api/connections/{0}", connectionName);

            LogInfo(
                string.Format(
                    "Sent request to return all information pertaining to connection '{0}' on current RabbitMQ server.",
                    connectionName));

            return base.Get(url, cancellationToken).As<Connection>(cancellationToken);
        }

        public Task<ServerResponse> Close(string connectionName,
                                          CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            string url = string.Format("api/connections/{0}", connectionName);

            LogInfo(
                string.Format(
                    "Sent request to return all information pertaining to connection '{0}' on current RabbitMQ server.",
                    connectionName));

            return base.Delete(url, cancellationToken).Response<ServerResponse>(cancellationToken);
        }
    }
}