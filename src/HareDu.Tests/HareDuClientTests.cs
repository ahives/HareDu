using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HareDu.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class HareDuClientTests
    {
        [Test]
        public void Verify_GetInfoOnOpenChannels_Working()
        {
            var client = new HareDuClient("http://localhost", 55672, "guest", "guest");
            var channels = client.GetInfoOnOpenChannels();

            foreach (var channelInfo in channels)
            {
                Console.WriteLine(channelInfo.MessageStats.Published);
                Console.WriteLine(channelInfo.MessageStats.Acknowledged);
                //Console.WriteLine(channelInfo.MessageStats.Delivered);
                //Console.WriteLine(channelInfo.MessageStats.DeliveredOrGet);
                //Console.WriteLine(channelInfo.MessageStats.Unacknowledged);
                //Console.WriteLine(channelInfo.MessageStats.Unconfirmed);
                //Console.WriteLine(channelInfo.MessageStats.Uncommitted);
                //Console.WriteLine(channelInfo.MessageStats.AcknowledgesUncommitted);
            }
        }

    }
}
