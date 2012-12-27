// Copyright 2012-2013 Albert L. Hives, Chris Patterson, Rajesh Gande, et al.
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

    public class Exchange
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("vhost")]
        public string VirtualHostName { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("durable")]
        public bool IsDurable { get; set; }

        [JsonProperty("auto_delete")]
        public bool IsSetToAutoDelete { get; set; }

        [JsonProperty("internal")]
        public bool IsInternal { get; set; }
    }
}