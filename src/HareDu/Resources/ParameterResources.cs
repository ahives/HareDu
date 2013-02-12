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

    public interface ParameterResources :
        ResourceClient
    {
        Task<Parameter> Get(Action<ComponentTarget> target,
                            CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<Parameter>> GetAll(CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<Parameter>> GetAll(string component,
                                            CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<Parameter>> GetAll(Action<ComponentTarget> target,
                                            CancellationToken cancellationToken = default(CancellationToken));

        Task<ServerResponse> New(Action<ComponentTarget> target,
                                 CancellationToken cancellationToken = default(CancellationToken));

        Task<ServerResponse> Delete(Action<ComponentTarget> target,
                                    CancellationToken cancellationToken = default(CancellationToken));
    }
}