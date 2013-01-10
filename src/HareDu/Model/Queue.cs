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

namespace HareDu.Model
{
    using Newtonsoft.Json;

    public class Queue
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("memory")]
        public string Memory { get; set; }

        [JsonProperty("idle_since")]
        public string IdleSince { get; set; }

        [JsonProperty("messages_ready")]
        public int MessagesReady { get; set; }

        [JsonProperty("messages_unacknowledged")]
        public string MessagesUnacknowledged { get; set; }

        [JsonProperty("messages")]
        public string Messages { get; set; }

        [JsonProperty("consumers")]
        public string Consumers { get; set; }

        [JsonProperty("vhost")]
        public string VirtualHostName { get; set; }

        [JsonProperty("durable")]
        public bool IsDurable { get; set; }

        [JsonProperty("node")]
        public string Node { get; set; }
    }
}