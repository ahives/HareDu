namespace HareDu
{
    using System;
    using Newtonsoft.Json.Linq;

    public static class JsonValueExtensions
    {
        public static T GetValue<T>(this JToken token)
        {
            return token.IsNull() ? default(T) : token.Value<T>();
        }

        public static bool IsNull(this JToken token)
        {
            return (token == null);
        }
    }
}