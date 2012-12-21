namespace HareDu.Contracts
{
    using System.Collections.Generic;

    public interface BindQueueCmd
    {
        void UsingRoutingKey(string routingKey);
        void UsingArguments(List<string> args);
    }
}