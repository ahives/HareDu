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

    internal class ExchangeResourcesImpl :
        HareDuResourcesBase,
        ExchangeResources
    {
        public ExchangeResourcesImpl(HttpClient client, ILog logger) :
            base(client, logger)
        {
        }

        public Task<IEnumerable<Exchange>> GetAll(CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            LogInfo(
                "Sent request to return all information pertaining to all exchanges on all virtual hosts on current RabbitMQ server.");

            string url = "api/exchanges";

            return base.Get(url, cancellationToken).As<IEnumerable<Exchange>>(cancellationToken);
        }

        public Task<Exchange> Get(Action<ExchangeTarget> exchange, Action<VirtualHostTarget> virtualHost,
                                  CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var exchangeTargetImpl = new ExchangeTargetImpl();
            exchange(exchangeTargetImpl);

            var virtualHostTargetImpl = new VirtualHostTargetImpl();
            virtualHost(virtualHostTargetImpl);

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to return information pertaining to exchange '{0}' belonging to virtual host '{1}'.",
                    exchangeTargetImpl.Target, virtualHostTargetImpl.Target));

            string url = string.Format("api/exchanges/{0}/{1}", virtualHostTargetImpl.Target.SanitizeVirtualHostName(),
                                       exchangeTargetImpl.Target);

            return base.Get(url, cancellationToken).As<Exchange>(cancellationToken);
        }

        public Task<IEnumerable<Binding>> GetAllBindings(Action<ExchangeTarget> exchange,
                                                         Action<VirtualHostTarget> virtualHost,
                                                         Action<BindingDirection> direction,
                                                         CancellationToken cancellationToken =
                                                             default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var exchangeTargetImpl = new ExchangeTargetImpl();
            exchange(exchangeTargetImpl);

            var virtualHostTargetImpl = new VirtualHostTargetImpl();
            virtualHost(virtualHostTargetImpl);

            var directionImpl = new BindingDirectionImpl();
            direction(directionImpl);

            string url = string.Format("api/exchanges/{0}/{1}/bindings/{2}",
                                       virtualHostTargetImpl.Target.SanitizeVirtualHostName(), exchangeTargetImpl.Target,
                                       directionImpl.BindingDirection);

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to return all the bindings for exchange '{0}' belonging to virtual host '{1}'.",
                    exchangeTargetImpl.Target, virtualHostTargetImpl.Target));

            return base.Get(url, cancellationToken).As<IEnumerable<Binding>>(cancellationToken);
        }

        public Task<ServerResponse> New(Action<ExchangeTarget> exchange, Action<ExchangeBehavior> behavior,
                                        Action<VirtualHostTarget> virtualHost,
                                        CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var behaviorImpl = new ExchangeBehaviorImpl();
            behavior(behaviorImpl);

            var exchangeTargetImpl = new ExchangeTargetImpl();
            exchange(exchangeTargetImpl);

            var virtualHostTargetImpl = new VirtualHostTargetImpl();
            virtualHost(virtualHostTargetImpl);

            string url = string.Format("api/exchanges/{0}/{1}", virtualHostTargetImpl.Target.SanitizeVirtualHostName(),
                                       exchangeTargetImpl.Target);

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to create a new exchange '{0}' within virtual host '{1}'.",
                    exchangeTargetImpl.Target, virtualHostTargetImpl.Target));

            return base.Put(url, behaviorImpl, cancellationToken).Response<ServerResponse>(cancellationToken);
        }

        public Task<ServerResponse> Delete(Action<ExchangeTarget> exchange, Action<VirtualHostTarget> virtualHost,
                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var exchangeTargetImpl = new ExchangeTargetImpl();
            exchange(exchangeTargetImpl);

            var virtualHostTargetImpl = new VirtualHostTargetImpl();
            virtualHost(virtualHostTargetImpl);

            LogInfo(string.Format("Sent request to RabbitMQ server to delete exchange '{0}' from virtual host '{1}'.",
                                  exchangeTargetImpl.Target, virtualHostTargetImpl.Target));

            string url = string.Format("api/exchanges/{0}/{1}", virtualHostTargetImpl.Target.SanitizeVirtualHostName(),
                                       exchangeTargetImpl.Target);

            return base.Delete(url, cancellationToken).Response<ServerResponse>(cancellationToken);
        }
    }
}