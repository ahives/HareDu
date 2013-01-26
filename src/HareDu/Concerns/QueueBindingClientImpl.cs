// Copyright 2012-2013 Albert L. Hives, Chris Patterson, et al.
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
    using Async;
    using Contracts;
    using Internal;
    using Model;

    internal class QueueBindingClientImpl :
        HareDuClientBase,
        QueueBindingClient
    {
        public QueueBindingClientImpl(HareDuClientBehaviorImpl args) :
            base(args)
        {
        }

        public Task<ServerResponse> New(string queue, string exchange, Action<QueueBindingBehavior> args,
                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            Init.VirtualHost.Validate("Queue.Binding.New", "Queue.Binding.Init.VirtualHost", LogError);
            queue.Validate("Queue.Binding.New", "queue", LogError);
            exchange.Validate("Queue.Binding.New", "exchange", LogError);
            args.Validate("Queue.Binding.New", "args", LogError);

            var argsImpl = new QueueBindingBehaviorImpl();
            args(argsImpl);

            argsImpl.RoutingKey.Validate("Queue.Binding.New", "routingKey", LogError);

            string url = string.Format("api/bindings/{0}/e/{1}/q/{2}", Init.VirtualHost.SanitizeVirtualHostName(), exchange, queue);

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to bind queue '{0}' to exchange '{1}' belonging to virtual host '{2}'.",
                    queue, exchange, Init.VirtualHost));

            return base.Post(url, argsImpl, cancellationToken).Response<ServerResponse>(cancellationToken);
        }

        public Task<ServerResponse> Delete(string queue, string exchange, string propertiesKey,
                                              CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            Init.VirtualHost.Validate("Queue.Binding.Init.Delete", "Init.VirtualHost", LogError);
            queue.Validate("Queue.Binding.Delete", "queue", LogError);
            exchange.Validate("Queue.Binding.Delete", "exchange", LogError);
            propertiesKey.Validate("Queue.Binding.Delete", "routingKey", LogError);

            string url = string.Format("api/bindings/{0}/e/{1}/q/{2}/{3}", Init.VirtualHost.SanitizeVirtualHostName(),
                                       exchange, queue, propertiesKey.SanitizePropertiesKey());

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to delete queue binding between queue '{0}' and exchange '{1}' in virtual host '{2}'.",
                    queue, exchange, Init.VirtualHost));

            return base.Delete(url, cancellationToken).Response<ServerResponse>(cancellationToken);
        }

        public Task<IEnumerable<Binding>> GetAll(string queue, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            Init.VirtualHost.Validate("Queue.Binding.GetAll", "Init.VirtualHost", LogError);
            queue.Validate("Queue.Binding.GetAll", "queue", LogError);

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to return all bindings on queue '{0}' belonging to virtual host '{1}'.",
                    queue, Init.VirtualHost));

            string url = string.Format("api/queues/{0}/{1}/bindings", Init.VirtualHost.SanitizeVirtualHostName(), queue);

            return base.Get(url, cancellationToken).As<IEnumerable<Binding>>(cancellationToken);
        }

        public Task<Binding> Get(string queue, string exchange, string propertiesKey,
                                 CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            Init.VirtualHost.Validate("Queue.Binding.Get", "Init.VirtualHost", LogError);
            queue.Validate("Queue.Binding.Get", "queue", LogError);
            exchange.Validate("Queue.Binding.Get", "exchange", LogError);
            propertiesKey.Validate("Queue.Binding.Get", "routingKey", LogError);

            string url = string.Format("api/bindings/{0}/e/{1}/q/{2}/{3}", Init.VirtualHost.SanitizeVirtualHostName(),
                                       exchange, queue, propertiesKey.SanitizePropertiesKey());

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to return queue binding between queue '{0}' and exchange '{1}' in virtual host '{2}'.",
                    queue, exchange, Init.VirtualHost));

            return base.Get(url, cancellationToken).As<Binding>(cancellationToken);
        }
    }
}