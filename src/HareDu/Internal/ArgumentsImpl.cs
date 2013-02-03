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
    using System.Collections.Generic;
    using Contracts;

    internal class ArgumentsImpl :
        Arguments
    {
        public ArgumentsImpl()
        {
            ArgumentMap = new Dictionary<string, object>();
        }

        public Dictionary<string, object> ArgumentMap { get; private set; }

        public void Set(string arg, int value)
        {
            ArgumentMap.Add(arg, value);
        }

        public void Set(string arg, long value)
        {
            ArgumentMap.Add(arg, value);
        }

        public void Set(string arg, string value)
        {
            ArgumentMap.Add(arg, value);
        }

        public void Set(string arg, bool value)
        {
            ArgumentMap.Add(arg, value);
        }

        public void SetPerQueueExpiration(int milliseconds)
        {
            ArgumentMap.Add("x-message-ttl", milliseconds);
        }

        public void SetQueueExpiration(int milliseconds)
        {
            ArgumentMap.Add("x-expires", milliseconds);
        }

        public void SetDeadLetterExchange(string exchange)
        {
            ArgumentMap.Add("x-dead-letter-exchange", exchange);
        }

        public void SetDeadLetterExchangeRoutingKey(string routingKey)
        {
            ArgumentMap.Add("x-dead-letter-routing-key", routingKey);
        }

        public void SetAlternateExchange(string exchange)
        {
            ArgumentMap.Add("alternate-exchange", exchange);
        }
    }
}