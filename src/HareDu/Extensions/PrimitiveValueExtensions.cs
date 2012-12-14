namespace HareDu
{
    public static class PrimitiveValueExtensions
    {
        public static string SanitizeVirtualHostName(this string value)
        {
            if (value == @"/")
            {
                return value.Replace("/", "%2F");
            }

            return value;
        }
    }
}