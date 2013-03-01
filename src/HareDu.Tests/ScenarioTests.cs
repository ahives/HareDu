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
    public class ScenarioTests
    {
        [Test, Category("Integration"), Explicit]
        public void I_Want_To_Create_A_New_Exchange_On_An_Existing_VirtualHost()
        {
            try
            {
                var client = HareDuFactory.New(x =>
                                                   {
                                                       x.ConnectTo("http://localhost:15672");
                                                       x.EnableLogging(y => y.Logger("HareDuLogger"));
                                                   });
                string virtualHost = "MyVirtualHostScenarioTest1";
                string exchange = "MyExchangeScenarioTest2";
                string username = "haredu_1";
                string password = "haredu";
                var virtualHostResources = client.Factory<VirtualHostResources>(x => x.Credentials(username, password));
                var createNewExchangeResponse = virtualHostResources.Exchange
                                                                    .New(x => x.Exchange(exchange),
                                                                         x =>
                                                                             {
                                                                                 x.IsDurable();
                                                                                 x.UsingRoutingType(y => y.Fanout());
                                                                             },
                                                                         x => x.VirtualHost(virtualHost))
                                                                    .Response();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
        }

        [Test]
        public void I_Want_To_Create_A_New_Queue_And_Bind_It_To_An_Existing_Exchange()
        {
            try
            {
                var client = HareDuFactory.New(x =>
                                                   {
                                                       x.ConnectTo("http://localhost:15672");
                                                       x.EnableLogging(y => y.Logger("HareDuLogger"));
                                                   });
                string virtualHost = "MyVirtualHostScenarioTest1";
                string exchange = "MyExchangeScenarioTest1";
                string username = "haredu_1";
                string password = "haredu";
                string queue = "MyQueueScenarioTest1";
                var virtualHostResources = client.Factory<VirtualHostResources>(x => x.Credentials(username, password));
                var createNewQueueResponse = virtualHostResources.Queue
                                                                 .New(x => x.Queue(queue),
                                                                      x => x.IsDurable(),
                                                                      x => x.VirtualHost(virtualHost))
                                                                 .Response();
                string routingKey = "test_routing_key";
                var createBindingResponse = virtualHostResources.QueueExchangeBindings
                                                                .New(x => x.Binding(queue, exchange),
                                                                     x => x.UsingRoutingKey(routingKey),
                                                                     x => x.VirtualHost(virtualHost))
                                                                .Response();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        [Test]
        public void I_Want_To_Create_A_New_User_With_Permissions()
        {
            try
            {
                var client = HareDuFactory.New(x =>
                                                   {
                                                       x.ConnectTo("http://localhost:15672");
                                                       x.EnableLogging(y => y.Logger("HareDuLogger"));
                                                   });
                string virtualHost = "MyVirtualHostScenarioTest1";
                var userResources = client.Factory<UserResources>(x => x.Credentials("guest", "guest"));
                string username = "haredu_2";
                string password = "haredu";
                var createUserResponse = userResources.New(x => x.User(username),
                                                           x =>
                                                               {
                                                                   x.WithPassword(password);
                                                                   x.WithTags(y => y.Administrator());
                                                               })
                                                      .Response();
                var setUserPermissionsResponse = userResources.Permissions
                                                              .Set(x => x.User(username),
                                                                   x =>
                                                                       {
                                                                           x.Configure(".*");
                                                                           x.Read(".*");
                                                                           x.Write(".*");
                                                                       },
                                                                   x => x.VirtualHost(virtualHost))
                                                              .Response();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        [Test, Category("Integration"), Explicit]
        public void I_Want_To_Create_A_New_VirtualHost_With_A_User()
        {
            try
            {
                var client = HareDuFactory.New(x =>
                                                   {
                                                       x.ConnectTo("http://localhost:15672");
                                                       x.EnableLogging(y => y.Logger("HareDuLogger"));
                                                   });
                var virtualHostResources = client.Factory<VirtualHostResources>(x => x.Credentials("guest", "guest"));
                string virtualHost = "MyVirtualHostScenarioTest1";
                var createVirtualHostResponse = virtualHostResources.New(x => x.VirtualHost(virtualHost))
                                                                    .Response();
                var userResources = client.Factory<UserResources>(x => x.Credentials("guest", "guest"));
                string username = "haredu_1";
                string password = "haredu";
                var createUserResponse = userResources.New(x => x.User(username),
                                                           x =>
                                                               {
                                                                   x.WithPassword(password);
                                                                   x.WithTags(y => y.Administrator());
                                                               })
                                                      .Response();
                var setUserPermissionsResponse = userResources.Permissions
                                                              .Set(x => x.User(username),
                                                                   x =>
                                                                       {
                                                                           x.Configure(".*");
                                                                           x.Read(".*");
                                                                           x.Write(".*");
                                                                       },
                                                                   x => x.VirtualHost(virtualHost))
                                                              .Response();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        [Test, Category("Integration"), Explicit]
        public void I_Want_To_See_Queue_And_Message_Stats()
        {
            try
            {
                var client = HareDuFactory.New(x =>
                                                   {
                                                       x.ConnectTo("http://localhost:15672");
                                                       x.EnableLogging(y => y.Logger("HareDuLogger"));
                                                   });
                var overview = client
                    .Factory<OverviewResources>(x => x.Credentials("guest", "guest"))
                    .Get()
                    .Data();

                Console.WriteLine("******************** Queue Totals ********************");
                Console.WriteLine("Messages: {0}", overview.QueueTotals.Messages);
                Console.WriteLine("Messages Ready: {0}", overview.QueueTotals.MessagesReady);
                Console.WriteLine("Messages Unacknowledged: {0}", overview.QueueTotals.MessagesUnacknowledged);

                Console.WriteLine("******************** Message Details ********************");
                Console.WriteLine("Rate: {0}", overview.QueueTotals.MessagesDetails.Rate);
                Console.WriteLine("Interval: {0}", overview.QueueTotals.MessagesDetails.Interval);
                Console.WriteLine("Last Event: {0}", overview.QueueTotals.MessagesDetails.LastEvent);

                Console.WriteLine("******************** Ready Messages ********************");
                Console.WriteLine("Rate: {0}", overview.QueueTotals.MessagesReadyDetails.Rate);
                Console.WriteLine("Interval: {0}", overview.QueueTotals.MessagesReadyDetails.Interval);
                Console.WriteLine("Last Event: {0}", overview.QueueTotals.MessagesReadyDetails.LastEvent);

                Console.WriteLine("******************** Unacknowledged Messages ********************");
                Console.WriteLine("Rate: {0}", overview.QueueTotals.MessagesUnacknowledgedDetails.Rate);
                Console.WriteLine("Interval: {0}", overview.QueueTotals.MessagesUnacknowledgedDetails.Interval);
                Console.WriteLine("Last Event: {0}", overview.QueueTotals.MessagesUnacknowledgedDetails.LastEvent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}