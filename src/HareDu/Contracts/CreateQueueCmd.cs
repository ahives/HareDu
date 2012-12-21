namespace HareDu.Contracts
{
    using System.Collections.Generic;

    public interface CreateQueueCmd
    {
        void IsDurable();
        void AutoDeleteWhenNotInUse();
        void UsingArguments(List<string> args);
    }
}