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

namespace HareDu.Resources
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Async;
    using Common.Logging;
    using Contracts;
    using Internal;
    using Model;

    internal class VirtualHostResourcesImpl :
        HareDuResourcesBase,
        VirtualHostResources
    {
        public VirtualHostResourcesImpl(HttpClient client, ILog logger) :
            base(client, logger)
        {
            Exchange = new ExchangeResourcesImpl(client, logger);
            Queue = new QueueResourcesImpl(client, logger);
            QueueExchangeBindings = new QueueBindingResourcesImpl(client, logger);
        }

        public ExchangeResources Exchange { get; private set; }
        public QueueResources Queue { get; private set; }
        public QueueBindingResources QueueExchangeBindings { get; private set; }

        public Task<IEnumerable<VirtualHost>> GetAll(CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            string url = "api/vhosts";

            LogInfo("Sent request to return all information on all virtual hosts on current RabbitMQ server.");

            return base.Get(url, cancellationToken).As<IEnumerable<VirtualHost>>(cancellationToken);
        }

        public Task<ServerResponse> New(Action<VirtualHostTarget> virtualHost,
                                        CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var virtualHostTargetImpl = new VirtualHostTargetImpl();
            virtualHost(virtualHostTargetImpl);

            string url = string.Format("api/vhosts/{0}", virtualHostTargetImpl.Target.SanitizeVirtualHostName());

            LogInfo(string.Format("Sent request to RabbitMQ server to create virtual host '{0}'.",
                                  virtualHostTargetImpl.Target));

            return
                base.Put(url, new StringContent(string.Empty), cancellationToken)
                    .Response<ServerResponse>(cancellationToken);
        }

        public Task<ServerResponse> Delete(Action<VirtualHostTarget> virtualHost,
                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var virtualHostTargetImpl = new VirtualHostTargetImpl();
            virtualHost(virtualHostTargetImpl);

            if (virtualHostTargetImpl.Target.SanitizeVirtualHostName() == "2%f")
            {
                LogError(
                    "VirtualHost.Delete method threw a CannotDeleteVirtualHostException exception for attempting to delete the default virtual host.");
                throw new CannotDeleteVirtualHostException("Cannot delete the default virtual host.");
            }

            string url = string.Format("api/vhosts/{0}", virtualHostTargetImpl.Target.SanitizeVirtualHostName());

            LogInfo(string.Format("Sent request to RabbitMQ server to delete virtual host '{0}'.",
                                  virtualHostTargetImpl.Target));

            return base.Delete(url, cancellationToken).Response<ServerResponse>(cancellationToken);
        }

        public Task<AlivenessTestResponse> IsAlive(Action<VirtualHostTarget> virtualHost,
                                                   CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var virtualHostTargetImpl = new VirtualHostTargetImpl();
            virtualHost(virtualHostTargetImpl);

            string url = string.Format("api/aliveness-test/{0}", virtualHostTargetImpl.Target.SanitizeVirtualHostName());

            LogInfo(
                string.Format(
                    "Sent request to execute an aliveness test on virtual host '{0}' on current RabbitMQ server.",
                    virtualHostTargetImpl.Target));

            return base.Get(url, cancellationToken)
                       .ContinueWith(t =>
                                         {
                                             t.Result.EnsureSuccessStatusCode();
                                             var response = t.Result.Content.ReadAsAsync<AlivenessTestResponse>().Result;
                                             response.StatusCode = t.Result.StatusCode;
                                             response.ServerResponseReason = t.Result.ReasonPhrase;

                                             return response;
                                         }, cancellationToken, TaskContinuationOptions.OnlyOnRanToCompletion,
                                     TaskScheduler.Current);
        }
    }
}