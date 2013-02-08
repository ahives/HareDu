﻿// Copyright 2013-2014 Albert L. Hives, Chris Patterson, et al.
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
    using Contracts;
    using Newtonsoft.Json;

    public class UserPermissionsBehaviorImpl :
        UserPermissionsBehavior
    {
        public UserPermissionsBehaviorImpl()
        {
            ConfigurePermissions = ".*";
            WritePermissions = ".*";
            ReadPermissions = ".*";
        }

        [JsonProperty(PropertyName = "configure", Order = 1)]
        public string ConfigurePermissions { get; set; }

        [JsonProperty(PropertyName = "write", Order = 2)]
        public string WritePermissions { get; set; }

        [JsonProperty(PropertyName = "read", Order = 3)]
        public string ReadPermissions { get; set; }

        public void Configure(string configurePermissions)
        {
            ConfigurePermissions = configurePermissions;
        }

        public void Write(string writePermissions)
        {
            WritePermissions = writePermissions;
        }

        public void Read(string readPermissions)
        {
            ReadPermissions = readPermissions;
        }
    }
}