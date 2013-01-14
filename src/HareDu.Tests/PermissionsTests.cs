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
    using NUnit.Framework;

    [TestFixture]
    public class PermissionsTests :
        HareDuTestBase
    {
        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Create_User_Persmissions()
        {
            var request = Client.User.Permissions.Set(Settings.Default.Username, x =>
                                                                                     {
                                                                                         x.SetConfigurePermissions(".*");
                                                                                         x.SetReadPermissions(".*");
                                                                                         x.SetWritePermissions(".*");
                                                                                     });

            Assert.AreEqual(HttpStatusCode.NoContent, request.Result.StatusCode);
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Delete_User_Permissions()
        {
            var request = Client.User.Permissions.Delete(Settings.Default.Username);
            Assert.AreEqual(HttpStatusCode.NoContent, request.Result.StatusCode);
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Return_All_User_Permissions()
        {
            var request = Client.User.Permissions.GetAll();

            foreach (var userPermissions in request.Result)
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

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Return_User_Permissions()
        {
            var permissions = Client.User.Permissions.Get(Settings.Default.Username).Result;

            Console.WriteLine("Virtual Host: {0}", permissions.VirtualHostName);
            Console.WriteLine("Username: {0}", permissions.Username);
            Console.WriteLine("Configure Permissions: {0}", permissions.ConfigurePermissions);
            Console.WriteLine("Read Permissions: {0}", permissions.ReadPermissions);
            Console.WriteLine("Write Permissions: {0}", permissions.WritePermissions);
            Console.WriteLine("****************************************************");
            Console.WriteLine();
        }
    }
}