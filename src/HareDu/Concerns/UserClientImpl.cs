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

    internal class UserClientImpl :
        HareDuClientBase,
        UserClient
    {
        public UserClientImpl(ClientCharacteristicsImpl args) : base(args)
        {
            Permissions = new UserPermissionsClientImpl(args);
        }

        public UserPermissionsClient Permissions { get; private set; }

        public Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken = default(CancellationToken))
        {
            LogInfo("Sent request to return all information pertaining to all users on current RabbitMQ server.");

            string url = "api/users";

            return base.Get(url, cancellationToken).As<IEnumerable<User>>(cancellationToken);
        }

        public Task<User> Get(string userName, CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(userName, "userName",
                         () =>
                         LogError(
                             "User.Get method threw an ArgumentNullException exception because username was invalid (i.e. empty, null, or all whitespaces)"));

            LogInfo(string.Format(
                "Sent request to the RabbitMQ server to return information pertaining to user '{0}'.", userName));

            string url = string.Format("api/users/{0}", userName);

            return base.Get(url, cancellationToken).As<User>(cancellationToken);
        }

        public Task<CreateCmdResponse> New(string userName, Action<UserCharacteristics> args,
                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(userName, "userName",
                         () =>
                         LogError(
                             "User.New method threw an ArgumentNullException exception because username was invalid (i.e. empty, null, or all whitespaces)"));

            LogInfo(string.Format("Sent request to the RabbitMQ server to create user '{0}'.", userName));

            if (args == null)
                throw new ArgumentNullException("args");

            var user = new UserCharacteristicsImpl();
            args(user);

            Arg.Validate(user.Password, "password",
                         () =>
                         LogError(
                             "User.New method threw an ArgumentNullException exception because password was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/users/{0}", userName);

            return base.Put(url, user, cancellationToken).Response<CreateCmdResponse>(cancellationToken);
        }

        public Task<DeleteCmdResponse> Delete(string userName,
                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(userName, "userName",
                         () =>
                         LogError(
                             "User.Delete method threw an ArgumentNullException exception because username was invalid (i.e. empty, null, or all whitespaces)"));

            if (userName == Init.Username)
            {
                LogError(
                    "User.Delete method threw a CannotDeleteSessionLoginUserException exception because attempted to delete the login user");
                throw new CannotDeleteSessionLoginUserException(
                    string.Format(
                        "Cannot delete user '{0}' because it is being used to send requests login to the current client session.",
                        userName));
            }

            LogInfo(string.Format("Sent request to RabbitMQ server to delete user '{0}'.", userName));

            string url = string.Format("api/users/{0}", userName);

            return base.Delete(url, cancellationToken).Response<DeleteCmdResponse>(cancellationToken);
        }
    }
}