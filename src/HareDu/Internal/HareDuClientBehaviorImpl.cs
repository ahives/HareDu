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
    using Internal;

    internal class HareDuClientBehaviorImpl :
        HareDuClientBehavior
    {
        public string HostUrl { get; private set; }
        public ILog Logger { get; private set; }
        public TimeSpan Timeout { get; private set; }

        public void ConnectTo(string hostUrl)
        {
            HostUrl = hostUrl;
        }

        public void EnableLogging(Action<LoggingCharacteristics> logger)
        {
            if (Logger.IsNull())
            {
                var loggingCharacteristicsImpl = new LoggingCharacteristicsImpl();
                logger(loggingCharacteristicsImpl);
                Logger = LogManager.GetLogger(loggingCharacteristicsImpl.Target);
            }
        }

        public void TimeoutAfter(TimeSpan timeout)
        {
            Timeout = timeout;
        }
    }
}