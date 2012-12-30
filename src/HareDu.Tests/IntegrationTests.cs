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
    using NUnit.Framework;

    [TestFixture]
    public class IntegrationTests :
        HareDuTestBase
    {
        [Test, Explicit]
        public void Create_Virtual_Host()
        {
            try
            {
                CreateVirtualHost(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [Test]
        public void Create_Exchange()
        {
            try
            {
                CreateExchange();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [Test]
        public void Create_Queue()
        {
            try
            {
                CreateExchange();
                var createQueueRequest = Client.CreateQueue(
                    Settings.Default.VirtualHost,
                    Settings.Default.Queue,
                    x => x.IsDurable());
                createQueueRequest.Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        private void CreateExchange()
        {
            CreateVirtualHost(false);
            var createExchangeRequest = Client.CreateExchange(
                Settings.Default.VirtualHost,
                Settings.Default.Exchange,
                x =>
                    {
                        x.IsDurable();
                        x.RoutingType(ExchangeRoutingType.Fanout);
                    });
            createExchangeRequest.Wait();
        }

        private void CreateVirtualHost(bool useDefaultUser)
        {
            if (!useDefaultUser)
            {
                var createUserRequest = Client.CreateUser(
                    Settings.Default.Username,
                    x =>
                        {
                            x.WithPassword(Settings.Default.UserPassword);
                            x.WithTags(PermissionTag.Admin);
                        });
            }

            var createVirtualHostRequest = Client.CreateVirtualHost(Settings.Default.VirtualHost);
            createVirtualHostRequest.Wait();

            var createUserPremissionsRequest = Client.CreateUserPermissions(
                Settings.Default.VirtualHost,
                useDefaultUser ? Settings.Default.LoginUsername : Settings.Default.Username,
                    x =>
                    {
                        x.AssignConfigurePermissions(".*");
                        x.AssignReadPermissions(".*");
                        x.AssignWritePermissions(".*");
                    });
            createUserPremissionsRequest.Wait();
        }
    }
}