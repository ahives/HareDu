namespace HareDu
{
    using System;

    public class HareDuClientInitException :
        Exception
    {
        public HareDuClientInitException(string message, Exception innerException) :
            base(message, innerException)
        {
        }
    }
}