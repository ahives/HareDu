namespace HareDu.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class HareDuTestBase
    {
        protected HareDuClient Client { get; set; }

        [SetUp]
        public void Setup()
        {
            Client = HareDuFactory.New(x =>
                                           {
                                               x.ConnectTo(Settings.Default.HostUrl);
                                               x.UsingCredentials(Settings.Default.Username, Settings.Default.Password);
                                           });
        }
    }
}