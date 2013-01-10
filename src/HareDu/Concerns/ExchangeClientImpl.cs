﻿// Copyright 2012-2013 Albert L. Hives, Chris Patterson, et al.
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
    using Internal;
    using Model;

    internal class ExchangeClientImpl :
        HareDuClientBase,
        ExchangeClient
    {
        public ExchangeClientImpl(ClientInitParamsImpl args) : base(args)
        {
        }

        public Task<IEnumerable<Exchange>> GetAll(CancellationToken cancellationToken = default(CancellationToken))
        {
            LogInfo(
                "Sent request to return all information pertaining to all exchanges on all virtual hosts on current RabbitMQ server.");

            string url = "api/exchanges";

            return base.Get(url, cancellationToken).Response<IEnumerable<Exchange>>(cancellationToken);
        }

        public Task<Exchange> Get(string exchangeName, CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(Init.VirtualHost, "virtualHostName",
                         () =>
                         LogError(
                             "Exchange.Get method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(exchangeName, "exchangeName",
                         () =>
                         LogError(
                             "Exchange.Get method threw an ArgumentNullException exception because exchange name was invalid (i.e. empty, null, or all whitespaces)"));

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to return information pertaining to exchange '{0}' belonging to virtual host '{1}'.",
                    exchangeName, Init.VirtualHost));

            string url = string.Format("api/exchanges/{0}/{1}", Init.VirtualHost.SanitizeVirtualHostName(),
                                       exchangeName);

            return base.Get(url, cancellationToken).Response<Exchange>(cancellationToken);
        }

        public Task<IEnumerable<Binding>> GetAllBindings(string exchangeName, bool isSource,
                                                           CancellationToken cancellationToken =
                                                               default(CancellationToken))
        {
            Arg.Validate(Init.VirtualHost, "virtualHostName",
                         () =>
                         LogError(
                             "Exchange.GetAllBindings method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(exchangeName, "exchangeName",
                         () =>
                         LogError(
                             "Exchange.GetAllBindings method threw an ArgumentNullException exception because exchange name was invalid (i.e. empty, null, or all whitespaces)"));

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to return all the bindings for exchange '{0}' belonging to virtual host '{1}'.",
                    exchangeName, Init.VirtualHost));

            string url = string.Format("api/exchanges/{0}/{1}/bindings/{2}",
                                       Init.VirtualHost.SanitizeVirtualHostName(), exchangeName,
                                       isSource ? "source" : "destination");

            return base.Get(url, cancellationToken).Response<IEnumerable<Binding>>(cancellationToken);
        }

        public Task<ModifyResponse> Create(string exchangeName, Action<ExchangeCreateParams> args = null,
                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(Init.VirtualHost, "virtualHostName",
                         () =>
                         LogError(
                             "Exchange.Create method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(exchangeName, "exchangeName",
                         () =>
                         LogError(
                             "Exchange.Create method threw an ArgumentNullException exception because exchange name was invalid (i.e. empty, null, or all whitespaces)"));

            LogInfo(
                string.Format("Sent request to RabbitMQ server to create an exchange '{0}' within virtual host '{1}'.",
                              exchangeName, Init.VirtualHost));

            if (args == null)
                throw new ArgumentNullException("args");

            var exchange = new ExchangeCreateParamsImpl();
            args(exchange);

            Arg.Validate(exchange.RoutingType, "routingType",
                         () =>
                         LogError(
                             "Exchange.CreateExchange method threw an ArgumentNullException exception because routing type was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/exchanges/{0}/{1}", Init.VirtualHost.SanitizeVirtualHostName(),
                                       exchangeName);

            return base.Put(url, exchange, cancellationToken).Response(cancellationToken);
        }

        public Task<ModifyResponse> Delete(string exchangeName,
                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(Init.VirtualHost, "virtualHostName",
                         () =>
                         LogError(
                             "Exchange.Delete method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(exchangeName, "exchangeName",
                         () =>
                         LogError(
                             "Exchange.Delete method threw an ArgumentNullException exception because exchange name was invalid (i.e. empty, null, or all whitespaces)"));

            LogInfo(string.Format("Sent request to RabbitMQ server to delete exchange '{0}' from virtual host '{1}'.",
                                  exchangeName, Init.VirtualHost));

            string url = string.Format("api/exchanges/{0}/{1}", Init.VirtualHost.SanitizeVirtualHostName(),
                                       exchangeName);

            return base.Delete(url, cancellationToken).Response(cancellationToken);
        }
    }
}