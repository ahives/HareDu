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
    using System.Collections.Generic;
    using System.Net;
    using NUnit.Framework;

    [TestFixture]
    public class UserTests :
        HareDuTestBase
    {
        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Create_User()
        {
            var request = Client.User.New(Settings.Default.Username, x =>
                                                                            {
                                                                                x.WithPassword(
                                                                                    Settings.Default.UserPassword);
                                                                                x.WithTags(
                                                                                    new List<string>() { Settings.Default.UserPermissionsTags });
                                                                            });

            Assert.AreEqual(HttpStatusCode.NoContent, request.Result.StatusCode);
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Delete_User()
        {
            var response = Client.User.Delete(Settings.Default.Username).Result;
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Return_All_Users()
        {
            var response = Client.User.GetAll();

            foreach (var user in response.Result)
            {
                Console.WriteLine("Name: {0}", user.Name);
                Console.WriteLine("Password Hash: {0}", user.PasswordHash);
                Console.WriteLine("Tags: {0}", user.Tags);
                Console.WriteLine("****************************************************");
                Console.WriteLine();
            }
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Return_User()
        {
            var user = Client.User.Get(Settings.Default.Username).Result;

            Console.WriteLine("Username: {0}", user.Name);
            Console.WriteLine("Password Hash: {0}", user.PasswordHash);
            Console.WriteLine("Tags: {0}", user.Tags);
            Console.WriteLine("****************************************************");
            Console.WriteLine();
        }

        [Test, Category("Integration"), Explicit]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Verify_Exception_Thrown_When_Password_And_Tags_Missing_When_Creating_User()
        {
            var request = Client.User.New(Settings.Default.Username, null);
        }

        [Test, Category("Integration"), Explicit]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Verify_Exception_Thrown_When_Password_Missing_When_Creating_User()
        {
            var request = Client.User.New(Settings.Default.Username,
                                             x => x.WithTags(new List<string>() { Settings.Default.UserPermissionsTags }));
        }

        [Test, Category("Integration"), Explicit]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Verify_Exception_Thrown_When_Username_Missing_When_Creating_User()
        {
            var request = Client.User.New(null, x =>
                                                       {
                                                           x.WithPassword(Settings.Default.UserPassword);
                                                           x.WithTags(
                                                               new List<string>() { Settings.Default.UserPermissionsTags });
                                                       });
        }
    }
}