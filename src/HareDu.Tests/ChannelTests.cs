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

namespace HareDu.Tests
{
    using System;
    using NUnit.Framework;
    using Resources;

    [TestFixture]
    public class ChannelTests :
        HareDuTestBase
    {
        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Return_All_Channels()
        {
            var data = Client
                                .RequestResource<ConnectionResources>(
                    x => x.Credentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                //.Connection
                             .Channel
                             .GetAll()
                             .Data();

            foreach (var channel in data)
            {
                Console.WriteLine("Name: {0}", channel.Name);
                Console.WriteLine("Node: {0}", channel.Node);
                Console.WriteLine("User: {0}", channel.User);
                Console.WriteLine("Confirm: {0}", channel.Confirm);
                Console.WriteLine("Client Flow Blocked: {0}", channel.IsClientFlowBlocked);
                Console.WriteLine("Is Transactional: {0}", channel.IsTransactional);
                Console.WriteLine("Idle Since: {0}", channel.IdleSince.ToString());
                Console.WriteLine("Virtual Host: {0}", channel.VirtualHostName);
                Console.WriteLine("Prefetch Count: {0}", channel.PrefetchCount);
                Console.WriteLine("Unacknowledged: {0}", channel.Unacknowledged);
                Console.WriteLine("Uncommitted: {0}", channel.Uncommitted);
                Console.WriteLine("Unconfirmed: {0}", channel.Unconfirmed);
                Console.WriteLine("Acknowledges Uncommitted: {0}", channel.AcknowledgesUncommitted);
                Console.WriteLine("Consumer Count: {0}", channel.ConsumerCount);
                Console.WriteLine("******************** Connection Details ********************");
                Console.WriteLine("Name: {0}", channel.ConnectionDetails.Name);
                Console.WriteLine("PeerAddress: {0}", channel.ConnectionDetails.PeerAddress);
                Console.WriteLine("PeerPort: {0}", channel.ConnectionDetails.PeerPort);

                if (!channel.MessageStats.IsNull())
                {
                    Console.WriteLine("Acknowledged: {0}", channel.MessageStats.Acknowledged);
                    Console.WriteLine("Published: {0}", channel.MessageStats.Published);
                    Console.WriteLine("Delivered: {0}", channel.MessageStats.Delivered);
                    Console.WriteLine("Delivered/Get: {0}", channel.MessageStats.DeliveredOrGet);
                    Console.WriteLine("Acknowledged: {0}", channel.MessageStats.Acknowledged);
                }

                Console.WriteLine("****************************************************");
                Console.WriteLine();
            }
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Return_Channel()
        {
            var data = Client
                                .RequestResource<ConnectionResources>(
                    x => x.Credentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                //.Connection
                             .Channel
                             .Get(Settings.Default.Channel)
                             .Data();

            Console.WriteLine("Name: {0}", data.Name);
            Console.WriteLine("Node: {0}", data.Node);
            Console.WriteLine("User: {0}", data.User);
            Console.WriteLine("Confirm: {0}", data.Confirm);
            Console.WriteLine("Client Flow Blocked: {0}", data.IsClientFlowBlocked);
            Console.WriteLine("Is Transactional: {0}", data.IsTransactional);
            Console.WriteLine("Idle Since: {0}", data.IdleSince.ToString());
            Console.WriteLine("Virtual Host: {0}", data.VirtualHostName);
            Console.WriteLine("Prefetch Count: {0}", data.PrefetchCount);
            Console.WriteLine("Unacknowledged: {0}", data.Unacknowledged);
            Console.WriteLine("Uncommitted: {0}", data.Uncommitted);
            Console.WriteLine("Unconfirmed: {0}", data.Unconfirmed);
            Console.WriteLine("Acknowledges Uncommitted: {0}", data.AcknowledgesUncommitted);
            Console.WriteLine("Consumer Count: {0}", data.ConsumerCount);

            if (!data.MessageStats.IsNull())
            {
                Console.WriteLine("Acknowledged: {0}", data.MessageStats.Acknowledged);
                Console.WriteLine("Published: {0}", data.MessageStats.Published);
                Console.WriteLine("Delivered: {0}", data.MessageStats.Delivered);
                Console.WriteLine("Delivered/Get: {0}", data.MessageStats.DeliveredOrGet);
                Console.WriteLine("Acknowledged: {0}", data.MessageStats.Acknowledged);
            }
        }
    }
}