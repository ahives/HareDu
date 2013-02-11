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
    using Concerns;
    using NUnit.Framework;

    [TestFixture]
    public class PolicyTests :
        HareDuTestBase
    {
        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Create_New_Policy()
        {
            var response = Client
                .EstablishConnection<PolicyResources>(
                    x => x.Using(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                .New(x => x.Source(Settings.Default.Policy, Settings.Default.VirtualHost),
                     x =>
                         {
                             x.UsingPattern(@"^amq\.");
                             x.DefinedAs(
                                 y => y.Set("federation-upstream-set", "all"));
                             x.WithPriority(1);
                         })
                .Response();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            Console.WriteLine(response.ServerResponseReason);
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Return_All_Policies()
        {
            var data = Client
                                .EstablishConnection<PolicyResources>(
                    x => x.Using(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                //.Policy
                             .GetAll()
                             .Data();
            foreach (var policy in data)
            {
                Console.WriteLine("Pattern: {0} ", policy.Pattern);
                Console.WriteLine("Definitions");
                foreach (var definition in policy.Definition)
                    Console.WriteLine("\t{0}:{1}", definition.Key, definition.Value);
                Console.WriteLine("Priority: {0}", policy.Priority);
            }
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Return_All_Policies_In_VirtualHost()
        {
            var data = Client
                                .EstablishConnection<PolicyResources>(
                    x => x.Using(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                             .GetAll(x => x.Source(Settings.Default.VirtualHost))
                             .Data();
            foreach (var policy in data)
            {
                Console.WriteLine("Pattern: {0} ", policy.Pattern);
                Console.WriteLine("Definitions");
                foreach (var definition in policy.Definition)
                    Console.WriteLine("\t{0}:{1}", definition.Key, definition.Value);
                Console.WriteLine("Priority: {0}", policy.Priority);
            }
        }
    }
}