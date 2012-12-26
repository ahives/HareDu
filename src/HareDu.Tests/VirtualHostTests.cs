namespace HareDu.Tests
{
    using System;
    using System.Collections.Generic;
    using Model;
    using NUnit.Framework;

    [TestFixture]
    public class VirtualHostTests :
        HareDuTestBase
    {
        [Test]
        public void Verify_Can_Delete_Virtual_Host()
        {
            var request = Client.DeleteVirtualHost(Settings.Default.VirtualHost);

            Assert.AreEqual(true, request.Result.IsSuccessStatusCode);
        }

        [Test]
        public void Verify_Can_Delete_Virtual_Host_Using_TPL()
        {
            var request = Client.DeleteVirtualHost(Settings.Default.VirtualHost);
            var response = request.ContinueWith(x => { Assert.AreEqual(true, x.Result.IsSuccessStatusCode); });
            response.Wait();
        }

        [Test]
        public void Verify_Can_Get_All_Virtual_Hosts()
        {
            var vhosts = Client.GetListOfAllVirtualHosts()
                               .Result
                               .GetResponse<IEnumerable<VirtualHost>>();

            foreach (var vhost in vhosts)
            {
                Console.WriteLine("Name: {0}", vhost.Name);
                Console.WriteLine("Tracing: {0}", vhost.Tracing);
                Console.WriteLine("****************************************************");
                Console.WriteLine();
            }
        }

        [Test]
        public void Verify_Can_Get_All_Virtual_Hosts_Using_TPL()
        {
            var request = Client.GetListOfAllVirtualHosts();
            var response = request.ContinueWith(x =>
                                                    {
                                                        var vhosts = x.Result.GetResponse<IEnumerable<VirtualHost>>();
                                                        foreach (var vhost in vhosts)
                                                        {
                                                            Console.WriteLine("Name: {0}", vhost.Name);
                                                            Console.WriteLine("Tracing: {0}", vhost.Tracing);
                                                            Console.WriteLine("****************************************************");
                                                            Console.WriteLine();
                                                        }
                                                    });
            response.Wait();
        }

        [Test]
        public void Verify_Create_Virtual_Host_Is_Working()
        {
            var request = Client.CreateVirtualHost(Settings.Default.VirtualHost).Result;

            Assert.AreEqual(true, request.IsSuccessStatusCode);
        }

        [Test]
        public void Verify_Create_Virtual_Host_Is_Working_Using_TPL()
        {
            var request = Client.CreateVirtualHost(Settings.Default.VirtualHost);
            var response = request.ContinueWith(x => { Assert.AreEqual(true, x.Result.IsSuccessStatusCode); });
            response.Wait();
        }
    }
}