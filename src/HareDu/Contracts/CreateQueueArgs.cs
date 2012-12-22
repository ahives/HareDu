namespace HareDu.Contracts
{
    using System.Collections.Generic;

    public interface CreateQueueArgs
    {
        void IsDurable();
        void AutoDeleteWhenNotInUse();
        void UsingArguments(List<string> args);
    }
}