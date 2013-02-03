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

namespace HareDu
{
    using System;

    public static class ValidationExtensions
    {
        private static readonly Func<string, string, string> EmptyOrWhitespaceParamMsg;
        private static readonly Func<string, string, string> NullReferenceParamMsg;

        static ValidationExtensions()
        {
            EmptyOrWhitespaceParamMsg = (source, paramName) =>
                                        string.Format(
                                            "{0} method threw an ArgumentException exception because parameter '{1}' was invalid (i.e. empty, null, or all whitespaces)",
                                            source, paramName);
            NullReferenceParamMsg = (source, paramName) =>
                                    string.Format(
                                        "{0} method threw an ArgumentNullException exception because parameter '{1}' was invalid (i.e. null)",
                                        source, paramName);
        }

        public static void Validate(this string value, string paramName, Action logging)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                if (!logging.IsNull())
                    logging();

                throw new ArgumentException(string.Format("{0} is null, empty, or consists of all whitespaces",
                                                          paramName));
            }
        }

        public static void Validate(this string value, string source, string paramName, Action<string> logging)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                if (!logging.IsNull())
                    logging(EmptyOrWhitespaceParamMsg(source, paramName));

                throw new ArgumentException(string.Format("{0} is null, empty, or consists of all whitespaces",
                                                          paramName));
            }
        }

        public static void Validate<T>(this T value, string source, string paramName, Action<string> logging)
            where T : class
        {
            if (!logging.IsNull())
                logging(NullReferenceParamMsg(source, paramName));

            if (value.IsNull())
                throw new ArgumentException(string.Format("{0} is null", paramName));
        }
    }
}