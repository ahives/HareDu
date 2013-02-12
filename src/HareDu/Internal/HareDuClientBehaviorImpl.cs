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
    using Common.Logging;
    using Contracts;

    public class HareDuClientBehaviorImpl :
        HareDuClientBehavior
    {
        public string HostUrl { get; private set; }

        // TODO: DELETE ME
        public string Username { get; private set; }

        // TODO: DELETE ME
        public string Password { get; private set; }

        // TODO: DELETE ME
        public string VirtualHost { get; private set; }

        public ILog Logger { get; private set; }

        public TimeSpan Timeout { get; private set; }

        //public void ConnectTo(string hostUrl, string virtualHost = "/")
        //{
        //    HostUrl = hostUrl;
        //    VirtualHost = virtualHost;
        //}

        //public void UsingCredentials(string username, string password)
        //{
        //    Username = username;
        //    Password = password;
        //}

        public void ConnectTo(string hostUrl)
        {
            HostUrl = hostUrl;
        }

        public void EnableLogging(string loggerName)
        {
            if (Logger.IsNull())
            {
                Logger = LogManager.GetLogger(loggerName);
            }
        }

        //public void OnVirtualHost(string virtualHost)
        //{
        //    VirtualHost = virtualHost;
        //}

        public void TimeoutAfter(TimeSpan timeout)
        {
            Timeout = timeout;
        }
    }
}