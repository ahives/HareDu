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