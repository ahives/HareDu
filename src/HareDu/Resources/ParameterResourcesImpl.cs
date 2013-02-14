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

    internal class ParameterResourcesImpl :
        HareDuResourcesBase,
        ParameterResources
    {
        public ParameterResourcesImpl(HttpClient client, ILog logger) :
            base(client, logger)
        {
        }

        public Task<Parameter> Get(Action<ComponentTarget> target,
                                   CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            LogInfo(
                "Sent request to return all information pertaining to all parameters on all virtual hosts on current RabbitMQ server.");

            var targetImpl = new ComponentTargetImpl();
            target(targetImpl);

            string url = string.Format("api/parameters/{0}/{1}/{2}", targetImpl.Component,
                                       targetImpl.VirtualHost.SanitizeVirtualHostName(), targetImpl.Name);

            return base.Get(url, cancellationToken).As<Parameter>(cancellationToken);
        }

        public Task<IEnumerable<Parameter>> GetAll(CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            LogInfo(
                "Sent request to return all information pertaining to all parameters on all virtual hosts on current RabbitMQ server.");

            string url = "api/parameters";

            return base.Get(url, cancellationToken).As<IEnumerable<Parameter>>(cancellationToken);
        }

        public Task<IEnumerable<Parameter>> GetAll(string component,
                                                   CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            LogInfo(
                string.Format(
                    "Sent request to return all information pertaining to all parameters for component '{0}' on current RabbitMQ server.",
                    component));

            string url = string.Format("api/parameters/{0}", component);

            return base.Get(url, cancellationToken).As<IEnumerable<Parameter>>(cancellationToken);
        }

        public Task<IEnumerable<Parameter>> GetAll(Action<ComponentTarget> target,
                                                   CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var targetImpl = new ComponentTargetImpl();
            target(targetImpl);

            LogInfo(
                string.Format(
                    "Sent request to return all information pertaining to all parameters for component '{0} on virtual host '{1}' on current RabbitMQ server.",
                    targetImpl.Component, targetImpl.VirtualHost));

            string url = string.Format("api/parameters/{0}/{1}", targetImpl.Component,
                                       targetImpl.VirtualHost.SanitizeVirtualHostName());

            return base.Get(url, cancellationToken).As<IEnumerable<Parameter>>(cancellationToken);
        }

        public Task<ServerResponse> New(Action<ComponentTarget> target,
                                        CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var targetImpl = new ComponentTargetImpl();
            target(targetImpl);

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to create a new parameter named '{0}' for component '{1}' on virtual host '{2}'.",
                    targetImpl.Name, targetImpl.Component, targetImpl.VirtualHost));

            string url = string.Format("api/parameters/{0}/{1}", targetImpl.Component,
                                       targetImpl.VirtualHost.SanitizeVirtualHostName());

            return base.Put(url, targetImpl, cancellationToken).Response<ServerResponse>(cancellationToken);
        }

        public Task<ServerResponse> Delete(Action<ComponentTarget> target,
                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var targetImpl = new ComponentTargetImpl();
            target(targetImpl);

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to delete parameter '{0}' of component '{1}' on virtual host '{2}'.",
                    targetImpl.Name, targetImpl.Component, targetImpl.VirtualHost));

            string url = string.Format("api/parameters/{0}/{1}/{2}", targetImpl.Component,
                                       targetImpl.VirtualHost.SanitizeVirtualHostName(), targetImpl.Name);

            return base.Delete(url, cancellationToken).Response<ServerResponse>(cancellationToken);
        }
    }
}