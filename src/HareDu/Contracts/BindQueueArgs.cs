namespace HareDu.Contracts
{
    using System.Collections.Generic;

    public interface BindQueueArgs
    {
        void UsingRoutingKey(string routingKey);
        void UsingArguments(List<string> args);
    }
}