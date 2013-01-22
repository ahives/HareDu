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
    using System;
    using System.Collections.Generic;
    using Contracts;
    using Newtonsoft.Json;

    public class ExchangeCharacteristicsImpl :
        ExchangeCharacteristics
    {
        public ExchangeCharacteristicsImpl()
        {
            RoutingType = ExchangeType.Direct;
        }

        [JsonProperty(PropertyName = "type", Order = 1)]
        public string RoutingType { get; set; }

        [JsonProperty(PropertyName = "auto_delete", Order = 2)]
        public bool AutoDelete { get; set; }

        [JsonProperty(PropertyName = "durable", Order = 3)]
        public bool Durable { get; set; }

        [JsonProperty(PropertyName = "internal", Order = 4)]
        public bool Internal { get; set; }

        [JsonProperty(PropertyName = "arguments", Order = 5, Required = Required.Default)]
        public IEnumerable<string> Arguments { get; set; }

        public void IsDurable()
        {
            Durable = true;
        }

        public void AutoDeleteWhenNotInUse()
        {
            AutoDelete = true;
        }

        public void IsForInternalUse()
        {
            Internal = true;
        }

        public void WithArguments(Action<Arguments> arg)
        {
            var action = new ArgumentsImpl();
            arg(action);
            Arguments = action.ArgumentMap.ToList();
        }

        public void UsingRoutingType(Action<ExchangeTypeCharacteristics> routingType)
        {
            var routingTypeParam = new ExchangeTypeCharacteristicsImpl();
            routingType(routingTypeParam);
            RoutingType = routingTypeParam.RoutingType;
        }
    }
}