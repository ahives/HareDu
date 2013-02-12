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
    public class ParameterTests :
        HareDuTestBase
    {
        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Add_New_Parameter()
        {
            var response = Client
                                .RequestResource<ParameterResources>(
                    x => x.Credentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                                 //.Parameter
                                 .New(x =>
                                          {
                                              x.For(Settings.Default.Component, Settings.Default.Parameter);
                                              x.On(Settings.Default.VirtualHost);
                                          })
                                 .Response();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Delete_Parameter()
        {
            var response = Client
                                .RequestResource<ParameterResources>(
                    x => x.Credentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                                 //.Parameter
                                 .Delete(x =>
                                             {
                                                 x.For(Settings.Default.Component, Settings.Default.Parameter);
                                                 x.On(Settings.Default.VirtualHost);
                                             })
                                 .Response();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Return_All_Parameters()
        {
            var data = Client
                                .RequestResource<ParameterResources>(
                    x => x.Credentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                             //.Parameter
                             .GetAll()
                             .Data();

            foreach (var parameter in data)
            {
                Console.WriteLine("Component: {0}", parameter.Component);
                Console.WriteLine("Name: {0}", parameter.Name);
                Console.WriteLine("VirtualHost: {0}", parameter.VirtualHost);
            }
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Return_All_Parameters_For_Given_Component()
        {
            var data = Client
                                .RequestResource<ParameterResources>(
                    x => x.Credentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                             //.Parameter
                             .GetAll(Settings.Default.Component)
                             .Data();

            foreach (var parameter in data)
            {
                Console.WriteLine("Component: {0}", parameter.Component);
                Console.WriteLine("Name: {0}", parameter.Name);
                Console.WriteLine("VirtualHost: {0}", parameter.VirtualHost);
            }
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Return_All_Parameters_For_Given_Component_On_Given_VirtualHost()
        {
            var data = Client
                                .RequestResource<ParameterResources>(
                    x => x.Credentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                             //.Parameter
                             .GetAll(x =>
                                         {
                                             x.For(Settings.Default.Component);
                                             x.On(Settings.Default.VirtualHost);
                                         })
                             .Data();

            foreach (var parameter in data)
            {
                Console.WriteLine("Component: {0}", parameter.Component);
                Console.WriteLine("Name: {0}", parameter.Name);
                Console.WriteLine("VirtualHost: {0}", parameter.VirtualHost);
            }
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Return_Parameter_For_Given_Component_On_Given_VirtualHost()
        {
            var data = Client
                                .RequestResource<ParameterResources>(
                    x => x.Credentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                             //.Parameter
                             .Get(x =>
                                      {
                                          x.For(Settings.Default.Component, Settings.Default.Parameter);
                                          x.On(Settings.Default.VirtualHost);
                                      })
                             .Data();

            Console.WriteLine("Component: {0}", data.Component);
            Console.WriteLine("Name: {0}", data.Name);
            Console.WriteLine("VirtualHost: {0}", data.VirtualHost);
        }
    }
}