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

    internal class PolicyResourcesImpl :
        HareDuResourcesBase,
        PolicyResources
    {
        public PolicyResourcesImpl(HttpClient client, ILog logger) :
            base(client, logger)
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

        public Task<IEnumerable<Policy>> GetAll(Action<VirtualHostTarget> virtualHost,
                                                CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var virtualHostTargetImpl = new VirtualHostTargetImpl();
            virtualHost(virtualHostTargetImpl);

            LogInfo(
                string.Format(
                    "Sent request to return all policy information pertaining to virtual host {0} on current RabbitMQ server.",
                    virtualHostTargetImpl.Target));

            string url = string.Format("api/policies/{0}", virtualHostTargetImpl.Target.SanitizeVirtualHostName());

            return base.Get(url, cancellationToken).As<IEnumerable<Policy>>(cancellationToken);
        }

        public Task<Policy> Get(Action<PolicyTarget> policy, Action<VirtualHostTarget> virtualHost,
                                CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var policyTargetImpl = new PolicyTargetImpl();
            policy(policyTargetImpl);

            var virtualHostTargetImpl = new VirtualHostTargetImpl();
            virtualHost(virtualHostTargetImpl);

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to return policy information pertaining to policy '{0}' belonging to virtual host '{1}'.",
                    policyTargetImpl.Target, virtualHostTargetImpl.Target));

            string url = string.Format("api/policies/{0}/{1}", virtualHostTargetImpl.Target.SanitizeVirtualHostName(),
                                       policyTargetImpl.Target);

            return base.Get(url, cancellationToken).As<Policy>(cancellationToken);
        }

        public Task<ServerResponse> New(Action<PolicyTarget> policy, Action<PolicyCharacteristics> characteristics,
                                        Action<VirtualHostTarget> virtualHost,
                                        CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var characteristicsImpl = new PolicyCharacteristicsImpl();
            characteristics(characteristicsImpl);

            var policyTargetImpl = new PolicyTargetImpl();
            policy(policyTargetImpl);

            var virtualHostTargetImpl = new VirtualHostTargetImpl();
            virtualHost(virtualHostTargetImpl);

            LogInfo(
                string.Format("Sent request to RabbitMQ server to create a new policy '{0}' on virtual host '{1}'.",
                              policyTargetImpl.Target, virtualHostTargetImpl.Target));

            string url = string.Format("api/policies/{0}/{1}", virtualHostTargetImpl.Target.SanitizeVirtualHostName(),
                                       policyTargetImpl.Target);

            return base.Put(url, characteristicsImpl, cancellationToken).Response<ServerResponse>(cancellationToken);
        }

        public Task<ServerResponse> Delete(Action<PolicyTarget> policy, Action<VirtualHostTarget> virtualHost,
                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var policyTargetImpl = new PolicyTargetImpl();
            policy(policyTargetImpl);

            var virtualHostTargetImpl = new VirtualHostTargetImpl();
            virtualHost(virtualHostTargetImpl);

            LogInfo(string.Format("Sent request to RabbitMQ server to delete policy '{0}' from virtual host '{1}'.",
                                  policyTargetImpl.Target, virtualHostTargetImpl.Target));

            string url = string.Format("api/policies/{0}/{1}", virtualHostTargetImpl.Target.SanitizeVirtualHostName(),
                                       policyTargetImpl.Target);

            return base.Delete(url, cancellationToken).Response<ServerResponse>(cancellationToken);
        }
    }
}