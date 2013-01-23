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

    internal class ExchangeClientImpl :
        HareDuClientBase,
        ExchangeClient
    {
        public ExchangeClientImpl(ClientCharacteristicsImpl args) : base(args)
        {
        }

        public Task<IEnumerable<Exchange>> GetAll(CancellationToken cancellationToken = default(CancellationToken))
        {
            LogInfo(
                "Sent request to return all information pertaining to all exchanges on all virtual hosts on current RabbitMQ server.");

            string url = "api/exchanges";

            return base.Get(url, cancellationToken).As<IEnumerable<Exchange>>(cancellationToken);
        }

        public Task<Exchange> Get(string exchange, CancellationToken cancellationToken = default(CancellationToken))
        {
            Init.VirtualHost.Validate("VirtualHost.Init.VirtualHost", () => LogError(GetArgumentNullExceptionMsg, "Exchange.Get"));
            exchange.Validate("exchange", () => LogError(GetArgumentNullExceptionMsg, "Exchange.Get"));

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to return information pertaining to exchange '{0}' belonging to virtual host '{1}'.",
                    exchange, Init.VirtualHost));

            string url = string.Format("api/exchanges/{0}/{1}", Init.VirtualHost.SanitizeVirtualHostName(), exchange);

            return base.Get(url, cancellationToken).As<Exchange>(cancellationToken);
        }

        public Task<IEnumerable<Binding>> GetAllBindings(string exchange, Action<BindingDirection> args, CancellationToken cancellationToken = default(CancellationToken))
        {
            Init.VirtualHost.Validate("VirtualHost.Init.VirtualHost", () => LogError(GetArgumentNullExceptionMsg, "Exchange.GetAllBindings"));
            exchange.Validate("exchange", () => LogError(GetArgumentNullExceptionMsg, "Exchange.GetAllBindings"));

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to return all the bindings for exchange '{0}' belonging to virtual host '{1}'.",
                    exchange, Init.VirtualHost));

            var argsImpl = new BindingDirectionImpl();
            args(argsImpl);

            args.Validate("args", () => LogError(GetArgumentNullExceptionMsg, "Exchange.GetAllBindings"));

            string url = string.Format("api/exchanges/{0}/{1}/bindings/{2}",
                                       Init.VirtualHost.SanitizeVirtualHostName(), exchange, argsImpl.BindingDirection);

            return base.Get(url, cancellationToken).As<IEnumerable<Binding>>(cancellationToken);
        }

        public Task<CreateCmdResponse> New(string exchange, Action<ExchangeCharacteristics> args,
                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            Init.VirtualHost.Validate("VirtualHost.Init.VirtualHost", () => LogError(GetArgumentNullExceptionMsg, "Exchange.New"));
            exchange.Validate("exchange", () => LogError(GetArgumentNullExceptionMsg, "Exchange.New"));

            LogInfo(
                string.Format("Sent request to RabbitMQ server to create an exchange '{0}' within virtual host '{1}'.", exchange, Init.VirtualHost));

            args.Validate("args", () => LogError(GetArgumentNullExceptionMsg, "Exchange.New"));

            var argsImpl = new ExchangeCharacteristicsImpl();
            args(argsImpl);

            exchange.Validate("exchange", () => LogError(GetArgumentNullExceptionMsg, "Exchange.New"));
            argsImpl.RoutingType.Validate("routingType", () => LogError(GetArgumentNullExceptionMsg, "Exchange.New"));

            string url = string.Format("api/exchanges/{0}/{1}", Init.VirtualHost.SanitizeVirtualHostName(), exchange);

            return base.Put(url, argsImpl, cancellationToken).Response<CreateCmdResponse>(cancellationToken);
        }

        public Task<DeleteCmdResponse> Delete(string exchange, CancellationToken cancellationToken = default(CancellationToken))
        {
            Init.VirtualHost.Validate("VirtualHost.Init.VirtualHost", () => LogError(GetArgumentNullExceptionMsg, "Exchange.Delete"));
            exchange.Validate("exchange", () => LogError(GetArgumentNullExceptionMsg, "Exchange.Delete"));

            LogInfo(string.Format("Sent request to RabbitMQ server to delete exchange '{0}' from virtual host '{1}'.",
                                  exchange, Init.VirtualHost));

            string url = string.Format("api/exchanges/{0}/{1}", Init.VirtualHost.SanitizeVirtualHostName(), exchange);

            return base.Delete(url, cancellationToken).Response<DeleteCmdResponse>(cancellationToken);
        }
    }
}