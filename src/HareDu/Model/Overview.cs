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

    public class Overview :
        HareDuModel
    {
        [JsonProperty("management_version")]
        public string ManagementVersion { get; set; }

        [JsonProperty("statistics_level")]
        public string StatisticsLevel { get; set; }

        [JsonProperty("exchange_types")]
        public IEnumerable<ExchangeType> ExchangeTypes { get; set; }

        [JsonProperty("message_stats")]
        public IEnumerable<MessageStats> MessageStats { get; set; }

        [JsonProperty("queue_totals")]
        public QueueTotals QueueTotals { get; set; }

        [JsonProperty("node")]
        public string Node { get; set; }

        [JsonProperty("statistics_db_node")]
        public string StatisticsDbNode { get; set; }

        [JsonProperty("listeners")]
        public IEnumerable<Listener> Listeners { get; set; }

        [JsonProperty("contexts")]
        public IEnumerable<Context> Contexts { get; set; }
    }
}