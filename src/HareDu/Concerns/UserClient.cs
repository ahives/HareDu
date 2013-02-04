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

namespace HareDu.Concerns
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Async;
    using Contracts;
    using Model;

    public interface UserClient
    {
        /// <summary>
        /// 
        /// </summary>
        UserPermissionsClient Permissions { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<User> Get(string userName, CancellationToken cancellationToken =
                                            default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="args"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ServerResponse> New(string userName, Action<UserCharacteristics> args,
                                 CancellationToken cancellationToken =
                                     default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ServerResponse> Delete(string userName, CancellationToken cancellationToken =
                                                         default(CancellationToken));
    }
}