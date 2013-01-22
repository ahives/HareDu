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

    [TestFixture]
    public class ExchangeTests :
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
        public void Verify_Can_Create_Exchange()
        {
            var response = Client.VirtualHost
                                 .Exchange
                                 .New("NewExchange2", x =>
                                                          {
                                                              x.IsDurable();
                                                              x.UsingRoutingType(y => y.Fanout());
                                                          })
                                 .Response();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Delete_Exchanges()
        {
            var response = Client.VirtualHost
                                 .Exchange
                                 .Delete(Settings.Default.Exchange)
                                 .Response();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Return_All_Bindings_On_Destination()
        {
            var response = Client.VirtualHost
                                 .Exchange
                                 .GetAllBindings(Settings.Default.Exchange, x => x.Destination());

            foreach (var binding in response.Result)
            {
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

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Return_All_Bindings_On_Source()
        {
            var response = Client.VirtualHost
                                 .Exchange
                                 .GetAllBindings(Settings.Default.Exchange, x => x.Source());

            foreach (var binding in response.Result)
            {
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

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Return_All_Exchanges()
        {
            var response = Client.VirtualHost
                                 .Exchange
                                 .GetAll();

            foreach (var exchange in response.Result)
            {
                Console.WriteLine("Name: {0}", exchange.Name);
                Console.WriteLine("Type: {0}", exchange.Type);
                Console.WriteLine("Virtual Host: {0}", exchange.VirtualHostName);
                Console.WriteLine("Durable: {0}", exchange.IsDurable);
                Console.WriteLine("Internal: {0}", exchange.IsInternal);
                Console.WriteLine("Auto delete: {0}", exchange.IsSetToAutoDelete);
                Console.WriteLine("****************************************************");
                Console.WriteLine();
            }
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Return_An_Exchange()
        {
            var exchange = Client.VirtualHost
                                 .Exchange
                                 .Get(Settings.Default.Exchange)
                                 .Result;

            Console.WriteLine("Name: {0}", exchange.Name);
            Console.WriteLine("Type: {0}", exchange.Type);
            Console.WriteLine("Virtual Host: {0}", exchange.VirtualHostName);
            Console.WriteLine("Durable: {0}", exchange.IsDurable);
            Console.WriteLine("Internal: {0}", exchange.IsInternal);
            Console.WriteLine("Auto delete: {0}", exchange.IsSetToAutoDelete);
            Console.WriteLine("****************************************************");
            Console.WriteLine();
        }
    }
}