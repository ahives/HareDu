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

    internal class QueueResourcesImpl :
        HareDuResourcesBase,
        QueueResources
    {
        public QueueResourcesImpl(HttpClient client, ILog logger) :
            base(client, logger)
        {
        }

        public Task<IEnumerable<Queue>> GetAll(CancellationToken cancellationToken = default(CancellationToken))
        {
            LogInfo(
                "Sent request to return all information on all queues on all virtual hosts on current RabbitMQ server.");

            string url = "api/queues";

            return base.Get(url, cancellationToken).As<IEnumerable<Queue>>(cancellationToken);
        }

        public Task<ServerResponse> New(Action<QueueTarget> queue, Action<QueueBehavior> behavior,
                                        Action<VirtualHostTarget> virtualHost,
                                        CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var queueTargetImpl = new QueueTargetImpl();
            queue(queueTargetImpl);

            var behaviorImpl = new QueueBehaviorImpl();
            behavior(behaviorImpl);

            var virtualHostTargetImpl = new VirtualHostTargetImpl();
            virtualHost(virtualHostTargetImpl);

            string url = string.Format("api/queues/{0}/{1}", virtualHostTargetImpl.Target.SanitizeVirtualHostName(),
                                       queueTargetImpl.Target);

            LogInfo(string.IsNullOrEmpty(behaviorImpl.Node)
                        ? string.Format(
                            "Sent request to RabbitMQ server to create queue '{0}' on virtual host '{1}'.",
                            queueTargetImpl.Target,
                            virtualHostTargetImpl.Target)
                        : string.Format(
                            "Sent request to RabbitMQ server to create queue '{0}' on virtual host '{1}' on node '{2}'.",
                            queueTargetImpl.Target, virtualHostTargetImpl.Target, behaviorImpl.Node));

            return base.Put(url, behaviorImpl, cancellationToken).Response<ServerResponse>(cancellationToken);
        }

        public Task<ServerResponse> Delete(Action<QueueTarget> queue, Action<VirtualHostTarget> virtualHost,
                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var queueTargetImpl = new QueueTargetImpl();
            queue(queueTargetImpl);

            var virtualHostTargetImpl = new VirtualHostTargetImpl();
            virtualHost(virtualHostTargetImpl);

            string url = string.Format("api/queues/{0}/{1}", virtualHostTargetImpl.Target.SanitizeVirtualHostName(),
                                       queueTargetImpl.Target);

            LogInfo(string.Format("Sent request to RabbitMQ server to delete queue '{0}' from virtual host '{1}'.",
                                  queueTargetImpl.Target, virtualHostTargetImpl.Target));

            return base.Delete(url, cancellationToken).Response<ServerResponse>(cancellationToken);
        }

        public Task<ServerResponse> Clear(Action<QueueTarget> queue, Action<VirtualHostTarget> virtualHost,
                                          CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var queueTargetImpl = new QueueTargetImpl();
            queue(queueTargetImpl);

            var virtualHostTargetImpl = new VirtualHostTargetImpl();
            virtualHost(virtualHostTargetImpl);

            string url = string.Format("api/queues/{0}/{1}/contents",
                                       virtualHostTargetImpl.Target.SanitizeVirtualHostName(),
                                       queueTargetImpl.Target);

            LogInfo(string.Format("Sent request to RabbitMQ server to purge the contents of queue '{0}'.",
                                  queueTargetImpl.Target));

            return base.Delete(url, cancellationToken).Response<ServerResponse>(cancellationToken);
        }
    }
}