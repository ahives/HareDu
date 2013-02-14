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

namespace HareDu.Internal
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using Newtonsoft.Json;

    internal class QueueBindingBehaviorImpl :
        QueueBindingBehavior
    {
        public QueueBindingBehaviorImpl()
        {
            RoutingKey = string.Empty;
            Arguments = new List<string>();
        }

        [JsonProperty(PropertyName = "routing_key", Order = 1)]
        public string RoutingKey { get; set; }

        [JsonProperty(PropertyName = "arguments", Order = 2, Required = Required.Default)]
        public IEnumerable<string> Arguments { get; set; }

        public void UsingRoutingKey(string routingKey)
        {
            RoutingKey = routingKey;
        }

        public void WithArguments(Action<Arguments> arg)
        {
            if (arg == null)
                return;

            var action = new ArgumentsImpl();
            arg(action);
            Arguments = action.ArgumentMap.ToList();
        }

        public void WithArguments(Dictionary<string, object> args)
        {
            if (args == null)
                return;

            Arguments = args.ToList();
        }
    }
}