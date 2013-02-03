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

namespace HareDu
{
    internal class ClusterClientImpl :
        ClusterClient
    {
        public ClusterClientImpl(HareDuClientBehaviorImpl args)
        {
            Overview = new OverviewClientImpl(args);
            Node = new NodeClientImpl(args);
        }

        public OverviewClient Overview { get; private set; }
        public NodeClient Node { get; private set; }
    }
}