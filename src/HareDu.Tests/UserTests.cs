// Copyright 2013-2014 Albert L. Hives, Chris Patterson, et al.
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
    using Resources;

    [TestFixture]
    public class UserTests :
        HareDuTestBase
    {
        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Create_User()
        {
            var response = Client
                                .Factory<UserResources>(
                    x => x.Credentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                //.User
                                 .New(string.Format("{0}1", Settings.Default.Username), x =>
                                                                                            {
                                                                                                x.WithPassword(
                                                                                                    Settings.Default
                                                                                                            .UserPassword);
                                                                                                x.WithTags(
                                                                                                    y =>
                                                                                                    y.Administrator());
                                                                                            })
                                 .Response();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Create_User_With_Multiple_Tags()
        {
            var response = Client
                                .Factory<UserResources>(
                    x => x.Credentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                //.User
                                 .New(string.Format("{0}2", Settings.Default.Username), x =>
                                                                                            {
                                                                                                x.WithPassword(
                                                                                                    Settings.Default
                                                                                                            .UserPassword);
                                                                                                x.WithTags(y =>
                                                                                                               {
                                                                                                                   y
                                                                                                                       .Monitoring
                                                                                                                       ();
                                                                                                                   y
                                                                                                                       .Management
                                                                                                                       ();
                                                                                                               });
                                                                                            })
                                 .Response();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Delete_User()
        {
            var response = Client
                                .Factory<UserResources>(
                    x => x.Credentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                //.User
                                 .Delete(Settings.Default.Username)
                                 .Response();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Return_All_Users()
        {
            var data = Client
                                .Factory<UserResources>(
                    x => x.Credentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                //.User
                             .GetAll()
                             .Data();

            foreach (var user in data)
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
            var data = Client
                                .Factory<UserResources>(
                    x => x.Credentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                //.User
                             .Get(Settings.Default.Username)
                             .Data();

            Console.WriteLine("Username: {0}", data.Name);
            Console.WriteLine("Password Hash: {0}", data.PasswordHash);
            Console.WriteLine("Tags: {0}", data.Tags);
            Console.WriteLine("****************************************************");
            Console.WriteLine();
        }

        [Test, Category("Integration"), Explicit]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Verify_Exception_Thrown_When_Password_And_Tags_Missing_When_Creating_User()
        {
            var response = Client
                                .Factory<UserResources>(
                    x => x.Credentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                //.User
                                 .New(Settings.Default.Username, null)
                                 .Response();
        }

        [Test, Category("Integration"), Explicit]
        [ExpectedException(typeof (ArgumentException))]
        public void Verify_Exception_Thrown_When_Password_Missing_When_Creating_User()
        {
            var response = Client
                                .Factory<UserResources>(
                    x => x.Credentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                //.User
                                 .New(Settings.Default.Username, x => x.WithTags(y => y.Administrator()))
                                 .Response();
        }

        [Test, Category("Integration"), Explicit]
        [ExpectedException(typeof (ArgumentException))]
        public void Verify_Exception_Thrown_When_Username_Missing_When_Creating_User()
        {
            var response = Client
                                .Factory<UserResources>(
                    x => x.Credentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                //.User
                                 .New(null, x =>
                                                {
                                                    x.WithPassword(Settings.Default.UserPassword);
                                                    x.WithTags(y => y.Administrator());
                                                })
                                 .Response();
        }
    }
}