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
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using Model;
    using NUnit.Framework;

    [TestFixture]
    public class HareDuClientTests
    {
        [SetUp]
        public void SetUp()
        {
            _client = HareDuFactory.New(x =>
                                            {
                                                x.ConnectTo("http://localhost:55672");
                                                x.UsingCredentials("guest", "guest");
                                            });
        }

        private HareDuClient _client;
        private string _vhost = "buspaas";
        private bool _enableTracing = true;

        [Test]
        public void Test_Exchanges()
        {
            string exchangeName = "Test123";
            //var exhangeputreq = new CreateExchangeArgsImpl
            //                        {
            //                            ExchangeName = exchangeName,
            //                            Type = "direct",
            //                            VirtualHostName = _vhost
            //                        };
            //_client.CreateExchange(_vhost, exchangeName, x =>
            //                                                 {
            //                                                     x.IsDurable();
            //                                                     x.RoutingType("direct");
            //                                                 });

            ////Test Get
            //var exchange = _client.GetExchange(_vhost, exchangeName);
            //Assert.AreEqual(exchangeName, exchange.Name);

            ////Test List

            //var exchanges = _client.GetListOfAllExchangesInVirtualHost(_vhost);
            //Assert.IsNotNull(exchanges);
            //if (_enableTracing)
            //    exchanges.ToList().ForEach(p => Console.WriteLine(p.Name));

            //Assert.AreEqual(1, exchanges.Count(p => p.Name == exchangeName));

            ////Test Delete
            //_client.DeleteExchange(_vhost, exchangeName);
            //exchanges = _client.GetListOfAllExchangesInVirtualHost(_vhost);
            //Assert.AreEqual(0, exchanges.Count(p => p.Name == exchangeName));
        }

        [Test]
        public void Test_Exchanges_Binding()
        {
            string exchangeName = "provider";
            //Test Get
            var requestTask = _client.GetListOfAllBindingsOnExchange(_vhost, exchangeName, true);
            var responseTask = requestTask.ContinueWith(x =>
                                                            {
                                                                var response = x.Result;
                                                                var bindings =
                                                                    response.GetResponse<IEnumerable<Binding>>()
                                                                            .ToList();
                                                                if (_enableTracing)
                                                                    bindings.ForEach(p => Console.WriteLine(p.Source));
                                                            });
            responseTask.Wait();
            //Assert.IsNotNull(requestTask);
            //if (_enableTracing)
            //    requestTask.ToList().ForEach(p => Console.WriteLine(p.Source));


            //Test Get
            //requestTask = _client.GetListOfAllBindingsOnExchange(_vhost, exchangeName, false);
            //Assert.IsNotNull(requestTask);
            //if (_enableTracing)
            //    requestTask.ToList().ForEach(p => Console.WriteLine(p.Source));
        }

        [Test]
        public void Test_GetListOfAllQueuesInVirtualHost()
        {
            //var client = new HareDuClient("http://localhost", 15672, "guest", "guest");
            //var queues = client.GetListOfAllQueuesInVirtualHost("/");
            //foreach (var queue in queues)
            //{
            //    Console.WriteLine(queue);
            //}
        }

        [Test]
        public void Verify_Binding_Queue_To_Exchange_Working()
        {
            //var client = new HareDuClientImpl("http://localhost", 55672, "guest", "guest");
            _client.BindQueueToExchange("/", "RouteControllerTest", "HareDuTestQueue3",
                                        x => x.UsingRoutingKey(string.Empty));
        }

        [Test]
        public void Verify_Creating_Queues_Are_Working()
        {
            //var client = new HareDuClientImpl("http://localhost", 15672, "guest", "guest");
            var tokenSource = new CancellationTokenSource();
            _client.CreateQueue("hydro", "MyFluentAPITest2", x => { x.IsDurable(); }, tokenSource.Token);
            //client.CreateQueue("/", "rabbit@ahives-t5500", "MyFluentAPITest1", x =>
            //{
            //    x.Durable();
            //});
        }

        [Test]
        public void Verify_GetInfoOnOpenChannels_Working()
        {
            //var client = new HareDuClientImpl("http://localhost", 15672, "guest", "guest");
            var channels = _client.GetListOfAllOpenChannels().Result.Content.ReadAsAsync<IEnumerable<Channel>>().Result;

            foreach (var channelInfo in channels)
            {
                Console.WriteLine(channelInfo.MessageStats.Published);
                Console.WriteLine(channelInfo.MessageStats.Acknowledged);
                //Console.WriteLine(channelInfo.MessageStats.Delivered);
                //Console.WriteLine(channelInfo.MessageStats.DeliveredOrGet);
                //Console.WriteLine(channelInfo.MessageStats.Unacknowledged);
                //Console.WriteLine(channelInfo.MessageStats.Unconfirmed);
                //Console.WriteLine(channelInfo.MessageStats.Uncommitted);
                //Console.WriteLine(channelInfo.MessageStats.AcknowledgesUncommitted);
            }
        }
    }
}