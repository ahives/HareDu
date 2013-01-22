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
        public HareDuClientImpl(ClientCharacteristicsImpl args) :
            base(args)
        {
            VirtualHost = new VirtualHostClientImpl(args);
            User = new UserClientImpl(args);
            Connection = new ConnectionClientImpl(args);
            Cluster = new ClusterClientImpl(args);
        }

        public VirtualHostClient VirtualHost { get; private set; }
        public UserClient User { get; private set; }
        public ConnectionClient Connection { get; private set; }
        public ClusterClient Cluster { get; private set; }

        public void CancelPendingRequests()
        {
            LogInfo("Cancel all pending requests.");

            Client.CancelPendingRequests();
        }
    }
}