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

namespace HareDu
{
    using System;

    internal class Arg
    {
        public static void Validate(string value, string paramName, Action logging)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                if (!logging.IsNull())
                    logging();

                throw new ArgumentException(string.Format("{0} is null, empty, or consists of all whitespaces", paramName));
            }
        }

        public static void Validate<T>(T value, string paramName)
            where T : class
        {
            if (value.IsNull())
                throw new ArgumentNullException(paramName);
        }
    }
}