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

    internal class QueueBindingClientImpl :
        HareDuResourcesBase,
        QueueBindingClient
    {
        public QueueBindingClientImpl(HttpClient client, ILog logger) :
            base(client, logger)
        {
        }

        public Task<ServerResponse> New(Action<QueueBinding> target, Action<QueueBindingBehavior> behavior,
                                        CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var behaviorImpl = new QueueBindingBehaviorImpl();
            behavior(behaviorImpl);

            var targetImpl = new QueueBindingImpl();
            target(targetImpl);

            string url = string.Format("api/bindings/{0}/e/{1}/q/{2}", targetImpl.VirtualHostName.SanitizeVirtualHostName(),
                                       targetImpl.Exchange, targetImpl.Queue);

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to bind queue '{0}' to exchange '{1}' belonging to virtual host '{2}'.",
                    targetImpl.Queue, targetImpl.Exchange, targetImpl.VirtualHostName));

            return base.Post(url, behaviorImpl, cancellationToken).Response<ServerResponse>(cancellationToken);
        }

        public Task<ServerResponse> Delete(Action<QueueBindingTarget> target, string propertiesKey,
                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var targetImpl = new QueueBindingTargetImpl();
            target(targetImpl);

            string url = string.Format("api/bindings/{0}/e/{1}/q/{2}/{3}", targetImpl.VirtualHost.SanitizeVirtualHostName(),
                                       targetImpl.Exchange, targetImpl.Queue, propertiesKey.SanitizePropertiesKey());

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to delete queue binding between queue '{0}' and exchange '{1}' in virtual host '{2}'.",
                    targetImpl.Queue, targetImpl.Exchange, targetImpl.VirtualHost));

            return base.Delete(url, cancellationToken).Response<ServerResponse>(cancellationToken);
        }

        public Task<IEnumerable<Binding>> GetAll(Action<QueueBindingTarget> target,
                                                 CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var targetImpl = new QueueBindingTargetImpl();
            target(targetImpl);

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to return all bindings on queue '{0}' belonging to virtual host '{1}'.",
                    targetImpl.Queue, targetImpl.VirtualHost));

            string url = string.Format("api/queues/{0}/{1}/bindings", targetImpl.VirtualHost.SanitizeVirtualHostName(), targetImpl.Queue);

            return base.Get(url, cancellationToken).As<IEnumerable<Binding>>(cancellationToken);
        }

        public Task<Binding> Get(Action<QueueBindingTarget> target, string propertiesKey,
                                 CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var targetImpl = new QueueBindingTargetImpl();
            target(targetImpl);

            string url = string.Format("api/bindings/{0}/e/{1}/q/{2}/{3}", targetImpl.VirtualHost.SanitizeVirtualHostName(),
                                       targetImpl.Exchange, targetImpl.Queue, propertiesKey.SanitizePropertiesKey());

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to return queue binding between queue '{0}' and exchange '{1}' in virtual host '{2}'.",
                    targetImpl.Queue, targetImpl.Exchange, targetImpl.VirtualHost));

            return base.Get(url, cancellationToken).As<Binding>(cancellationToken);
        }
    }

    public interface QueueBindingTarget
    {
        void Source(string queue, string virtualHost);
        void Source(string queue, string exchange, string virtualHost);
    }

    internal class QueueBindingTargetImpl :
        QueueBindingTarget
    {
        public string Exchange { get; private set; }

        public string Queue { get; private set; }

        public string VirtualHost { get; private set; }
        public void Source(string queue, string virtualHost)
        {
            Queue = queue;
            VirtualHost = virtualHost;
        }
        public void Source(string queue, string exchange, string virtualHost)
        {
            Queue = queue;
            Exchange = exchange;
            VirtualHost = virtualHost;
        }
    }

    public interface QueueBinding
    {
        void Bind(string queue, string exchange);
        void VirtualHost(string virtualHost);
    }

    internal class QueueBindingImpl : QueueBinding
    {
        public void Bind(string queue, string exchange)
        {
            Queue = queue;
            Exchange = exchange;
        }

        public string Queue { get; private set; }

        public string Exchange { get; private set; }

        public void VirtualHost(string virtualHost)
        {
            VirtualHostName = virtualHost;
        }

        public string VirtualHostName { get; private set; }
    }
}