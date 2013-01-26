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
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Async;

    public static class AsyncExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="task"></param>
        /// <returns></returns>
        public static T Response<T>(this Task<T> task)
            where T : AsyncResponse
        {
            return task.Result;
        }

        public static T Data<T>(this Task<T> task)
        {
            return task.Result;
        }

        public static Task<T> Response<T>(this Task<HttpResponseMessage> task, CancellationToken cancellationToken)
            where T : AsyncResponse, new()
        {
            return task.ContinueWith(t =>
                                         {
                                             t.Result.EnsureSuccessStatusCode();
                                             return new T
                                                        {
                                                            ServerResponseReason = t.Result.ReasonPhrase,
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

        internal static void RequestCanceled(this CancellationToken cancellationToken, Action<string> logging)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                if (!logging.IsNull())
                    logging("Cancellation of this task was requested by the caller, therefore, request for resources has been canceled.");

                cancellationToken.ThrowIfCancellationRequested();
            }
        }
    }
}