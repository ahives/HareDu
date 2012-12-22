namespace HareDu.Contracts
{
    using System.Collections.Generic;

    public interface CreateExchangeArgs
    {
        void IsDurable();
        void AutoDeleteWhenNotInUse();
        void IsForInternalUse();
        void UsingArguments(List<string> args);
        void RoutingType(string routingType);
    }
}