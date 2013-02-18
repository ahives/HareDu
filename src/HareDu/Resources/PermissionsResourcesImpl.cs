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

    internal class PermissionsResourcesImpl :
        HareDuResourcesBase,
        PermissionsResources
    {
        public PermissionsResourcesImpl(HttpClient client, ILog logger) :
            base(client, logger)
        {
        }

        public Task<Permissions> Get(Action<UserTarget> user, Action<VirtualHostTarget> virtualHost,
                                     CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var virtualHostTargetImpl = new VirtualHostTargetImpl();
            virtualHost(virtualHostTargetImpl);

            var userTargetImpl = new UserTargetImpl();
            user(userTargetImpl);

            string url = string.Format("api/permissions/{0}/{1}", virtualHostTargetImpl.Target.SanitizeVirtualHostName(),
                                       userTargetImpl.Target);

            LogInfo(
                string.Format(
                    "Sent request to return user permission information pertaining to user '{0}' on virtual host '{1}' users on current RabbitMQ server.",
                    userTargetImpl.Target, virtualHostTargetImpl.Target));

            return base.Get(url, cancellationToken).As<Permissions>(cancellationToken);
        }

        public Task<IEnumerable<Permissions>> GetAll(CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            LogInfo(
                "Sent request to return all user permission information pertaining to all users on current RabbitMQ server.");

            string url = "api/permissions";

            return base.Get(url, cancellationToken).As<IEnumerable<Permissions>>(cancellationToken);
        }

        public Task<ServerResponse> Set(Action<UserTarget> user, Action<UserPermissionsBehavior> behavior,
                                        Action<VirtualHostTarget> virtualHost,
                                        CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var argsImpl = new UserPermissionsBehaviorImpl();
            behavior(argsImpl);

            var virtualHostTargetImpl = new VirtualHostTargetImpl();
            virtualHost(virtualHostTargetImpl);

            var userTargetImpl = new UserTargetImpl();
            user(userTargetImpl);

            string url = string.Format("api/permissions/{0}/{1}", virtualHostTargetImpl.Target.SanitizeVirtualHostName(),
                                       userTargetImpl.Target);

            LogInfo(
                string.Format(
                    "Sent request to the RabbitMQ server to set permissions for user '{0}' on virtual host '{1}'.",
                    userTargetImpl.Target, virtualHostTargetImpl.Target));

            return base.Put(url, argsImpl, cancellationToken).Response<ServerResponse>(cancellationToken);
        }

        public Task<ServerResponse> Delete(Action<UserTarget> user, Action<VirtualHostTarget> virtualHost,
                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var virtualHostTargetImpl = new VirtualHostTargetImpl();
            virtualHost(virtualHostTargetImpl);

            var userTargetImpl = new UserTargetImpl();
            user(userTargetImpl);

            string url = string.Format("api/permissions/{0}/{1}", virtualHostTargetImpl.Target.SanitizeVirtualHostName(),
                                       userTargetImpl.Target);

            LogInfo(
                string.Format(
                    "Sent request to the RabbitMQ server to delete permissions for user '{0}' on virtual host '{1}'.",
                    userTargetImpl.Target, virtualHostTargetImpl.Target));

            return base.Delete(url, cancellationToken).Response<ServerResponse>(cancellationToken);
        }
    }
}