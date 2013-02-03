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

    internal class PolicyCharacteristicsImpl :
        PolicyCharacteristics
    {
        public Dictionary<string, string> Definition { get; set; }
        public string Pattern { get; set; }
        public int Priority { get; set; }

        public void UsingPattern(string pattern)
        {
            Pattern = pattern;
        }

        public void DefinedAs(Action<PolicyDefinition> arg)
        {
            var argImpl = new PolicyDefinitionImpl();
            arg(argImpl);
            Definition = argImpl.Definition;
        }

        public void WithPriority(int priority)
        {
            Priority = priority;
        }
    }
}