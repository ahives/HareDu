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
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Model;
    using NUnit.Framework;

    [TestFixture]
    public class ExchangeTests :
        HareDuTestBase
    {
        //[Test]
        //public void Verify_Can_Create_Exchange()
        //{
        //    var request = Client.CreateExchange(Settings.Default.Exchange,
        //        Settings.Default.VirtualHost, x =>
        //                                          {
        //                                              x.IsDurable();
        //                                              x.RoutingType(ExchangeRoutingType.Fanout);
        //                                          }).GetHttpResponseMessage();

        //    Assert.AreEqual(true, request.IsSuccessStatusCode);
        //}

        //[Test]
        //public void Verify_Can_Delete_Exchanges()
        //{
        //    var request = Client.DeleteExchange(Settings.Default.Exchange, Settings.Default.VirtualHost).GetHttpResponseMessage();

        //    Assert.AreEqual(true, request.IsSuccessStatusCode);
        //}

        [Test]
        public void Verify_Can_Return_All_Bindings_On_Exchange()
        {
            var bindings = Client.GetAllBindingsOnExchange(Settings.Default.Exchange, Settings.Default.VirtualHost, true).GetResponse();

            foreach (var binding in bindings)
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

        [Test]
        public void Verify_Can_Return_An_Exchange()
        {
            var exchange = Client.GetExchange(Settings.Default.Exchange, Settings.Default.VirtualHost).GetResponse();

            Console.WriteLine("Name: {0}", exchange.Name);
            Console.WriteLine("Type: {0}", exchange.Type);
            Console.WriteLine("Virtual Host: {0}", exchange.VirtualHostName);
            Console.WriteLine("Durable: {0}", exchange.IsDurable);
            Console.WriteLine("Internal: {0}", exchange.IsInternal);
            Console.WriteLine("Auto delete: {0}", exchange.IsSetToAutoDelete);
            Console.WriteLine("****************************************************");
            Console.WriteLine();
        }

        [Test]
        public void Verify_Can_Return_All_Exchanges_In_Virtual_Host()
        {
            var exchanges = Client.GetAllExchangesInVirtualHost(Settings.Default.VirtualHost).GetResponse();

            foreach (var exchange in exchanges)
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

        [Test]
        public void Verify_Can_Return_All_Exchanges()
        {
            var exchanges = Client.GetAllExchanges().GetResponse();
                                  //.Result
                                  //.GetResponse<IEnumerable<Exchange>>();

            foreach (var exchange in exchanges)
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
    }
}