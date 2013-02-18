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

    public abstract class Logging
    {
        protected Logging(ILog logger)
        {
            Logger = logger;
            IsLoggingEnabled = !Logger.IsNull();
        }

        protected ILog Logger { get; private set; }
        protected bool IsLoggingEnabled { get; private set; }

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