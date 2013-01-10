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

    public class UserPermissionClientImpl :
        HareDuClientBase,
        UserPermissionClient
    {
        public UserPermissionClientImpl(ClientInitParamsImpl args) : base(args)
        {
        }

        public Task<UserPermissions> Get(string userName, CancellationToken cancellationToken = new CancellationToken())
        {
            Arg.Validate(InitParams.VirtualHost, "virtualHostName",
                         () =>
                         LogError(
                             "UserPermission.Get method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(userName, "userName",
                         () =>
                         LogError(
                             "UserPermission.Get method threw an ArgumentNullException exception because username was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/permissions/{0}/{1}", InitParams.VirtualHost.SanitizeVirtualHostName(),
                                       userName);

            LogInfo(
                string.Format(
                    "Sent request to return user permission information pertaining to user '{0}' on virtual host '{1}' users on current RabbitMQ server.",
                    userName, InitParams.VirtualHost));

            return base.Get(url, cancellationToken).Response<UserPermissions>(cancellationToken);
        }

        public Task<IEnumerable<UserPermissions>> GetAll(CancellationToken cancellationToken = new CancellationToken())
        {
            LogInfo(
                "Sent request to return all user permission information pertaining to all users on current RabbitMQ server.");

            string url = "api/permissions";

            return base.Get(url, cancellationToken).Response<IEnumerable<UserPermissions>>(cancellationToken);
        }

        public Task<ModifyResponse> Set(string userName, Action<UserPermissionsCreateParams> args,
                                        CancellationToken cancellationToken = new CancellationToken())
        {
            Arg.Validate(InitParams.VirtualHost, "virtualHostName",
                         () =>
                         LogError(
                             "UserPermission.Set method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(userName, "userName",
                         () =>
                         LogError(
                             "UserPermission.Set method threw an ArgumentNullException exception because username was invalid (i.e. empty, null, or all whitespaces)"));

            if (args == null)
                throw new ArgumentNullException("args");

            var permissions = new UserPermissionsCreateParamsImpl();
            args(permissions);

            Arg.Validate(permissions.ConfigurePermissions, "configurePermissions",
                         () =>
                         LogError(
                             "UserPermission.Set method threw an ArgumentNullException exception because configure permissions was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(permissions.WritePermissions, "writePermissions",
                         () =>
                         LogError(
                             "UserPermission.Set method threw an ArgumentNullException exception because write permissions was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(permissions.ReadPermissions, "readPermissions",
                         () =>
                         LogError(
                             "UserPermission.Set method threw an ArgumentNullException exception because read permissions was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/permissions/{0}/{1}", InitParams.VirtualHost.SanitizeVirtualHostName(),
                                       userName);

            LogInfo(
                string.Format(
                    "Sent request to the RabbitMQ server to set permissions for user '{0}' on virtual host '{1}'.",
                    userName, InitParams.VirtualHost));

            return base.Put(url, permissions, cancellationToken).Response(cancellationToken);
        }

        public Task<ModifyResponse> Delete(string userName,
                                           CancellationToken cancellationToken = new CancellationToken())
        {
            Arg.Validate(InitParams.VirtualHost, "virtualHostName",
                         () =>
                         LogError(
                             "UserPermission.Delete method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(userName, "userName",
                         () =>
                         LogError(
                             "UserPermission.Delete method threw an ArgumentNullException exception because username was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/permissions/{0}/{1}", InitParams.VirtualHost.SanitizeVirtualHostName(),
                                       userName);

            return base.Delete(url, cancellationToken).Response(cancellationToken);
        }
    }
}