namespace HareDu.Model
{
    using System.Collections.Generic;
    using Contracts;
    using Newtonsoft.Json;

    public class CreateExchangeArgsImpl :
        CreateExchangeArgs
    {
        public CreateExchangeArgsImpl()
        {
            Arguments = new List<string>();
            Type = ExchangeRoutingType.Direct;
        }

        [JsonProperty(PropertyName = "type", Order = 1)]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "auto_delete", Order = 2)]
        public bool AutoDelete { get; set; }

        [JsonProperty(PropertyName = "durable", Order = 3)]
        public bool Durable { get; set; }

        [JsonProperty(PropertyName = "internal", Order = 4)]
        public bool Internal { get; set; }

        [JsonProperty(PropertyName = "arguments", Order = 5, Required = Required.Default)]
        public List<string> Arguments { get; set; }

        public void IsDurable()
        {
            Durable = true;
        }

        public void AutoDeleteWhenNotInUse()
        {
            AutoDelete = true;
        }

        public void IsForInternalUse()
        {
            Internal = true;
        }

        public void UsingArguments(List<string> args)
        {
            Arguments = args;
        }

        public void RoutingType(string routingType)
        {
            Type = routingType;
        }
    }
}