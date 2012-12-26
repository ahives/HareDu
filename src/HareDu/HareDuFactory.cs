namespace HareDu
{
    using System;
    using Contracts;

    public static class HareDuFactory
    {
         public static HareDuClient New(Action<HareDuInitArgs> args)
         {
             try
             {
                 var hareDuInitArgs = new HareDuInitArgsImpl();
                 args(hareDuInitArgs);
                 var client = new HareDuClientImpl(hareDuInitArgs);

                 return client;
             }
             catch (Exception e)
             {
                 throw new HareDuClientInitException("", e);
             }
         }
    }
}