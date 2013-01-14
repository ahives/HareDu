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
            Binding = new QueueBindingClientImpl(args);
        }

        public QueueBindingClient Binding { get; private set; }

        public Task<IEnumerable<Queue>> GetAll(CancellationToken cancellationToken = default(CancellationToken))
        {
            LogInfo(
                "Sent request to return all information on all queues on all virtual hosts on current RabbitMQ server.");

            string url = "api/queues";

            return base.Get(url, cancellationToken).As<IEnumerable<Queue>>(cancellationToken);
        }

        public Task<CreateCmdResponse> New(string queueName, Action<NewQueueParams> args,
                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(Init.VirtualHost, "virtualHostName",
                         () =>
                         LogError(
                             "Queue.New method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(queueName, "queueName",
                         () =>
                         LogError(
                             "Queue.New method threw an ArgumentNullException exception because queue name was invalid (i.e. empty, null, or all whitespaces)"));

            if (args == null)
                throw new ArgumentNullException("args");

            var queue = new NewQueueParamsImpl();
            args(queue);

            string url = string.Format("api/queues/{0}/{1}", Init.VirtualHost.SanitizeVirtualHostName(), queueName);

            LogInfo(string.IsNullOrEmpty(queue.Node)
                        ? string.Format(
                            "Sent request to RabbitMQ server to create queue '{0}' on virtual host '{1}'.", queueName,
                            Init.VirtualHost)
                        : string.Format(
                            "Sent request to RabbitMQ server to create queue '{0}' on virtual host '{1}' on node '{2}'.",
                            queueName, Init.VirtualHost, queue.Node));

            return base.Put(url, queue, cancellationToken).Response<CreateCmdResponse>(cancellationToken);
        }

        public Task<DeleteCmdResponse> Delete(string queueName,
                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(Init.VirtualHost, "virtualHostName",
                         () =>
                         LogError(
                             "Queue.Delete method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(queueName, "queueName",
                         () =>
                         LogError(
                             "Queue.Delete method threw an ArgumentNullException exception because queue name was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/queues/{0}/{1}", Init.VirtualHost.SanitizeVirtualHostName(), queueName);

            LogInfo(string.Format("Sent request to RabbitMQ server to delete queue '{0}' from virtual host '{1}'.",
                                  queueName, Init.VirtualHost));

            return base.Delete(url, cancellationToken).Response<DeleteCmdResponse>(cancellationToken);
        }
    }
}