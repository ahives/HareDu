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

    internal class QueueClientImpl :
        HareDuClientBase,
        QueueClient
    {
        public QueueClientImpl(ClientInitParamsImpl args)
            : base(args)
        {
        }

        public Task<IEnumerable<Queue>> GetAll(CancellationToken cancellationToken = new CancellationToken())
        {
            LogInfo(
                "Sent request to return all information on all queues on all virtual hosts on current RabbitMQ server.");

            string url = "api/queues";

            return base.Get(url, cancellationToken).Response<IEnumerable<Queue>>(cancellationToken);
        }

        public Task<IEnumerable<Binding>> GetAllBindings(string queueName,
                                                         CancellationToken cancellationToken = new CancellationToken())
        {
            Arg.Validate(InitParams.VirtualHost, "virtualHostName",
                         () =>
                         LogError(
                             "Queue.GetAllBindings method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(queueName, "queueName",
                         () =>
                         LogError(
                             "Queue.GetAllBindings method threw an ArgumentNullException exception because queue name was invalid (i.e. empty, null, or all whitespaces)"));

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to return all bindings on queue '{0}' belonging to virtual host '{1}'.",
                    queueName, InitParams.VirtualHost));

            string url = string.Format("api/queues/{0}/{1}/bindings", InitParams.VirtualHost.SanitizeVirtualHostName(),
                                       queueName);

            return Get(url, cancellationToken).Response<IEnumerable<Binding>>(cancellationToken);
        }

        public Task<ModifyResponse> Create(string queueName, Action<QueueCreateParams> args,
                                           CancellationToken cancellationToken = new CancellationToken())
        {
            Arg.Validate(InitParams.VirtualHost, "virtualHostName",
                         () =>
                         LogError(
                             "Queue.Create method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(queueName, "queueName",
                         () =>
                         LogError(
                             "Queue.Create method threw an ArgumentNullException exception because queue name was invalid (i.e. empty, null, or all whitespaces)"));

            if (args == null)
                throw new ArgumentNullException("args");

            var queue = new QueueCreateParamsImpl();
            args(queue);

            string url = string.Format("api/queues/{0}/{1}", InitParams.VirtualHost.SanitizeVirtualHostName(), queueName);

            LogInfo(string.IsNullOrEmpty(queue.Node)
                        ? string.Format(
                            "Sent request to RabbitMQ server to create queue '{0}' on virtual host '{1}'.", queueName,
                            InitParams.VirtualHost)
                        : string.Format(
                            "Sent request to RabbitMQ server to create queue '{0}' on virtual host '{1}' on node '{2}'.",
                            queueName, InitParams.VirtualHost, queue.Node));

            return base.Put(url, queue, cancellationToken).Response(cancellationToken);
        }

        public Task<ModifyResponse> BindToExchange(string queueName, string exchangeName, Action<QueueBindParams> args,
                                                   CancellationToken cancellationToken = new CancellationToken())
        {
            Arg.Validate(InitParams.VirtualHost, "virtualHostName",
                         () =>
                         LogError(
                             "Queue.BindToExchange method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(queueName, "queueName",
                         () =>
                         LogError(
                             "Queue.BindToExchange method threw an ArgumentNullException exception because queue name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(exchangeName, "exchangeName",
                         () =>
                         LogError(
                             "Queue.BindToExchange method threw an ArgumentNullException exception because exchange name was invalid (i.e. empty, null, or all whitespaces)"));

            if (args == null)
                throw new ArgumentNullException("args");

            var queueBinding = new QueueBindParamsImpl();
            args(queueBinding);

            Arg.Validate(queueBinding.RoutingKey, "routingKey",
                         () =>
                         LogError(
                             "Queue.BindToExchange method threw an ArgumentNullException exception because routing key was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/bindings/{0}/e/{1}/q/{2}", InitParams.VirtualHost.SanitizeVirtualHostName(),
                                       exchangeName, queueName);

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to bind queue '{0}' to exchange '{1}' belonging to virtual host '{2}'.",
                    queueName, exchangeName, InitParams.VirtualHost));

            return base.Put(url, queueBinding, cancellationToken).Response(cancellationToken);
        }

        public Task<ModifyResponse> Delete(string queueName,
                                           CancellationToken cancellationToken = new CancellationToken())
        {
            Arg.Validate(InitParams.VirtualHost, "virtualHostName",
                         () =>
                         LogError(
                             "Queue.Delete method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(queueName, "queueName",
                         () =>
                         LogError(
                             "Queue.Delete method threw an ArgumentNullException exception because queue name was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/queues/{0}/{1}", InitParams.VirtualHost.SanitizeVirtualHostName(), queueName);

            LogInfo(string.Format("Sent request to RabbitMQ server to delete queue '{0}' from virtual host '{1}'.",
                                  queueName, InitParams.VirtualHost));

            return base.Delete(url, cancellationToken).Response(cancellationToken);
        }
    }
}