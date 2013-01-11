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

namespace HareDu
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Logging;

    public abstract class HareDuClientBase
    {
        protected HareDuClientBase(ClientInitParamsImpl args)
        {
            Arg.Validate(args.HostUrl, "hostUrl",
                         () =>
                         LogError(
                             "HareDuClientBase constructor threw an ArgumentNullException exception because host URL was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(args.Username, "username",
                         () =>
                         LogError(
                             "HareDuClientBase constructor threw an ArgumentNullException exception because username was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(args.Password, "password",
                         () =>
                         LogError(
                             "HareDuClientBase constructor threw an ArgumentNullException exception because password was invalid (i.e. empty, null, or all whitespaces)"));
            Arg.Validate(args.VirtualHost, "virtualHostName",
                         () =>
                         LogError(
                             "HareDuClientBase constructor threw an ArgumentNullException exception because virtual host name was invalid (i.e. empty, null, or all whitespaces)"));

            Init = args;
            Logger = args.Logger;
            IsLoggingEnabled = !Logger.IsNull();
            Client = GetClient(Init.HostUrl, Init.Username, Init.Password);
        }

        protected HttpClient Client { get; set; }
        protected ILog Logger { get; private set; }
        protected bool IsLoggingEnabled { get; private set; }
        protected ClientInitParamsImpl Init { get; private set; }

        protected HttpClient GetClient(string hostUrl, string username, string password)
        {
            var client = new HttpClient(new HttpClientHandler
                                            {
                                                Credentials = new NetworkCredential(Init.Username, Init.Password)
                                            }) {BaseAddress = new Uri(string.Format("{0}/", Init.HostUrl))};
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

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

        protected virtual Task<HttpResponseMessage> Get(string url, CancellationToken cancellationToken =
                                                                        default(CancellationToken))
        {
            try
            {
                if (url.Contains("/%2f"))
                    LeaveDotsAndSlashesEscaped();

                return cancellationToken == default(CancellationToken)
                           ? Client.GetAsync(url)
                           : Client.GetAsync(url, cancellationToken);
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

                return cancellationToken == default(CancellationToken)
                           ? Client.DeleteAsync(url)
                           : Client.DeleteAsync(url, cancellationToken);
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

                return cancellationToken == default(CancellationToken)
                           ? Client.PutAsJsonAsync(url, value)
                           : Client.PutAsJsonAsync(url, value, cancellationToken);
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

                return cancellationToken == default(CancellationToken)
                           ? Client.PostAsJsonAsync(url, value)
                           : Client.PostAsJsonAsync(url, value, cancellationToken);
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }

        protected virtual void LogError(Exception e)
        {
            if (IsLoggingEnabled)
                Logger.Error(x => x("[Msg]: {0}, [Stack Trace] {1}", e.Message, e.StackTrace));
        }

        protected virtual void LogError(string message)
        {
            if (IsLoggingEnabled)
                Logger.Error(message);
        }

        protected virtual void LogInfo(string message)
        {
            if (IsLoggingEnabled)
                Logger.Info(message);
        }
    }
}