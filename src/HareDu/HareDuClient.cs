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
    public interface HareDuClient
    {
        /// <summary>
        /// Gets the client that 
        /// </summary>
        ExchangeClient Exchange { get; }

        /// <summary>
        /// 
        /// </summary>
        VirtualHostClient VirtualHost { get; }

        /// <summary>
        /// 
        /// </summary>
        OverviewClient Overview { get; }

        /// <summary>
        /// 
        /// </summary>
        NodeClient Node { get; }

        /// <summary>
        /// 
        /// </summary>
        QueueClient Queue { get; }

        /// <summary>
        /// 
        /// </summary>
        UserClient User { get; }

        /// <summary>
        /// 
        /// </summary>
        UserPermissionsClient UserPermissions { get; }

        /// <summary>
        /// 
        /// </summary>
        ChannelClient Channel { get; }

        /// <summary>
        /// 
        /// </summary>
        ConnectionClient Connection { get; }

        /// <summary>
        /// 
        /// </summary>
        void CancelPendingRequests();
    }
}