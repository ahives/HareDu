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
    internal class HareDuClientImpl :
        HareDuClientBase,
        HareDuClient
    {
        public HareDuClientImpl(ClientInitParamsImpl args) :
            base(args)
        {
            Exchange = new ExchangeClientImpl(args);
            VirtualHost = new VirtualHostClientImpl(args);
            Overview = new OverviewClientImpl(args);
            Node = new NodeClientImpl(args);
            Queue = new QueueClientImpl(args);
            User = new UserClientImpl(args);
            UserPermissions = new UserPermissionsClientImpl(args);
            Channel = new ChannelClientImpl(args);
            Connection = new ConnectionClientImpl(args);
        }

        public ExchangeClient Exchange { get; private set; }
        public VirtualHostClient VirtualHost { get; private set; }
        public OverviewClient Overview { get; private set; }
        public NodeClient Node { get; private set; }
        public QueueClient Queue { get; private set; }
        public UserClient User { get; private set; }
        public UserPermissionsClient UserPermissions { get; private set; }
        public ChannelClient Channel { get; private set; }
        public ConnectionClient Connection { get; private set; }

        public void CancelPendingRequests()
        {
            LogInfo("Cancel all pending requests.");

            Client.CancelPendingRequests();
        }
    }
}