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
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using Model;

    public interface ExchangeClient
    {
        /// <summary>
        /// Returns information of all exchanges on the current virtual host.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<Exchange>> GetAll(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Exchange> Get(string exchange, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="args"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CreateCmdResponse> New(string exchange, Action<ExchangeCharacteristics> args, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<DeleteCmdResponse> Delete(string exchange, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="boundTo"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<Binding>> GetAllBindings(string exchange, Action<BindingDirection> boundTo, CancellationToken cancellationToken = default(CancellationToken));
    }
}