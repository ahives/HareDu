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

        public Task<Permissions> Get(string userName, string virtualHost, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            string url = string.Format("api/permissions/{0}/{1}", virtualHost.SanitizeVirtualHostName(), userName);

            LogInfo(
                string.Format(
                    "Sent request to return user permission information pertaining to user '{0}' on virtual host '{1}' users on current RabbitMQ server.",
                    userName, virtualHost));

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

        public Task<ServerResponse> Set(string userName, string virtualHost, Action<UserPermissionsBehavior> args, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var argsImpl = new UserPermissionsBehaviorImpl();
            args(argsImpl);

            string url = string.Format("api/permissions/{0}/{1}", virtualHost.SanitizeVirtualHostName(), userName);

            LogInfo(
                string.Format(
                    "Sent request to the RabbitMQ server to set permissions for user '{0}' on virtual host '{1}'.",
                    userName, virtualHost));

            return base.Put(url, argsImpl, cancellationToken).Response<ServerResponse>(cancellationToken);
        }

        public Task<ServerResponse> Delete(string userName, string virtualHost, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            string url = string.Format("api/permissions/{0}/{1}", virtualHost.SanitizeVirtualHostName(), userName);

            LogInfo(
                string.Format(
                    "Sent request to the RabbitMQ server to delete permissions for user '{0}' on virtual host '{1}'.",
                    userName, virtualHost));

            return base.Delete(url, cancellationToken).Response<ServerResponse>(cancellationToken);
        }
    }
}