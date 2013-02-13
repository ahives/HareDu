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

namespace HareDu.Model
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class Node :
        HareDuModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("running")]
        public bool Running { get; set; }

        [JsonProperty("os_pid")]
        public string OperatingSystemPID { get; set; }

        [JsonProperty("mem_ets")]
        public long MemoryEts { get; set; }

        [JsonProperty("mem_binary")]
        public long MemoryBinary { get; set; }

        [JsonProperty("mem_proc")]
        public long MemoryProc { get; set; }

        [JsonProperty("mem_proc_used")]
        public long MemoryProcUsed { get; set; }

        [JsonProperty("mem_atom")]
        public long MemoryAtom { get; set; }

        [JsonProperty("mem_atom_used")]
        public long MemoryAtomUsed { get; set; }

        [JsonProperty("mem_code")]
        public string MemoryCode { get; set; }

        [JsonProperty("fd_used")]
        public string FdUsed { get; set; }

        [JsonProperty("fd_total")]
        public int FdTotal { get; set; }

        [JsonProperty("sockets_used")]
        public int SocketsUsed { get; set; }

        [JsonProperty("sockets_total")]
        public string SocketsTotal { get; set; }

        [JsonProperty("mem_used")]
        public long MemoryUsed { get; set; }

        [JsonProperty("mem_limit")]
        public long MemoryLimit { get; set; }

        [JsonProperty("mem_alarm")]
        public bool MemoryAlarm { get; set; }

        [JsonProperty("disk_free_limit")]
        public long DiskFreeLimit { get; set; }

        [JsonProperty("disk_free")]
        public long DiskFree { get; set; }

        [JsonProperty("disk_free_alarm")]
        public bool DiskFreeAlarm { get; set; }

        [JsonProperty("proc_used")]
        public long ProcUsed { get; set; }

        [JsonProperty("proc_total")]
        public long ProcTotal { get; set; }

        [JsonProperty("statistics_level")]
        public string StatisticsLevel { get; set; }

        [JsonProperty("erlang_version")]
        public string ErlangVersion { get; set; }

        [JsonProperty("uptime")]
        public long Uptime { get; set; }

        [JsonProperty("run_queue")]
        public int RunQueue { get; set; }

        [JsonProperty("processors")]
        public int Processors { get; set; }

        [JsonProperty("exchange_types")]
        public IEnumerable<ExchangeType> ExchangeTypes { get; set; }

        [JsonProperty("auth_mechanisms")]
        public IEnumerable<AuthenticationMechanism> AuthenticationMechanisms { get; set; }

        [JsonProperty("applications")]
        public IEnumerable<Application> Applications { get; set; }
    }
}