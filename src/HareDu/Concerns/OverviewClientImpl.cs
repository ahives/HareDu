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
    using System.Threading;
    using System.Threading.Tasks;
    using Model;

    internal class OverviewClientImpl :
        HareDuClientBase,
        OverviewClient
    {
        public OverviewClientImpl(ClientInitParamsImpl args) : base(args)
        {
        }

        public Task<Overview> Get(CancellationToken cancellationToken = new CancellationToken())
        {
            LogInfo("Sent request to return general information pertaining to current RabbitMQ server.");

            string url = "api/overview";

            return base.Get(url, cancellationToken).Response<Overview>(cancellationToken);
        }
    }
}