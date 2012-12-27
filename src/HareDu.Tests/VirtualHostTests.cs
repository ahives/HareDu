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
                                                            Console.WriteLine(
                                                                "****************************************************");
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