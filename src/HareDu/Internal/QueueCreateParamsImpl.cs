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

namespace HareDu.Internal
{
    using System.Collections.Generic;
    using Contracts;
    using Newtonsoft.Json;

    public class QueueCreateParamsImpl :
        QueueCreateParams
    {
        public QueueCreateParamsImpl()
        {
            Arguments = new List<string>();
        }

        [JsonProperty(PropertyName = "durable", Order = 2)]
        public bool Durable { get; private set; }

        [JsonProperty(PropertyName = "auto_delete", Order = 1)]
        public bool AutoDelete { get; private set; }

        [JsonProperty(PropertyName = "arguments", Order = 3, Required = Required.Default)]
        public List<string> Arguments { get; set; }

        [JsonProperty(PropertyName = "node", Order = 4, Required = Required.Default)]
        public string Node { get; set; }

        public void OnNode(string nodeName)
        {
            Node = nodeName;
        }

        public void IsDurable()
        {
            Durable = true;
        }

        public void AutoDeleteWhenNotInUse()
        {
            AutoDelete = true;
        }

        public void UsingArguments(List<string> args)
        {
            Arguments = args;
        }
    }
}