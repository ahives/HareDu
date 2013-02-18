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

    internal class QueueBindingResourcesImpl :
        HareDuResourcesBase,
        QueueBindingResources
    {
        public QueueBindingResourcesImpl(HttpClient client, ILog logger) :
            base(client, logger)
        {
        }

        public Task<ServerResponse> New(Action<QueueBinding> binding, Action<QueueBindingBehavior> behavior,
                                        Action<VirtualHostTarget> virtualHost,
                                        CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var behaviorImpl = new QueueBindingBehaviorImpl();
            behavior(behaviorImpl);

            var bindingImpl = new QueueBindingImpl();
            binding(bindingImpl);

            var virtualHostTargetImpl = new VirtualHostTargetImpl();
            virtualHost(virtualHostTargetImpl);

            string url = string.Format("api/bindings/{0}/e/{1}/q/{2}",
                                       virtualHostTargetImpl.Target.SanitizeVirtualHostName(),
                                       bindingImpl.Exchange, bindingImpl.Queue);

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to bind queue '{0}' to exchange '{1}' belonging to virtual host '{2}'.",
                    bindingImpl.Queue, bindingImpl.Exchange, virtualHostTargetImpl.Target));

            return base.Post(url, behaviorImpl, cancellationToken).Response<ServerResponse>(cancellationToken);
        }

        public Task<ServerResponse> Delete(Action<QueueTarget> queue, Action<ExchangeTarget> exchange,
                                           Action<VirtualHostTarget> virtualHost,
                                           Action<PropertiesKeyTarget> propertiesKey,
                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var queueTargetImpl = new QueueTargetImpl();
            queue(queueTargetImpl);

            var virtualHostTargetImpl = new VirtualHostTargetImpl();
            virtualHost(virtualHostTargetImpl);

            var exchangeTargetImpl = new ExchangeTargetImpl();
            exchange(exchangeTargetImpl);

            var propertiesKeyTargetImpl = new PropertiesKeyTargetImpl();
            propertiesKey(propertiesKeyTargetImpl);

            string url = string.Format("api/bindings/{0}/e/{1}/q/{2}/{3}",
                                       virtualHostTargetImpl.Target.SanitizeVirtualHostName(),
                                       exchangeTargetImpl.Target, queueTargetImpl.Target,
                                       propertiesKeyTargetImpl.Target.SanitizePropertiesKey());

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to delete queue binding between queue '{0}' and exchange '{1}' in virtual host '{2}'.",
                    queueTargetImpl.Target, exchangeTargetImpl.Target, virtualHostTargetImpl.Target));

            return base.Delete(url, cancellationToken).Response<ServerResponse>(cancellationToken);
        }

        public Task<IEnumerable<Binding>> GetAll(Action<QueueTarget> queue, Action<VirtualHostTarget> virtualHost,
                                                 CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var queueTargetImpl = new QueueTargetImpl();
            queue(queueTargetImpl);

            var virtualHostTargetImpl = new VirtualHostTargetImpl();
            virtualHost(virtualHostTargetImpl);

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to return all bindings on queue '{0}' belonging to virtual host '{1}'.",
                    queueTargetImpl.Target, virtualHostTargetImpl.Target));

            string url = string.Format("api/queues/{0}/{1}/bindings",
                                       virtualHostTargetImpl.Target.SanitizeVirtualHostName(),
                                       queueTargetImpl.Target);

            return base.Get(url, cancellationToken).As<IEnumerable<Binding>>(cancellationToken);
        }

        public Task<Binding> Get(Action<QueueTarget> queue, Action<ExchangeTarget> exchange,
                                 Action<VirtualHostTarget> virtualHost, Action<PropertiesKeyTarget> propertiesKey,
                                 CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var queueTargetImpl = new QueueTargetImpl();
            queue(queueTargetImpl);

            var virtualHostTargetImpl = new VirtualHostTargetImpl();
            virtualHost(virtualHostTargetImpl);

            var exchangeTargetImpl = new ExchangeTargetImpl();
            exchange(exchangeTargetImpl);

            var propertiesKeyTargetImpl = new PropertiesKeyTargetImpl();
            propertiesKey(propertiesKeyTargetImpl);

            string url = string.Format("api/bindings/{0}/e/{1}/q/{2}/{3}",
                                       virtualHostTargetImpl.Target.SanitizeVirtualHostName(),
                                       exchangeTargetImpl.Target, queueTargetImpl.Target,
                                       propertiesKeyTargetImpl.Target.SanitizePropertiesKey());

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to return queue binding between queue '{0}' and exchange '{1}' in virtual host '{2}'.",
                    queueTargetImpl.Target, exchangeTargetImpl.Target, virtualHostTargetImpl.Target));

            return base.Get(url, cancellationToken).As<Binding>(cancellationToken);
        }
    }
}