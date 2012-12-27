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
    using Model;
    using NUnit.Framework;

    [TestFixture]
    public class ExchangeTests :
        HareDuTestBase
    {
        [Test]
        public void Verify_Can_Create_Exchange()
        {
            var request = Client.CreateExchange(
                Settings.Default.VirtualHost,
                Settings.Default.Exchange,
                x =>
                    {
                        x.IsDurable();
                        x.RoutingType(ExchangeRoutingType.Fanout);
                    });

            Assert.AreEqual(true, request.Result.IsSuccessStatusCode);
        }

        [Test]
        public void Verify_Can_Create_Exchange_TPL()
        {
            var request = Client.CreateExchange(
                Settings.Default.VirtualHost,
                Settings.Default.Exchange,
                x =>
                    {
                        x.IsDurable();
                        x.RoutingType(ExchangeRoutingType.Fanout);
                    });
            var responseTask = request.ContinueWith(x => { Assert.AreEqual(true, x.Result.IsSuccessStatusCode); });
            responseTask.Wait();
        }

        [Test]
        public void Verify_Can_Delete_Exchanges()
        {
            var request = Client.DeleteExchange(Settings.Default.VirtualHost, Settings.Default.Exchange);

            Assert.AreEqual(true, request.Result.IsSuccessStatusCode);
        }

        [Test]
        public void Verify_Can_Delete_Exchanges_Using_TPL()
        {
            var request = Client.DeleteExchange(Settings.Default.VirtualHost, Settings.Default.Exchange);
            var response = request.ContinueWith(x => { Assert.AreEqual(true, x.Result.IsSuccessStatusCode); });
            response.Wait();
        }

        [Test]
        public void Verify_Can_Return_All_Exchanges()
        {
            var exchanges = Client.GetListOfAllExchanges()
                                  .Result
                                  .GetResponse<IEnumerable<Exchange>>();

            foreach (var exchange in exchanges)
            {
                Console.WriteLine("name: {0}", exchange.Name);
                Console.WriteLine("type: {0}", exchange.Type);
                Console.WriteLine("vhost: {0}", exchange.VirtualHostName);
                Console.WriteLine("durable: {0}", exchange.IsDurable);
                Console.WriteLine("internal: {0}", exchange.IsInternal);
                Console.WriteLine("auto delete: {0}", exchange.IsSetToAutoDelete);
                Console.WriteLine("****************************************************");
                Console.WriteLine();
            }
        }

        [Test]
        public void Verify_Can_Return_All_Exchanges_Using_TPL()
        {
            var exchanges = Client.GetListOfAllExchanges()
                                  .Result
                                  .GetResponse<IEnumerable<Exchange>>();

            foreach (var exchange in exchanges)
            {
                Console.WriteLine("name: {0}", exchange.Name);
                Console.WriteLine("type: {0}", exchange.Type);
                Console.WriteLine("vhost: {0}", exchange.VirtualHostName);
                Console.WriteLine("durable: {0}", exchange.IsDurable);
                Console.WriteLine("internal: {0}", exchange.IsInternal);
                Console.WriteLine("auto delete: {0}", exchange.IsSetToAutoDelete);
                Console.WriteLine("****************************************************");
                Console.WriteLine();
            }
        }
    }
}