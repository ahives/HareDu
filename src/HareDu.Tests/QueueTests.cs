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
    using System.Net;
    using NUnit.Framework;

    [TestFixture]
    public class QueueTests :
        HareDuTestBase
    {
        [SetUp]
        public override void Setup()
        {
            Client = HareDuFactory.New(x =>
                                           {
                                               x.ConnectTo(Settings.Default.HostUrl, Settings.Default.VirtualHost);
                                               x.UsingCredentials(Settings.Default.LoginUsername,
                                                                  Settings.Default.LoginPassword);
                                               x.EnableLogging("HareDuLogger");
                                           });
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Create_Queue()
        {
            var response = Client.VirtualHost
                                 .Queue
                                 .New(string.Format("{0}5", Settings.Default.Queue), x => x.IsDurable())
                                 .Response();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Create_Queue_On_Specific_Node()
        {
            var response = Client.VirtualHost
                                 .Queue
                                 .New(string.Format("{0}4", Settings.Default.Queue),
                                      x =>
                                          {
                                              x.IsDurable();
                                              x.OnNode(
                                                  string.Format("rabbit@{0}",
                                                                Environment.GetEnvironmentVariable("COMPUTERNAME")));
                                          })
                                 .Response();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Create_Queue_With_Arguments()
        {
            var response = Client.VirtualHost
                                 .Queue
                                 .New(string.Format("{0}4", Settings.Default.Queue),
                                      x =>
                                          {
                                              x.IsDurable();
                                              x.WithArguments(y =>
                                                                  {
                                                                      y.SetPerQueueExpiration(1000);
                                                                      y.SetQueueExpiration(1000);
                                                                      //y.SetDeadLetterExchange("");
                                                                      //y.SetDeadLetterExchangeRoutingKey("");
                                                                      //y.SetAlternateExchange("");
                                                                      //y.Set("", 0);
                                                                      //y.Set("", 0);
                                                                  });
                                          })
                                 .Response();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Delete_Queue()
        {
            var response = Client.VirtualHost
                                 .Queue
                                 .Delete(string.Format("{0}5", Settings.Default.Queue))
                                 .Response();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Get_All_Queues()
        {
            var data = Client.VirtualHost
                             .Queue
                             .GetAll()
                             .Data();

            foreach (var queue in data)
            {
                Console.WriteLine("Name: {0}", queue.Name);
                Console.WriteLine("Virtual Host: {0}", queue.VirtualHostName);
                Console.WriteLine("Memory: {0}", queue.Memory);
                Console.WriteLine("Messages: {0}", queue.Messages);
                Console.WriteLine("Messages Ready: {0}", queue.MessagesReady);
                Console.WriteLine("Messages Unacknowledged: {0}", queue.MessagesUnacknowledged);
                Console.WriteLine("Node: {0}", queue.Node);
                Console.WriteLine("IsDurable: {0}", queue.IsDurable);
                Console.WriteLine("Consumers: {0}", queue.Consumers);
                Console.WriteLine("Idle Since: {0}", queue.IdleSince);
                Console.WriteLine("****************************************************");
                Console.WriteLine();
            }
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Purge_Queue()
        {
            var response = Client.VirtualHost
                                 .Queue
                                 .Purge(Settings.Default.Queue)
                                 .Response();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}