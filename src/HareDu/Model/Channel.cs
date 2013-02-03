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
    using System;
    using Newtonsoft.Json;

    public class Channel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("consumer_count")]
        public int ConsumerCount { get; set; }

        [JsonProperty("prefetch_count")]
        public int PrefetchCount { get; set; }

        [JsonProperty("idle_since")]
        public DateTime IdleSince { get; set; }

        [JsonProperty("transactional")]
        public bool IsTransactional { get; set; }

        [JsonProperty("confirm")]
        public bool Confirm { get; set; }

        [JsonProperty("client_flow_blocked")]
        public bool IsClientFlowBlocked { get; set; }

        [JsonProperty("node")]
        public string Node { get; set; }

        [JsonProperty("connection_details")]
        public ConnectionDetails ConnectionDetails { get; set; }

        [JsonProperty("vhost")]
        public string VirtualHostName { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("message_stats")]
        public MessageStats MessageStats { get; set; }

        [JsonProperty("messages_unacknowledged")]
        public int Unacknowledged { get; set; }

        [JsonProperty("messages_unconfirmed")]
        public int Unconfirmed { get; set; }

        [JsonProperty("messages_uncommitted")]
        public int Uncommitted { get; set; }

        [JsonProperty("acks_uncommitted")]
        public int AcknowledgesUncommitted { get; set; }
    }
}