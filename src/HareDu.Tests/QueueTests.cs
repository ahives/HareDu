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
    using Resources;

    [TestFixture]
    public class QueueTests :
        HareDuTestBase
    {
        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Create_Queue()
        {
            var response = Client
                .Factory<VirtualHostResources>(
                    x => x.Credentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                .Queue
                .New(x => x.Queue(string.Format("{0}5", Settings.Default.Queue)),
                     x => x.IsDurable(),
                     x => x.VirtualHost(Settings.Default.VirtualHost))
                .Response();

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Create_Queue_On_Specific_Node()
        {
            var response = Client
                .Factory<VirtualHostResources>(
                    x => x.Credentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                .Queue
                .New(x => x.Queue(string.Format("{0}4", Settings.Default.Queue)),
                     x =>
                         {
                             x.IsDurable();
                             x.OnNode(
                                 string.Format("rabbit@{0}",
                                               Environment.GetEnvironmentVariable("COMPUTERNAME")));
                         },
                     x => x.VirtualHost(Settings.Default.VirtualHost))
                .Response();

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Create_Queue_With_Arguments()
        {
            var response = Client
                .Factory<VirtualHostResources>(
                    x => x.Credentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                .Queue
                .New(x => x.Queue(string.Format("{0}4", Settings.Default.Queue)),
                     x =>
                         {
                             x.IsDurable();
                             x.WithArguments(y =>
                                                 {
                                                     y.SetPerQueueExpiration(1000);
                                                     y.SetQueueExpiration(1000);
                                                 });
                         },
                     x => x.VirtualHost(Settings.Default.VirtualHost))
                .Response();

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Delete_Queue()
        {
            var response = Client
                .Factory<VirtualHostResources>(
                    x => x.Credentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                .Queue
                .Delete(x => x.Queue(string.Format("{0}5", Settings.Default.Queue)),
                        x => x.VirtualHost(Settings.Default.VirtualHost))
                .Response();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Get_All_Queues()
        {
            var data = Client
                .Factory<VirtualHostResources>(
                    x => x.Credentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
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
        public void Verify_Can_Clear_Queue()
        {
            var response = Client
                .Factory<VirtualHostResources>(
                    x => x.Credentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                .Queue
                .Clear(x => x.Queue(Settings.Default.Queue), x => x.VirtualHost(Settings.Default.VirtualHost))
                .Response();

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}