namespace HareDu.Contracts
{
    using System.Collections.Generic;

    public interface CreateExchangeCmd
    {
        void IsDurable();
        void AutoDeleteWhenNotInUse();
        void IsForInternalUse();
        void UsingArguments(List<string> args);
        void RoutingType(string routingType);
    }
}