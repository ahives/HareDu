using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HareDu.Model;

namespace HareDu.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class HareDuClientTests
    {
        private HareDuClient _client;
        private string _vhost = "/";
        private bool _enableTracing = true;

        [SetUp]
        public void SetUp()
        {
            _client = new HareDuClient("http://localhost", 15672, "guest", "guest");
        }

        [Test]
        public void Verify_GetInfoOnOpenChannels_Working()
        {
            var client = new HareDuClient("http://localhost", 15672, "guest", "guest");
            var channels = client.GetListOfAllOpenChannels();

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

        [Test]
        public void Test_GetListOfAllQueuesInVirtualHost()
        {
            var client = new HareDuClient("http://localhost", 15672, "guest", "guest");
            var queues = client.GetListOfAllQueuesInVirtualHost("/");
            foreach (var queue in queues)
            {
                Console.WriteLine(queue);
            }
        }

        [Test]
        public void Test_Exchanges()
        {
            string exchangeName = "Test123";
            var exhangeputreq = new ExchangePutRequestParams()
                {
                    ExchangeName = exchangeName,
                    Type = "direct",
                    VirtualHostName = _vhost
                };
            _client.CreateExchange(exhangeputreq);

            //Test Get
            var exchange = _client.GetExchange(_vhost, exchangeName);
            Assert.AreEqual(exchangeName,exchange.Name);

            //Test List

            var exchanges = _client.GetListOfAllExchangesInVirtualHost(_vhost);
            Assert.IsNotNull(exchanges);
            if (_enableTracing)
                exchanges.ToList().ForEach(p => Console.WriteLine(p.Name));

            Assert.AreEqual(1, exchanges.Count(p => p.Name==exchangeName));
            
            //Test Delete
            _client.DeleteExchange(_vhost, exchangeName);
            exchanges = _client.GetListOfAllExchangesInVirtualHost(_vhost);
            Assert.AreEqual(0, exchanges.Count(p => p.Name == exchangeName));
        }

         [Test]
        public void Test_Exchanges_Binding()
        {
            string exchangeName = "provider";
            //Test Get
            var bindings = _client.GetListOfAllBindingsOnExchange(_vhost, exchangeName, true);
            Assert.IsNotNull(bindings);
            if (_enableTracing)
                bindings.ToList().ForEach(p => Console.WriteLine(p.Source));

           
           //Test Get
            bindings = _client.GetListOfAllBindingsOnExchange(_vhost, exchangeName, false);
            Assert.IsNotNull(bindings);
            if (_enableTracing)
                bindings.ToList().ForEach(p => Console.WriteLine(p.Source));
             
        }
       
    }
}
