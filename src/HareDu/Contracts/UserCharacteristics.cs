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

    public interface UserCharacteristics
    {
        /// <summary>
        /// Defines the password the user will have upon creation.
        /// </summary>
        /// <param name="password"></param>
        void WithPassword(string password);

        /// <summary>
        /// Defines the level of permissions that the user will have upon creation.
        /// </summary>
        /// <param name="tags">Generates a comma-separated list of tags (e.g. administrator, monitoring, management) representing the user access level.</param>
        void WithTags(Action<UserAccessCharacteristics> tags);
    }
}