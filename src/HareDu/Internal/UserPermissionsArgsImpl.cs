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

namespace HareDu.Internal
{
    using System;
    using Contracts;
    using Newtonsoft.Json;

    public class UserPermissionsArgsImpl :
        UserPermissionsArgs
    {
        [JsonProperty(PropertyName = "configure", Order = 1)]
        public string ConfigurePermissions { get; set; }

        [JsonProperty(PropertyName = "write", Order = 2)]
        public string WritePermissions { get; set; }

        [JsonProperty(PropertyName = "read", Order = 3)]
        public string ReadPermissions { get; set; }

        public void AssignConfigurePermissions(string configurePermissions)
        {
            if (configurePermissions == null)
                throw new ArgumentNullException("configurePermissions");

            ConfigurePermissions = configurePermissions;
        }

        public void AssignWritePermissions(string writePermissions)
        {
            if (writePermissions == null)
                throw new ArgumentNullException("writePermissions");

            WritePermissions = writePermissions;
        }

        public void AssignReadPermissions(string readPermissions)
        {
            if (readPermissions == null)
                throw new ArgumentNullException("readPermissions");

            ReadPermissions = readPermissions;
        }
    }
}