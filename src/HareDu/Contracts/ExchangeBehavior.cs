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

namespace HareDu.Contracts
{
    using System;
    using System.Collections.Generic;

    public interface ExchangeBehavior
    {
        /// <summary>
        /// 
        /// </summary>
        void IsDurable();

        /// <summary>
        /// 
        /// </summary>
        void AutoDeleteWhenNotInUse();

        /// <summary>
        /// 
        /// </summary>
        void IsForInternalUse();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        void WithArguments(Action<Arguments> arg);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        void WithArguments(Dictionary<string, object> args);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routingType"></param>
        void UsingRoutingType(Action<ExchangeRoutingBehavior> routingType);
    }
}