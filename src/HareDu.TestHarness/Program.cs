using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HareDu.TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("URL: ");
            string url = Console.ReadLine();
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            url = "http://localhost";
            var client = new HareDuClient(url, 55672, username, password);
            var channels = client.GetInfoOnOpenChannels();

            foreach (var channelInfo in channels)
            {
                Console.WriteLine(channelInfo.MessageStats.Published);
                //Console.WriteLine(channelInfo.MessageStats.Acknowledged);
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
