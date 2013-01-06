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

namespace HareDu.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class OverviewTests :
        HareDuTestBase
    {
        [Test]
        public void Verify_Can_Return_Overview()
        {
            var overview = Client.GetOverview().Result;

            Console.WriteLine("Management Version: {0}", overview.ManagementVersion);
            Console.WriteLine("Statistics Level: {0}", overview.StatisticsLevel);

            Console.WriteLine("******************** Exchange Types ********************");
            foreach (var exchangeType in overview.ExchangeTypes)
            {
                Console.WriteLine("Name: {0}", exchangeType.Name);
                Console.WriteLine("Description: {0}", exchangeType.Description);
                Console.WriteLine("Enabled: {0}", exchangeType.Enabled);
            }

            Console.WriteLine("Acknowledged: {0}", overview.MessageStats.Acknowledged);
            Console.WriteLine("Published: {0}", overview.MessageStats.Published);
            Console.WriteLine("Delivered: {0}", overview.MessageStats.Delivered);
            Console.WriteLine("Delivered/Get: {0}", overview.MessageStats.DeliveredOrGet);
            Console.WriteLine("Acknowledged: {0}", overview.MessageStats.Acknowledged);

            Console.WriteLine("******************** Message Details ********************");
            Console.WriteLine("Messages: {0}", overview.QueueTotals.Messages);
            Console.WriteLine("Messages Ready: {0}", overview.QueueTotals.MessagesReady);
            Console.WriteLine("Messages Unacknowledged: {0}", overview.QueueTotals.MessagesUnacknowledged);

            Console.WriteLine("******************** Messages Details ********************");
            Console.WriteLine("Rate: {0}", overview.QueueTotals.MessagesDetails.Rate);
            Console.WriteLine("Interval: {0}", overview.QueueTotals.MessagesDetails.Interval);
            Console.WriteLine("Last Event: {0}", overview.QueueTotals.MessagesDetails.LastEvent);

            Console.WriteLine("******************** Messages Ready Details ********************");
            Console.WriteLine("Rate: {0}", overview.QueueTotals.MessagesReadyDetails.Rate);
            Console.WriteLine("Interval: {0}", overview.QueueTotals.MessagesReadyDetails.Interval);
            Console.WriteLine("Last Event: {0}", overview.QueueTotals.MessagesReadyDetails.LastEvent);

            Console.WriteLine("******************** Messages Unacknowledged Details ********************");
            Console.WriteLine("Rate: {0}", overview.QueueTotals.MessagesUnacknowledgedDetails.Rate);
            Console.WriteLine("Interval: {0}", overview.QueueTotals.MessagesUnacknowledgedDetails.Interval);
            Console.WriteLine("Last Event: {0}", overview.QueueTotals.MessagesUnacknowledgedDetails.LastEvent);
            Console.WriteLine("Node: {0}", overview.Node);
            Console.WriteLine("Statistics DB Node: {0}", overview.StatisticsDbNode);

            Console.WriteLine("******************** Listeners ********************");
            foreach (var listener in overview.Listeners)
            {
                Console.WriteLine("Node: {0}", listener.Node);
                Console.WriteLine("Protocol: {0}", listener.Protocol);
                Console.WriteLine("IP Address: {0}", listener.IPAddress);
                Console.WriteLine("Port: {0}", listener.Port);
            }

            Console.WriteLine("******************** Contexts ********************");
            foreach (var context in overview.Contexts)
            {
                Console.WriteLine("Node: {0}", context.Node);
                Console.WriteLine("Description: {0}", context.Description);
                Console.WriteLine("Path: {0}", context.Path);
                Console.WriteLine("Port: {0}", context.Port);
            }
        }
    }
}