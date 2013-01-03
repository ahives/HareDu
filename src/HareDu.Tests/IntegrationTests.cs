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
    public class IntegrationTests :
        HareDuTestBase
    {
        [Test]
        public void Return_All_Virtual_Hosts()
        {
            //var vhosts = Client.GetAllVirtualHosts()
            //                   .Result
            //                   .GetResponse<IEnumerable<VirtualHost>>();
            //IResult result = Client.GetAllVirtualHosts()
            //                   .Result
            //                   .GetResponse<IEnumerable<VirtualHost>>();
            //var request = result.GetTask();
            //var rst = result.GetResponse();

            //var vhosts = Client.GetAllVirtualHosts().GetResponse();
            //foreach (var vhost in vhosts)
            //{
            //    Console.WriteLine("Name: {0}", vhost.Name);
            //    Console.WriteLine("Tracing: {0}", vhost.Tracing);
            //    Console.WriteLine("****************************************************");
            //    Console.WriteLine();
            //}
        }

        //[Test, Explicit]
        //public void Create_Virtual_Host()
        //{
        //    try
        //    {
        //        CreateVirtualHost(true);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //}

        //[Test, Explicit]
        //public void Create_Exchange()
        //{
        //    try
        //    {
        //        CreateExchange();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //}

        //[Test, Explicit]
        //public void Create_Queue()
        //{
        //    try
        //    {
        //        CreateExchange();
        //        Client.CreateQueue(
        //            Settings.Default.VirtualHost,
        //            Settings.Default.Queue,
        //            x => x.IsDurable()).GetAsyncTask().Wait();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //}


        //private void CreateExchange()
        //{
        //    CreateVirtualHost(false);
        //    Client.CreateExchange(Settings.Default.Exchange,
        //        Settings.Default.VirtualHost, x =>
        //                                          {
        //                                              x.IsDurable();
        //                                              x.RoutingType(ExchangeRoutingType.Fanout);
        //                                          }).GetAsyncTask().Wait();
        //}

        //private void CreateVirtualHost(bool useDefaultUser)
        //{
        //    if (!useDefaultUser)
        //    {
        //        var createUserRequest = Client.CreateUser(
        //            Settings.Default.Username,
        //            x =>
        //                {
        //                    x.WithPassword(Settings.Default.UserPassword);
        //                    x.WithTags(PermissionTag.Admin);
        //                });
        //    }

        //    Client.CreateVirtualHost(Settings.Default.VirtualHost)
        //        .GetAsyncTask()
        //        .Wait();

        //    var createUserPremissionsRequest = Client.CreateUserPermissions(useDefaultUser ? Settings.Default.LoginUsername : Settings.Default.Username,
        //            Settings.Default.VirtualHost, x =>
        //                                              {
        //                                                  x.AssignConfigurePermissions(".*");
        //                                                  x.AssignReadPermissions(".*");
        //                                                  x.AssignWritePermissions(".*");
        //                                              });
        //    createUserPremissionsRequest.Wait();
        //}
    }
}