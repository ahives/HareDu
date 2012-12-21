namespace HareDu.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class VirtualHostTests
    {
        [Test]
        public void Verify_Create_Virtual_Host_Is_Working()
        {
            var client = new HareDuClient("http://localhost", 55672, "guest", "guest");
            var requestTask = client.CreateVirtualHost("test1");
            var responseTask = requestTask.ContinueWith(x =>
                                                            {
                                                                var response = x.Result;

                                                            });
            responseTask.Wait();
        }

    }
}