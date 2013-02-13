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
    using System.Threading;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using Resources;

    [TestFixture]
    public class AsyncTests :
        HareDuTestBase
    {
        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Cancel_Request()
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            var task = Task.Factory
                           .StartNew(() => Console.WriteLine("Starting up a do nothing task"), token)
                           .ContinueWith(t =>
                                             {
                                                 try
                                                 {
                                                     tokenSource.Cancel();
                                                     var request = Client
                                                         .Factory<VirtualHostResources>(
                                                             x =>
                                                             x.Credentials(Settings.Default.LoginUsername,
                                                                     Settings.Default.LoginPassword))
                                                         .Queue
                                                         .New(string.Format("{0}6",
                                                                            Settings.Default.Queue), x => x.Source(
                                                                                Settings.Default.VirtualHost),
                                                              x => x.IsDurable(),
                                                              token);
                                                 }
                                                 catch (AggregateException ex)
                                                 {
                                                     foreach (var e in ex.InnerExceptions)
                                                         Console.WriteLine(e.Message);
                                                 }
                                             });
            //Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}