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
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using Internal;
    using Model;

    internal class VirtualHostClientImpl :
        HareDuClientBase,
        VirtualHostClient
    {
        public VirtualHostClientImpl(ClientInitParamsImpl args) :
            base(args)
        {
            Exchange = new ExchangeClientImpl(args);
            Queue = new QueueClientImpl(args);
        }

        public ExchangeClient Exchange { get; private set; }
        public QueueClient Queue { get; private set; }

        public Task<IEnumerable<VirtualHost>> GetAll(CancellationToken cancellationToken = default(CancellationToken))
        {
            LogInfo("Sent request to return all information on all virtual hosts on current RabbitMQ server.");

            string url = "api/vhosts";

            return base.Get(url, cancellationToken).As<IEnumerable<VirtualHost>>(cancellationToken);
        }

        public Task<CreateCmdResponse> New(string virtualHostName,
                                        CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(virtualHostName, "virtualHostName",
                         () =>
                         LogError(
                             "VirtualHost.New method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/vhosts/{0}", virtualHostName.SanitizeVirtualHostName());

            LogInfo(string.Format("Sent request to RabbitMQ server to create virtual host '{0}'.", virtualHostName));

            return base.Put(url, new StringContent(string.Empty), cancellationToken).Response<CreateCmdResponse>(cancellationToken);
        }

        public Task<DeleteCmdResponse> Delete(string virtualHostName,
                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            if (virtualHostName.SanitizeVirtualHostName() == "2%f")
            {
                LogError(
                    "VirtualHost.Delete method threw a CannotDeleteVirtualHostException exception for attempting to delete the default virtual host.");
                throw new CannotDeleteVirtualHostException("Cannot delete the default virtual host.");
            }

            Arg.Validate(virtualHostName, "virtualHostName",
                         () =>
                         LogError(
                             "VirtualHost.Delete method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/vhosts/{0}", virtualHostName.SanitizeVirtualHostName());

            LogInfo(string.Format("Sent request to RabbitMQ server to delete virtual host '{0}'.", virtualHostName));

            return base.Delete(url, cancellationToken).Response<DeleteCmdResponse>(cancellationToken);
        }

        public VirtualHostClient Change(string virtualHostName, Action<UserCredentials> args)
        {
            Init.OnVirtualHost(virtualHostName);

            var userCreds = new UserCredentialsImpl();
            args(userCreds);

            Client = GetClient(Init.HostUrl, userCreds.Username, userCreds.Password);

            return this;
        }

        public Task<AlivenessTestCmdResponse> IsAlive(CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(Init.VirtualHost, "virtualHostName",
                         () =>
                         LogError(
                             "VirtualHost.IsAlive method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/aliveness-test/{0}", Init.VirtualHost.SanitizeVirtualHostName());

            LogInfo(
                string.Format(
                    "Sent request to execute an aliveness test on virtual host '{0}' on current RabbitMQ server.", Init.VirtualHost));

            return base.Get(url, cancellationToken)
                       .ContinueWith(t =>
                                         {
                                             t.Result.EnsureSuccessStatusCode();
                                             var response = t.Result.Content.ReadAsAsync<AlivenessTestCmdResponse>().Result;
                                             response.StatusCode = t.Result.StatusCode;
                                             response.ServerResponse = t.Result.ReasonPhrase;

                                             return response;
                                         }, cancellationToken, TaskContinuationOptions.OnlyOnRanToCompletion,
                                     TaskScheduler.Current);
        }
    }
}