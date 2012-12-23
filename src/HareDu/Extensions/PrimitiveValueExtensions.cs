namespace HareDu
{
    using System;

    public static class PrimitiveValueExtensions
    {
        public static string SanitizeVirtualHostName(this string value)
        {
            if (value == @"/")
            {
                return value.Replace("/", "%2f");
            }

            return value;
        }

        public static bool IsNull<T>(this T value)
            where T : class
        {
            return (value == null);
        }

        public static void CheckIfArgValid(this string value, string paramName)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(paramName);
        }

        public static void CheckIfArgValid<T>(this T value, string paramName)
            where T : class
        {
            if (value.IsNull())
                throw new ArgumentNullException(paramName);
        }
    }
}