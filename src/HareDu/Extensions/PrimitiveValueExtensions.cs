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

namespace HareDu
{
    using System;

    public static class PrimitiveValueExtensions
    {
        public static string SanitizeVirtualHostName(this string value)
        {
            if (value == @"/")
            {
                return value.Replace("/", "%2f");
            }

            return value;
        }

        public static bool IsNull<T>(this T value)
            where T : class
        {
            return (value == null);
        }

        public static void CheckIfArgValid(this string value, string paramName)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(paramName);
        }

        public static void CheckIfArgValid<T>(this T value, string paramName)
            where T : class
        {
            if (value.IsNull())
                throw new ArgumentNullException(paramName);
        }
    }
}