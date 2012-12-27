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
namespace HareDu
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class HareDuClientBase
    {
        protected HareDuClientBase(HareDuInitArgsImpl args)
        {
            args.HostUrl.CheckIfArgValid("hostUrl");
            args.Username.CheckIfArgValid("username");
            args.Password.CheckIfArgValid("password");

            Client = new HttpClient(new HttpClientHandler
                                        {
                                            Credentials = new NetworkCredential(args.Username, args.Password)
                                        }) {BaseAddress = new Uri(string.Format("{0}/", args.HostUrl))};
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected HttpClient Client { get; set; }

        /// <summary>
        /// this method is to add workaound for isssue using forword shlash ('/') in uri
        /// default virtualHostName in RabbitMQ is named as '/' but '/' is uri charter so RabbitMQ suggest to uses '%2f' encoded character while passing default host name in URI
        /// but System.URI class replaces this encoded char with '/' which changes symantics fo URI. 
        /// This method is to overide the default System.Uri behaviour 
        /// </summary>
        protected virtual void LeaveDotsAndSlashesEscaped()
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

        protected virtual Task<HttpResponseMessage> Get(string url)
        {
            if (url.Contains("/%2f"))
                LeaveDotsAndSlashesEscaped();

            return Client.GetAsync(url);
        }

        protected virtual Task<HttpResponseMessage> Get(string url, CancellationToken cancellationToken)
        {
            if (url.Contains("/%2f"))
                LeaveDotsAndSlashesEscaped();

            return Client.GetAsync(url, cancellationToken);
        }

        protected virtual Task<HttpResponseMessage> Delete(string url)
        {
            if (url.Contains("/%2f"))
                LeaveDotsAndSlashesEscaped();

            return Client.DeleteAsync(url);
        }

        protected virtual Task<HttpResponseMessage> Delete(string url, CancellationToken cancellationToken)
        {
            if (url.Contains("/%2f"))
                LeaveDotsAndSlashesEscaped();

            return Client.DeleteAsync(url, cancellationToken);
        }

        protected virtual Task<HttpResponseMessage> Put<T>(string url, T value)
        {
            if (url.Contains("/%2f"))
                LeaveDotsAndSlashesEscaped();

            return Client.PutAsJsonAsync(url, value);
        }

        protected virtual Task<HttpResponseMessage> Put<T>(string url, T value, CancellationToken cancellationToken)
        {
            if (url.Contains("/%2f"))
                LeaveDotsAndSlashesEscaped();

            return Client.PutAsJsonAsync(url, value, cancellationToken);
        }

        protected virtual Task<HttpResponseMessage> Post<T>(string url, T value)
        {
            if (url.Contains("/%2f"))
                LeaveDotsAndSlashesEscaped();

            return Client.PostAsJsonAsync(url, value);
        }

        protected virtual Task<HttpResponseMessage> Post<T>(string url, T value, CancellationToken cancellationToken)
        {
            if (url.Contains("/%2f"))
                LeaveDotsAndSlashesEscaped();

            return Client.PostAsJsonAsync(url, value, cancellationToken);
        }
    }
}