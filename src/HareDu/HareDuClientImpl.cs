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

namespace HareDu
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Contracts;
    using Internal;
    using Resources;

    internal class HareDuClientImpl :
        Logging,
        HareDuClient
    {
        public HareDuClientImpl(HareDuClientBehaviorImpl args) :
            base(args.Logger)
        {
            Behavior = args;
        }

        private HareDuClientBehaviorImpl Behavior { get; set; }
        public HttpClient Client { get; set; }

        public T Factory<T>(Action<UserCredentials> userCredentials)
            where T : ResourceClient
        {
            var userCredentialsImpl = new UserCredentialsImpl();
            userCredentials(userCredentialsImpl);

            var uri = new Uri(string.Format("{0}/", Behavior.HostUrl));
            var handler = new HttpClientHandler
                              {
                                  Credentials =
                                      new NetworkCredential(userCredentialsImpl.Username, userCredentialsImpl.Password)
                              };
            var client = new HttpClient(handler);
            client.BaseAddress = uri;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (Behavior.Timeout != TimeSpan.Zero)
                client.Timeout = Behavior.Timeout;

            Client = client;

            var type = typeof (T);
            var implClass = GetType().Assembly
                                     .GetTypes()
                                     .FirstOrDefault(x => type.IsAssignableFrom(x) && !x.IsInterface);

            return (T) Activator.CreateInstance(implClass, client, Behavior.Logger);
        }

        public void CancelPendingRequests()
        {
            LogInfo("Cancel all pending requests.");

            Client.CancelPendingRequests();
        }
    }
}