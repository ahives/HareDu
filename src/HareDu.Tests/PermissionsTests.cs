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
    public class PermissionsTests :
        HareDuTestBase
    {
        [Test]
        public void Verify_Can_Create_User_Persmissions()
        {
            var request = Client.CreateUserPermissions(
                Settings.Default.VirtualHost,
                Settings.Default.LoginUsername,
                x =>
                    {
                        x.AssignConfigurePermissions(".*");
                        x.AssignReadPermissions(".*");
                        x.AssignWritePermissions(".*");
                    });

            Assert.AreEqual(true, request.Result.IsSuccessStatusCode);
        }

        [Test]
        public void Verify_Can_Delete_User_Permissions()
        {
            var request = Client.DeleteUserPermissions(Settings.Default.VirtualHost, Settings.Default.LoginUsername);
            Assert.AreEqual(true, request.Result.IsSuccessStatusCode);
        }

        [Test]
        public void Verify_Can_Return_All_User_Permissions()
        {
            var request = Client.GetAlllUserPermissions()
                                .Result
                                .GetResponse<IEnumerable<UserPermissions>>();

            foreach (var userPermissions in request)
            {
                Console.WriteLine("Virtual Host: {0}", userPermissions.VirtualHostName);
                Console.WriteLine("Username: {0}", userPermissions.Username);
                Console.WriteLine("Configure Permissions: {0}", userPermissions.ConfigurePermissions);
                Console.WriteLine("Read Permissions: {0}", userPermissions.ReadPermissions);
                Console.WriteLine("Write Permissions: {0}", userPermissions.WritePermissions);
                Console.WriteLine("****************************************************");
                Console.WriteLine();
            }
        }

        [Test]
        public void Verify_Can_Return_Individual_User_Permissions()
        {
            var request = Client.GetIndividualUser(Settings.Default.LoginUsername)
                                .Result
                                .GetResponse<UserPermissions>();

            Console.WriteLine("Virtual Host: {0}", request.VirtualHostName);
            Console.WriteLine("Username: {0}", request.Username);
            Console.WriteLine("Configure Permissions: {0}", request.ConfigurePermissions);
            Console.WriteLine("Read Permissions: {0}", request.ReadPermissions);
            Console.WriteLine("Write Permissions: {0}", request.WritePermissions);
            Console.WriteLine("****************************************************");
            Console.WriteLine();
        }
    }
}