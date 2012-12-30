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
    public class UserTests :
        HareDuTestBase
    {
        [Test]
        public void Verify_Can_Create_User()
        {
            var request = Client.CreateUser(Settings.Default.Username, x =>
                                                                       {
                                                                           x.WithPassword(Settings.Default.UserPassword);
                                                                           x.WithTags(
                                                                               Settings.Default.UserPermissionsTags);
                                                                       });

            Assert.AreEqual(true, request.Result.IsSuccessStatusCode);
        }

        [Test]
        public void Verify_Can_Delete_User()
        {
            var request = Client.DeleteUser(Settings.Default.Username);
            Assert.AreEqual(true, request.Result.IsSuccessStatusCode);
        }

        [Test]
        public void Verify_Can_Return_All_Users()
        {
            var request = Client.GetAllUsers()
                                .Result
                                .GetResponse<IEnumerable<User>>();

            foreach (var user in request)
            {
                Console.WriteLine("Name: {0}", user.Name);
                Console.WriteLine("Password Hash: {0}", user.PasswordHash);
                Console.WriteLine("Tags: {0}", user.Tags);
                Console.WriteLine("****************************************************");
                Console.WriteLine();
            }
        }

        [Test]
        public void Verify_Can_Return_Individual_User()
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