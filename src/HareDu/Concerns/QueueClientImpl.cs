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
        public QueueClientImpl(ClientCharacteristicsImpl args)
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

        public Task<CreateCmdResponse> New(string queue, Action<QueueCharacteristics> args,
                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(Init.VirtualHost, "virtualHost",
                         () =>
                         LogError(
                             "Queue.New method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(queue, "queue",
                         () =>
                         LogError(
                             "Queue.New method threw an ArgumentNullException exception because queue name was invalid (i.e. empty, null, or all whitespaces)"));

            if (args == null)
                throw new ArgumentNullException("args");

            var action = new QueueCharacteristicsImpl();
            args(action);

            string url = string.Format("api/queues/{0}/{1}", Init.VirtualHost.SanitizeVirtualHostName(), queue);

            LogInfo(string.IsNullOrEmpty(action.Node)
                        ? string.Format(
                            "Sent request to RabbitMQ server to create queue '{0}' on virtual host '{1}'.", queue, Init.VirtualHost)
                        : string.Format(
                            "Sent request to RabbitMQ server to create queue '{0}' on virtual host '{1}' on node '{2}'.",
                            queue, Init.VirtualHost, action.Node));

            return base.Put(url, action, cancellationToken).Response<CreateCmdResponse>(cancellationToken);
        }

        public Task<DeleteCmdResponse> Delete(string queue, CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(Init.VirtualHost, "virtualHost",
                         () =>
                         LogError(
                             "Queue.Delete method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(queue, "queue",
                         () =>
                         LogError(
                             "Queue.Delete method threw an ArgumentNullException exception because queue name was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/queues/{0}/{1}", Init.VirtualHost.SanitizeVirtualHostName(), queue);

            LogInfo(string.Format("Sent request to RabbitMQ server to delete queue '{0}' from virtual host '{1}'.",
                                  queue, Init.VirtualHost));

            return base.Delete(url, cancellationToken).Response<DeleteCmdResponse>(cancellationToken);
        }

        public Task<DeleteCmdResponse> Purge(string queue, CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(Init.VirtualHost, "virtualHost",
             () =>
             LogError(
                 "Queue.Purge method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(queue, "queue",
                         () =>
                         LogError(
                             "Queue.Purge method threw an ArgumentNullException exception because queue name was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/queues/{0}/{1}/contents", Init.VirtualHost.SanitizeVirtualHostName(), queue);

            LogInfo(string.Format("Sent request to RabbitMQ server to purge the contents of queue '{0}'.", queue));

            return base.Delete(url, cancellationToken).Response<DeleteCmdResponse>(cancellationToken);
        }
    }
}