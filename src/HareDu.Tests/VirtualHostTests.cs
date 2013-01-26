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
    using System.Threading;
    using NUnit.Framework;

    [TestFixture]
    public class VirtualHostTests :
        HareDuTestBase
    {
        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Delete_Virtual_Host()
        {
            var response = Client.VirtualHost
                                 .Delete(Settings.Default.VirtualHost)
                                 .Response();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Get_All_Virtual_Hosts()
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            tokenSource.Cancel();
            var data = Client.VirtualHost
                             .GetAll(token)
                             .Data();

            foreach (var vhost in data)
            {
                Console.WriteLine("Name: {0}", vhost.Name);
                Console.WriteLine("Tracing: {0}", vhost.Tracing);
                Console.WriteLine("****************************************************");
                Console.WriteLine();
            }
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Create_Virtual_Host_Is_Working()
        {
            var response = Client.VirtualHost
                                 .New(Settings.Default.VirtualHost)
                                 .Response();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Server_Working()
        {
            var response = Client.VirtualHost
                                 .Change(Settings.Default.VirtualHost, x =>
                                                                           {
                                                                               x.SetUsername(
                                                                                   Settings.Default.LoginUsername);
                                                                               x.SetPassword(
                                                                                   Settings.Default.LoginPassword);
                                                                           })
                                 .IsAlive()
                                 .Response();
            Assert.AreEqual(null, response.Status);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test, Category("Integration"), Explicit]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Verify_Throw_Exception_When_Virtual_Host_Missing()
        {
            var request = Client.VirtualHost
                                .Delete(string.Empty)
                                .Response();
            Assert.AreNotEqual(HttpStatusCode.NoContent, request.StatusCode);
        }
    }
}