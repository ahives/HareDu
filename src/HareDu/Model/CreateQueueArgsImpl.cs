namespace HareDu.Model
{
    using System.Collections.Generic;
    using Contracts;
    using Newtonsoft.Json;

    public class CreateQueueArgsImpl :
        CreateQueueArgs
    {
        public CreateQueueArgsImpl()
        {
            Arguments = new List<string>();
        }

        [JsonProperty(PropertyName = "durable", Order = 2)]
        public bool Durable { get; private set; }

        [JsonProperty(PropertyName = "auto_delete", Order = 1)]
        public bool AutoDelete { get; private set; }

        [JsonProperty(PropertyName = "arguments", Order = 3, Required = Required.Default)]
        public List<string> Arguments { get; set; }

        [JsonProperty(PropertyName = "node", Order = 4, Required = Required.Default)]
        public string Node { get; set; }

        public void IsDurable()
        {
            Durable = true;
        }

        public void AutoDeleteWhenNotInUse()
        {
            AutoDelete = true;
        }

        public void UsingArguments(List<string> args)
        {
            Arguments = args;
        }
    }
}