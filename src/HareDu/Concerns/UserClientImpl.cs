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

    internal class UserClientImpl :
        HareDuClientBase,
        UserClient
    {
        public UserClientImpl(HareDuClientBehaviorImpl args) :
            base(args)
        {
            Permissions = new PermissionsClientImpl(args);
        }

        public UserClientImpl(Dictionary<string, object> args) :
            base(args)
        {
            Permissions = new PermissionsClientImpl(args);
        }

        public PermissionsClient Permissions { get; private set; }

        public Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            LogInfo("Sent request to return all information pertaining to all users on current RabbitMQ server.");

            string url = "api/users";

            return base.Get(url, cancellationToken).As<IEnumerable<User>>(cancellationToken);
        }

        public Task<User> Get(string userName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            LogInfo(string.Format(
                "Sent request to the RabbitMQ server to return information pertaining to user '{0}'.", userName));

            string url = string.Format("api/users/{0}", userName);

            return base.Get(url, cancellationToken).As<User>(cancellationToken);
        }

        public Task<ServerResponse> New(string userName, Action<UserCharacteristics> args,
                                        CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            LogInfo(string.Format("Sent request to the RabbitMQ server to create user '{0}'.", userName));

            var argsImpl = new UserCharacteristicsImpl();
            args(argsImpl);

            string url = string.Format("api/users/{0}", userName);

            return base.Put(url, argsImpl, cancellationToken).Response<ServerResponse>(cancellationToken);
        }

        public Task<ServerResponse> Delete(string userName,
                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            if (userName == Init.Username)
            {
                string errorMsg = string.Format(
                    "Cannot delete user '{0}' because it is being used to send requests with the credentials of the logged in to the current client session.",
                    userName);
                LogError(string.Format("{0} method threw a CannotDeleteSessionLoginUserException exception. {1}",
                                       "User.Delete", errorMsg));
                throw new CannotDeleteSessionLoginUserException(errorMsg);
            }

            LogInfo(string.Format("Sent request to RabbitMQ server to delete user '{0}'.", userName));

            string url = string.Format("api/users/{0}", userName);

            return base.Delete(url, cancellationToken).Response<ServerResponse>(cancellationToken);
        }
    }
}