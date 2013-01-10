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
    public class ExchangeTests
        //HareDuTestBase
    {
        [SetUp]
        public void Setup()
        {
            Client = HareDuFactory.New(x =>
                                           {
                                               x.ConnectTo(Settings.Default.HostUrl);
                                               x.UsingCredentials(Settings.Default.LoginUsername,
                                                                  Settings.Default.LoginPassword);
                                               x.OnVirtualHost(Settings.Default.VirtualHost);
                                               x.EnableLogging("HarDuLogger");
                                           });
        }

        private HareDuClient Client { get; set; }

        [Test, Category("Integration")]
        public void Verify_Can_Create_Exchange()
        {
            var request = Client.Exchange.Create(Settings.Default.Exchange, x =>
                                                                                {
                                                                                    x.IsDurable();
                                                                                    x.UsingRoutingType(
                                                                                        ExchangeRoutingType.Fanout);
                                                                                });

            Assert.AreEqual(HttpStatusCode.NoContent, request.Result.StatusCode);
        }

        [Test, Category("Integration")]
        public void Verify_Can_Delete_Exchanges()
        {
            var response = Client.Exchange.Delete(Settings.Default.Exchange);

            Assert.AreEqual(HttpStatusCode.NoContent, response.Result.StatusCode);
        }

        [Test, Category("Integration")]
        public void Verify_Can_Return_All_Bindings_On_Exchange()
        {
            var response = Client.Exchange.GetAllBindings(Settings.Default.Exchange, true);

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

        [Test, Category("Integration")]
        public void Verify_Can_Return_All_Exchanges()
        {
            var response = Client.Exchange.GetAll();

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

        //[Test]
        //public void Verify_Can_Return_All_Exchanges_In_Virtual_Host()
        //{
        //    var response = Client.GetAllExchangesInVirtualHost(Settings.Default.VirtualHost);

        //    foreach (var exchange in response.Result)
        //    {
        //        Console.WriteLine("Name: {0}", exchange.Name);
        //        Console.WriteLine("Type: {0}", exchange.Type);
        //        Console.WriteLine("Virtual Host: {0}", exchange.VirtualHostName);
        //        Console.WriteLine("Durable: {0}", exchange.IsDurable);
        //        Console.WriteLine("Internal: {0}", exchange.IsInternal);
        //        Console.WriteLine("Auto delete: {0}", exchange.IsSetToAutoDelete);
        //        Console.WriteLine("****************************************************");
        //        Console.WriteLine();
        //    }
        //}

        [Test, Category("Integration")]
        public void Verify_Can_Return_An_Exchange()
        {
            var exchange = Client.Exchange.Get(Settings.Default.Exchange).Result;

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