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