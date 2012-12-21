namespace HareDu
{
    using System.Collections.Generic;
    using Contracts;
    using Newtonsoft.Json;

    public class BindQueueCmdImpl :
        BindQueueCmd
    {
        public BindQueueCmdImpl()
        {
            Arguments = new List<string>();
            RoutingKey = string.Empty;
        }

        [JsonProperty(PropertyName = "routing_key", Order = 1)]
        public string RoutingKey { get; set; }

        [JsonProperty(PropertyName = "arguments", Order = 2, Required = Required.Default)]
        public List<string> Arguments { get; set; }

        public void UsingRoutingKey(string routingKey)
        {
            RoutingKey = routingKey;
        }

        public void UsingArguments(List<string> args)
        {
            Arguments = args;
        }
    }
}