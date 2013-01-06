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
    using System.Net;
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
                                                                               x.WithPassword(
                                                                                   Settings.Default.UserPassword);
                                                                               x.WithTags(
                                                                                   Settings.Default.UserPermissionsTags);
                                                                           });

            Assert.AreEqual(HttpStatusCode.NoContent, request.Result.StatusCode);
        }


        [Test]
        public void Verify_Can_Delete_User()
        {
            var request = Client.DeleteUser(Settings.Default.Username);
            Assert.AreEqual(HttpStatusCode.NoContent, request.Result.StatusCode);
        }

        [Test]
        public void Verify_Can_Return_All_Users()
        {
            var users = Client.GetAllUsers().Result;

            foreach (var user in users)
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
            var user = Client.GetUser(Settings.Default.Username).Result;

            Console.WriteLine("Username: {0}", user.Name);
            Console.WriteLine("Password Hash: {0}", user.PasswordHash);
            Console.WriteLine("Tags: {0}", user.Tags);
            Console.WriteLine("****************************************************");
            Console.WriteLine();
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Verify_Exception_Thrown_When_Password_And_Tags_Missing_When_Creating_User()
        {
            var request = Client.CreateUser(Settings.Default.Username, null);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Verify_Exception_Thrown_When_Username_Missing_When_Creating_User()
        {
            var request = Client.CreateUser(null, x =>
                                                      {
                                                          x.WithPassword(Settings.Default.UserPassword);
                                                          x.WithTags(
                                                              Settings.Default.UserPermissionsTags);
                                                      });
        }
    }
}