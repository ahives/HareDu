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
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;

    internal static class InternalHttpExtensions
    {
        public static Task<T> Response<T>(this Task<HttpResponseMessage> task, CancellationToken cancellationToken)
            where T : AsyncResponse, new()
        {
            return task.ContinueWith(t =>
                                         {
                                             t.Result.EnsureSuccessStatusCode();
                                             return new T
                                                        {
                                                            ServerResponse = t.Result.ReasonPhrase,
                                                            StatusCode = t.Result.StatusCode
                                                        };
                                         },
                                     cancellationToken, TaskContinuationOptions.OnlyOnRanToCompletion,
                                     TaskScheduler.Current);
        }

        public static Task<T> As<T>(this Task<HttpResponseMessage> task, CancellationToken cancellationToken)
        {
            return task.ContinueWith(t =>
                                         {
                                             t.Result.EnsureSuccessStatusCode();
                                             return t.Result.Content.ReadAsAsync<T>();
                                         }, cancellationToken, TaskContinuationOptions.OnlyOnRanToCompletion,
                                     TaskScheduler.Current)
                       .Unwrap();
        }

        public static string SanitizeVirtualHostName(this string value)
        {
            if (value == @"/")
            {
                return value.Replace("/", "%2f");
            }

            return value;
        }

        public static string SanitizePropertiesKey(this string value)
        {
            return value.Replace("%5F", "%255F");
        }
    }
}