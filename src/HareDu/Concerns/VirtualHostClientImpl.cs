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
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using Model;

    internal class VirtualHostClientImpl :
        HareDuClientBase,
        VirtualHostClient
    {
        public VirtualHostClientImpl(ClientInitParamsImpl args) : base(args)
        {
        }

        public Task<IEnumerable<VirtualHost>> GetAll(CancellationToken cancellationToken = new CancellationToken())
        {
            LogInfo("Sent request to return all information on all virtual hosts on current RabbitMQ server.");

            string url = "api/vhosts";

            return base.Get(url, cancellationToken).Response<IEnumerable<VirtualHost>>(cancellationToken);
        }

        public Task<ModifyResponse> Create(string virtualHostName,
                                           CancellationToken cancellationToken = new CancellationToken())
        {
            Arg.Validate(virtualHostName, "virtualHostName",
                         () =>
                         LogError(
                             "CreateVirtualHost method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/vhosts/{0}", virtualHostName.SanitizeVirtualHostName());

            LogInfo(string.Format("Sent request to RabbitMQ server to create virtual host '{0}'.", virtualHostName));

            return base.Put(url, new StringContent(string.Empty), cancellationToken).Response(cancellationToken);
        }

        public Task<ModifyResponse> Delete(string virtualHostName,
                                           CancellationToken cancellationToken = new CancellationToken())
        {
            if (virtualHostName.SanitizeVirtualHostName() == "2%f")
            {
                LogError(
                    "DeleteVirtualHost method threw a CannotDeleteVirtualHostException exception for attempting to delete the default virtual host.");
                throw new CannotDeleteVirtualHostException("Cannot delete the default virtual host.");
            }

            Arg.Validate(virtualHostName, "virtualHostName",
                         () =>
                         LogError(
                             "DeleteVirtualHost method threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));

            string url = string.Format("api/vhosts/{0}", virtualHostName.SanitizeVirtualHostName());

            LogInfo(string.Format("Sent request to RabbitMQ server to delete virtual host '{0}'.", virtualHostName));

            return base.Delete(url, cancellationToken).Response(cancellationToken);
        }
    }
}