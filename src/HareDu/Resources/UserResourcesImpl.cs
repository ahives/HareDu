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

    internal class UserResourcesImpl :
        HareDuResourcesBase,
        UserResources
    {
        public UserResourcesImpl(HttpClient client, ILog logger) :
            base(client, logger)
        {
            Permissions = new PermissionsResourcesImpl(client, logger);
        }

        public PermissionsResources Permissions { get; private set; }

        public Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            string url = "api/users";

            LogInfo("Sent request to return all information pertaining to all users on current RabbitMQ server.");

            return base.Get(url, cancellationToken).As<IEnumerable<User>>(cancellationToken);
        }

        public Task<User> Get(string userName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            string url = string.Format("api/users/{0}", userName);

            LogInfo(string.Format(
                "Sent request to the RabbitMQ server to return information pertaining to user '{0}'.", userName));

            return base.Get(url, cancellationToken).As<User>(cancellationToken);
        }

        public Task<ServerResponse> New(string userName, Action<UserCharacteristics> characteristics,
                                        CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var characteristicsImpl = new UserCharacteristicsImpl();
            characteristics(characteristicsImpl);

            string url = string.Format("api/users/{0}", userName);

            LogInfo(string.Format("Sent request to the RabbitMQ server to create user '{0}'.", userName));

            return base.Put(url, characteristicsImpl, cancellationToken).Response<ServerResponse>(cancellationToken);
        }

        public Task<ServerResponse> Delete(string userName,
                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            string url = string.Format("api/users/{0}", userName);

            LogInfo(string.Format("Sent request to RabbitMQ server to delete user '{0}'.", userName));

            return base.Delete(url, cancellationToken).Response<ServerResponse>(cancellationToken);
        }
    }
}