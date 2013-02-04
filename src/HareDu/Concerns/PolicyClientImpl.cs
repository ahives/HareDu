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
    using System.Threading;
    using System.Threading.Tasks;
    using Async;
    using Contracts;
    using Internal;
    using Model;

    internal class PolicyClientImpl :
        HareDuClientBase,
        PolicyClient
    {
        public PolicyClientImpl(HareDuClientBehaviorImpl args) :
            base(args)
        {
        }

        public Task<IEnumerable<Policy>> GetAll(CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            LogInfo(
                "Sent request to return all information pertaining to all policies on all virtual hosts on current RabbitMQ server.");

            string url = "api/policies";

            return base.Get(url, cancellationToken).As<IEnumerable<Policy>>(cancellationToken);
        }

        public Task<IEnumerable<Policy>> GetAll(Action<PolicyBehavior> args,
                                                CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var argsImpl = new PolicyBehaviorImpl();
            args(argsImpl);

            LogInfo(
                string.Format(
                    "Sent request to return all policy information pertaining to virtual host {0} on current RabbitMQ server.",
                    argsImpl.VirtualHost));

            string url = string.Format("api/policies/{0}", argsImpl.VirtualHost.SanitizeVirtualHostName());

            return base.Get(url, cancellationToken).As<IEnumerable<Policy>>(cancellationToken);
        }

        public Task<Policy> Get(string policy, Action<PolicyBehavior> args,
                                CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to return policy information pertaining to policy '{0}' belonging to virtual host '{1}'.",
                    policy, Init.VirtualHost));

            string url = string.Format("api/policies/{0}/{1}", Init.VirtualHost.SanitizeVirtualHostName(), policy);

            return base.Get(url, cancellationToken).As<Policy>(cancellationToken);
        }

        public Task<ServerResponse> New(string policy, Action<VirtualHostTarget> target,
                                        Action<PolicyCharacteristics> characteristics,
                                        CancellationToken cancellationToken = new CancellationToken())
        {
            cancellationToken.RequestCanceled(LogInfo);

            var characteristicsImpl = new PolicyCharacteristicsImpl();
            characteristics(characteristicsImpl);

            var targetImpl = new VirtualHostTargetImpl();
            target(targetImpl);

            LogInfo(
                string.Format("Sent request to RabbitMQ server to create a new policy '{0}' on virtual host '{1}'.",
                              policy, targetImpl.VirtualHost));

            string url = string.Format("api/policies/{0}/{1}", targetImpl.VirtualHost.SanitizeVirtualHostName(), policy);

            return base.Put(url, characteristicsImpl, cancellationToken).Response<ServerResponse>(cancellationToken);
        }

        public Task<ServerResponse> New(string policy, Action<PolicyCharacteristics> characteristics,
                                        CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var characteristicsImpl = new PolicyCharacteristicsImpl();
            characteristics(characteristicsImpl);

            LogInfo(
                string.Format("Sent request to RabbitMQ server to create a new policy '{0}' on virtual host '{1}'.",
                              policy, Init.VirtualHost));

            string url = string.Format("api/policies/{0}/{1}", Init.VirtualHost.SanitizeVirtualHostName(), policy);

            return base.Put(url, characteristicsImpl, cancellationToken).Response<ServerResponse>(cancellationToken);
        }

        public Task<ServerResponse> Delete(string policy,
                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            LogInfo(string.Format("Sent request to RabbitMQ server to delete policy '{0}' from virtual host '{1}'.",
                                  policy, Init.VirtualHost));

            string url = string.Format("api/policies/{0}/{1}", Init.VirtualHost.SanitizeVirtualHostName(), policy);

            return base.Delete(url, cancellationToken).Response<ServerResponse>(cancellationToken);
        }
    }
}