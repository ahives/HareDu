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

namespace HareDu.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class OverviewTests :
        HareDuTestBase
    {
        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Return_Overview()
        {
            var data = Client.Cluster
                             .Overview
                             .Get()
                             .Data();

            Console.WriteLine("Management Version: {0}", data.ManagementVersion);
            Console.WriteLine("Statistics Level: {0}", data.StatisticsLevel);

            Console.WriteLine("******************** Exchange Types ********************");
            foreach (var exchangeType in data.ExchangeTypes)
            {
                Console.WriteLine("Name: {0}", exchangeType.Name);
                Console.WriteLine("Description: {0}", exchangeType.Description);
                Console.WriteLine("Enabled: {0}", exchangeType.Enabled);
            }

            Console.WriteLine("******************** Message Stats ********************");
            foreach (var messageStats in data.MessageStats)
            {
                Console.WriteLine("Acknowledged: {0}", messageStats.Acknowledged);
                Console.WriteLine("Published: {0}", messageStats.Published);
                Console.WriteLine("Delivered: {0}", messageStats.Delivered);
                Console.WriteLine("Delivered/Get: {0}", messageStats.DeliveredOrGet);
                Console.WriteLine("Acknowledged: {0}", messageStats.Acknowledged);
            }

            Console.WriteLine("******************** Message Details ********************");
            Console.WriteLine("Messages: {0}", data.QueueTotals.Messages);
            Console.WriteLine("Messages Ready: {0}", data.QueueTotals.MessagesReady);
            Console.WriteLine("Messages Unacknowledged: {0}", data.QueueTotals.MessagesUnacknowledged);

            Console.WriteLine("******************** Messages Details ********************");
            Console.WriteLine("Rate: {0}", data.QueueTotals.MessagesDetails.Rate);
            Console.WriteLine("Interval: {0}", data.QueueTotals.MessagesDetails.Interval);
            Console.WriteLine("Last Event: {0}", data.QueueTotals.MessagesDetails.LastEvent);

            Console.WriteLine("******************** Messages Ready Details ********************");
            Console.WriteLine("Rate: {0}", data.QueueTotals.MessagesReadyDetails.Rate);
            Console.WriteLine("Interval: {0}", data.QueueTotals.MessagesReadyDetails.Interval);
            Console.WriteLine("Last Event: {0}", data.QueueTotals.MessagesReadyDetails.LastEvent);

            Console.WriteLine("******************** Messages Unacknowledged Details ********************");
            Console.WriteLine("Rate: {0}", data.QueueTotals.MessagesUnacknowledgedDetails.Rate);
            Console.WriteLine("Interval: {0}", data.QueueTotals.MessagesUnacknowledgedDetails.Interval);
            Console.WriteLine("Last Event: {0}", data.QueueTotals.MessagesUnacknowledgedDetails.LastEvent);
            Console.WriteLine("Node: {0}", data.Node);
            Console.WriteLine("Statistics DB Node: {0}", data.StatisticsDbNode);

            Console.WriteLine("******************** Listeners ********************");
            foreach (var listener in data.Listeners)
            {
                Console.WriteLine("Node: {0}", listener.Node);
                Console.WriteLine("Protocol: {0}", listener.Protocol);
                Console.WriteLine("IP Address: {0}", listener.IPAddress);
                Console.WriteLine("Port: {0}", listener.Port);
            }

            Console.WriteLine("******************** Contexts ********************");
            foreach (var context in data.Contexts)
            {
                Console.WriteLine("Node: {0}", context.Node);
                Console.WriteLine("Description: {0}", context.Description);
                Console.WriteLine("Path: {0}", context.Path);
                Console.WriteLine("Port: {0}", context.Port);
            }
        }
    }
}