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
    using System.Net.Http;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Logging;

    public abstract class HareDuResourcesBase :
        Logging
    {
        public HareDuResourcesBase(HttpClient client, ILog logger) :
            base(logger)
        {
            Client = client;
        }

        protected HttpClient Client { get; set; }
        //protected HareDuClientBehaviorImpl Behavior { get; private set; }

        /// <summary>
        /// Overrides default behaviour of System.Uri because RabbitMQ uses a forward slash, "/" , to represent the default virtual host.
        /// This method is just a workaround.
        /// </summary>
        private void LeaveDotsAndSlashesEscaped()
        {
            var getSyntaxMethod =
                typeof (UriParser).GetMethod("GetSyntax", BindingFlags.Static | BindingFlags.NonPublic);
            if (getSyntaxMethod == null)
            {
                throw new MissingMethodException("UriParser", "GetSyntax");
            }

            var uriParser = getSyntaxMethod.Invoke(null, new object[] {"http"});

            var setUpdatableFlagsMethod =
                uriParser.GetType().GetMethod("SetUpdatableFlags", BindingFlags.Instance | BindingFlags.NonPublic);
            if (setUpdatableFlagsMethod == null)
            {
                throw new MissingMethodException("UriParser", "SetUpdatableFlags");
            }

            setUpdatableFlagsMethod.Invoke(uriParser, new object[] {0});
        }

        protected virtual Task<HttpResponseMessage> Get(string url, CancellationToken cancellationToken =
                                                                        default(CancellationToken))
        {
            try
            {
                if (url.Contains("/%2f"))
                    LeaveDotsAndSlashesEscaped();

                return Client.GetAsync(url, cancellationToken);
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }

        protected virtual Task<HttpResponseMessage> Delete(string url, CancellationToken cancellationToken =
                                                                           default(CancellationToken))
        {
            try
            {
                if (url.Contains("/%2f"))
                    LeaveDotsAndSlashesEscaped();

                return Client.DeleteAsync(url, cancellationToken);
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }

        protected virtual Task<HttpResponseMessage> Put<T>(string url, T value, CancellationToken cancellationToken =
                                                                                    default(CancellationToken))
        {
            try
            {
                if (url.Contains("/%2f"))
                    LeaveDotsAndSlashesEscaped();

                return Client.PutAsJsonAsync(url, value, cancellationToken);
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }

        protected virtual Task<HttpResponseMessage> Post<T>(string url, T value, CancellationToken cancellationToken =
                                                                                     default(CancellationToken))
        {
            try
            {
                if (url.Contains("/%2f"))
                    LeaveDotsAndSlashesEscaped();

                return Client.PostAsJsonAsync(url, value, cancellationToken);
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }
    }
}