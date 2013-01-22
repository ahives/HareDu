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
    using System.Net;
    using NUnit.Framework;

    public class QueueBindingTests :
        HareDuTestBase
    {
        [SetUp]
        public new void Setup()
        {
            Client = HareDuFactory.New(x =>
                                           {
                                               x.ConnectTo(Settings.Default.HostUrl);
                                               x.UsingCredentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword);
                                               x.OnVirtualHost(Settings.Default.VirtualHost);
                                               x.EnableLogging("HareDuLogger");
                                           });
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Bind_to_Exchange()
        {
            var response = Client.VirtualHost
                                 .Queue
                                 .Binding
                                 .New(Settings.Default.Queue, Settings.Default.Exchange, x => x.UsingRoutingKey(Settings.Default.RoutingKey))
                                 .Response();
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Delete_Binding()
        {
            var response = Client.VirtualHost
                                 .Queue
                                 .Binding
                                 .Delete(Settings.Default.Queue, Settings.Default.Exchange, "fanout")
                                 .Response();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Get_All_Queue_Bindings()
        {
            var response = Client.VirtualHost
                                 .Queue
                                 .Binding
                                 .GetAll(Settings.Default.Queue);

            foreach (var queue in response.Result)
            {
                Console.WriteLine("Source: {0}", queue.Source);
                Console.WriteLine("Destination: {0}", queue.Destination);
                Console.WriteLine("Destination Type: {0}", queue.DestinationType);
                Console.WriteLine("Virtual Host: {0}", queue.VirtualHostName);
                Console.WriteLine("Routing Key: {0}", queue.RoutingKey);
                Console.WriteLine("Properties Key: {0}", queue.PropertiesKey);
                Console.WriteLine("****************************************************");
                Console.WriteLine();
            }
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Get_Queue_Binding()
        {
            var binding = Client.VirtualHost
                                .Queue
                                .Binding
                                .Get(Settings.Default.Queue, Settings.Default.Exchange, "fanout")
                                .Result;

            Console.WriteLine("Source: {0}", binding.Source);
            Console.WriteLine("Destination: {0}", binding.Destination);
            Console.WriteLine("Destination Type: {0}", binding.DestinationType);
            Console.WriteLine("Virtual Host: {0}", binding.VirtualHostName);
            Console.WriteLine("Routing Key: {0}", binding.RoutingKey);
            Console.WriteLine("Properties Key: {0}", binding.PropertiesKey);
            Console.WriteLine("****************************************************");
            Console.WriteLine();
        }
    }
}