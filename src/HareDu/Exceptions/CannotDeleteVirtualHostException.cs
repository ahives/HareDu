namespace HareDu
{
    using System;

    public class CannotDeleteVirtualHostException :
        Exception
    {
        public CannotDeleteVirtualHostException(string message) :
            base(message)
        {
        }
    }
}