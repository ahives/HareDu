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

    public class UserPermissionsClientImpl :
        HareDuClientBase,
        UserPermissionsClient
    {
        public UserPermissionsClientImpl(ClientCharacteristicsImpl args) : base(args)
        {
        }

        public Task<Permissions> Get(string userName, CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(Init.VirtualHost, "virtualHost",
                         () =>
                         LogError(
                             "Permissions.Get method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(userName, "userName",
                         () =>
                         LogError(
                             "Permissions.Get method threw an ArgumentNullException exception because username was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/permissions/{0}/{1}", Init.VirtualHost.SanitizeVirtualHostName(),
                                       userName);

            LogInfo(
                string.Format(
                    "Sent request to return user permission information pertaining to user '{0}' on virtual host '{1}' users on current RabbitMQ server.",
                    userName, Init.VirtualHost));

            return base.Get(url, cancellationToken).As<Permissions>(cancellationToken);
        }

        public Task<IEnumerable<Permissions>> GetAll(CancellationToken cancellationToken = default(CancellationToken))
        {
            LogInfo(
                "Sent request to return all user permission information pertaining to all users on current RabbitMQ server.");

            string url = "api/permissions";

            return base.Get(url, cancellationToken).As<IEnumerable<Permissions>>(cancellationToken);
        }

        public Task<CreateCmdResponse> Set(string userName, Action<PermissionCharacteristics> args,
                                        CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(Init.VirtualHost, "virtualHost",
                         () =>
                         LogError(
                             "Permissions.Set method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(userName, "userName",
                         () =>
                         LogError(
                             "Permissions.Set method threw an ArgumentNullException exception because username was invalid (i.e. empty, null, or all whitespaces)"));

            if (args == null)
                throw new ArgumentNullException("args");

            var permissions = new PermissionCharacteristicsImpl();
            args(permissions);

            Arg.Validate(permissions.ConfigurePermissions, "configurePermissions",
                         () =>
                         LogError(
                             "Permissions.Set method threw an ArgumentNullException exception because configure permissions was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(permissions.WritePermissions, "writePermissions",
                         () =>
                         LogError(
                             "Permissions.Set method threw an ArgumentNullException exception because write permissions was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(permissions.ReadPermissions, "readPermissions",
                         () =>
                         LogError(
                             "Permissions.Set method threw an ArgumentNullException exception because read permissions was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/permissions/{0}/{1}", Init.VirtualHost.SanitizeVirtualHostName(),
                                       userName);

            LogInfo(
                string.Format(
                    "Sent request to the RabbitMQ server to set permissions for user '{0}' on virtual host '{1}'.",
                    userName, Init.VirtualHost));

            return base.Put(url, permissions, cancellationToken).Response<CreateCmdResponse>(cancellationToken);
        }

        public Task<DeleteCmdResponse> Delete(string userName, CancellationToken cancellationToken = default(CancellationToken))
        {
            Arg.Validate(Init.VirtualHost, "virtualHost",
                         () =>
                         LogError(
                             "Permissions.Delete method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(userName, "userName",
                         () =>
                         LogError(
                             "Permissions.Delete method threw an ArgumentNullException exception because username was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/permissions/{0}/{1}", Init.VirtualHost.SanitizeVirtualHostName(), userName);

            return base.Delete(url, cancellationToken).Response<DeleteCmdResponse>(cancellationToken);
        }
    }
}