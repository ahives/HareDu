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

namespace HareDu.Resources
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Async;
    using Contracts;
    using Model;

    public interface QueueResources
    {
        ///// <summary>
        ///// 
        ///// </summary>
        //QueueBindingResources QueueExchangeBindings { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken">Task cancellation token.</param>
        /// <returns></returns>
        Task<IEnumerable<Queue>> GetAll(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="target"></param>
        /// <param name="behavior"></param>
        /// <param name="cancellationToken">Task cancellation token.</param>
        /// <returns>Returns an asynchronous task having the server response and HTTP response code as the result.</returns>
        Task<ServerResponse> New(string queue, Action<QueueTarget> target, Action<QueueBehavior> behavior,
                                 CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="target"></param>
        /// <param name="cancellationToken">Task cancellation token.</param>
        /// <returns>Returns an asynchronous task having the server response and HTTP response code as the result.</returns>
        Task<ServerResponse> Delete(Action<QueueTarget> target,
                                    CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Deletes all the messages in the specified queue.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="cancellationToken">Task cancellation token.</param>
        /// <returns>Returns an asynchronous task having the server response and HTTP response code as the result.</returns>
        Task<ServerResponse> Purge(Action<QueueTarget> target,
                                   CancellationToken cancellationToken = default(CancellationToken));
    }
}