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
    using Contracts;
    using Internal;
    using Model;

    internal class QueueBindingClientImpl :
        HareDuClientBase,
        QueueBindingClient
    {
        public QueueBindingClientImpl(ClientInitParamsImpl args) : base(args)
        {
        }

        public Task<CreateCmdResponse> New(string queueName, string exchangeName, Action<BindQueueParams> args,
                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(Init.VirtualHost, "virtualHostName",
                         () =>
                         LogError(
                             "Queue.Binding.New method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(queueName, "queueName",
                         () =>
                         LogError(
                             "Queue.Binding.New method threw an ArgumentNullException exception because queue name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(exchangeName, "exchangeName",
                         () =>
                         LogError(
                             "Queue.Binding.New method threw an ArgumentNullException exception because exchange name was invalid (i.e. empty, null, or all whitespaces)"));

            if (args == null)
                throw new ArgumentNullException("args");

            var queueBinding = new BindQueueParamsImpl();
            args(queueBinding);

            Arg.Validate(queueBinding.RoutingKey, "routingKey",
                         () =>
                         LogError(
                             "Queue.Binding.New method threw an ArgumentNullException exception because routing key was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/bindings/{0}/e/{1}/q/{2}", Init.VirtualHost.SanitizeVirtualHostName(),
                                       exchangeName, queueName);

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to bind queue '{0}' to exchange '{1}' belonging to virtual host '{2}'.",
                    queueName, exchangeName, Init.VirtualHost));

            return base.Post(url, queueBinding, cancellationToken).Response<CreateCmdResponse>(cancellationToken);
        }

        public Task<DeleteCmdResponse> Delete(string queueName, string exchangeName, string propertiesKey,
                                              CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(Init.VirtualHost, "virtualHostName",
                         () =>
                         LogError(
                             "Queue.Binding.Binding.Delete method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(queueName, "queueName",
                         () =>
                         LogError(
                             "Queue.Binding.Binding.Delete method threw an ArgumentNullException exception because queue name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(exchangeName, "exchangeName",
                         () =>
                         LogError(
                             "Queue.Binding.Binding.Delete method threw an ArgumentNullException exception because exchange name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(propertiesKey, "propertiesKey",
                         () =>
                         LogError(
                             "Queue.Binding.Binding.Delete method threw an ArgumentNullException exception because exchange name was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/bindings/{0}/e/{1}/q/{2}/{3}", Init.VirtualHost.SanitizeVirtualHostName(),
                                       exchangeName, queueName, propertiesKey.SanitizePropertiesKey());

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to delete queue binding between queue '{0}' and exchange '{1}' in virtual host '{2}'.",
                    queueName, exchangeName, Init.VirtualHost));

            return base.Delete(url, cancellationToken).Response<DeleteCmdResponse>(cancellationToken);
        }

        public Task<IEnumerable<Binding>> GetAll(string queueName,
                                                 CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(Init.VirtualHost, "virtualHostName",
                         () =>
                         LogError(
                             "Queue.Binding.GetAll method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(queueName, "queueName",
                         () =>
                         LogError(
                             "Queue.Binding.GetAll method threw an ArgumentNullException exception because queue name was invalid (i.e. empty, null, or all whitespaces)"));

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to return all bindings on queue '{0}' belonging to virtual host '{1}'.",
                    queueName, Init.VirtualHost));

            string url = string.Format("api/queues/{0}/{1}/bindings", Init.VirtualHost.SanitizeVirtualHostName(),
                                       queueName);

            return base.Get(url, cancellationToken).As<IEnumerable<Binding>>(cancellationToken);
        }

        public Task<Binding> Get(string queueName, string exchangeName, string propertiesKey,
                                 CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(Init.VirtualHost, "virtualHostName",
                         () =>
                         LogError(
                             "Queue.Binding.Delete method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(queueName, "queueName",
                         () =>
                         LogError(
                             "Queue.Binding.Delete method threw an ArgumentNullException exception because queue name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(exchangeName, "exchangeName",
                         () =>
                         LogError(
                             "Queue.Binding.Delete method threw an ArgumentNullException exception because exchange name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(propertiesKey, "propertiesKey",
                         () =>
                         LogError(
                             "Queue.Binding.Delete method threw an ArgumentNullException exception because exchange name was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/bindings/{0}/e/{1}/q/{2}/{3}", Init.VirtualHost.SanitizeVirtualHostName(),
                                       exchangeName, queueName, propertiesKey.SanitizePropertiesKey());

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to return queue binding between queue '{0}' and exchange '{1}' in virtual host '{2}'.",
                    queueName, exchangeName, Init.VirtualHost));

            return base.Get(url, cancellationToken).As<Binding>(cancellationToken);
        }
    }
}