﻿using System;
using System.Collections.Generic;

namespace HareDu.TestHarness
{
    using Model;

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
            int port = 55672;

            outputAllVirtualHosts(url, port, username, password);
            outputAllOpenChannels(url, port, username, password);
        }

        private static void outputAllVirtualHosts(string url, int port, string username, string password)
        {
            Console.WriteLine("************ VIRTUAL HOSTS *************");
            var client = new HareDuClient(url, port, username, password);
            var requestTask = client.GetListOfVirtualHosts();
            var responseTask = requestTask.ContinueWith(x =>
            {
                var response = x.Result;

                foreach (
                    var virtualHost in response.GetResponse<IEnumerable<VirtualHost>>())
                {
                    Console.WriteLine(virtualHost.Name);
                    Console.WriteLine(virtualHost.Tracing);
                }
            });
            responseTask.Wait();
        }

        private static void outputAllOpenChannels(string url, int port, string username, string password)
        {
            Console.WriteLine("************ Open Channels *************");
            var client = new HareDuClient(url, port, username, password);
            var requestTask = client.GetListOfAllOpenChannels();
            var responseTask = requestTask.ContinueWith(x =>
                                                            {
                                                                var response = x.Result;

                                                                foreach (
                                                                    var channel in response.GetResponse<IEnumerable<Channel>>())
                                                                {
                                                                    Console.WriteLine(channel.Name);
                                                                    Console.WriteLine(channel.Node);
                                                                }
                                                            });
            responseTask.Wait();
            //Task.Factory.StartNew(() => requestTask);
            //Task.Factory.StartNew(() => responseTask);
            //.Result.Content.ReadAsAsync<IEnumerable<Channel>>().Result;

            //foreach (var channelInfo in channels)
            //{
            //    Console.WriteLine(channelInfo.MessageStats.Published);
            //    Console.WriteLine(channelInfo.MessageStats.Acknowledged);
            //    Console.WriteLine(channelInfo.MessageStats.Delivered);
            //    Console.WriteLine(channelInfo.MessageStats.DeliveredOrGet);
            //    Console.WriteLine(channelInfo.MessageStats.Unacknowledged);
            //    Console.WriteLine(channelInfo.MessageStats.Unconfirmed);
            //    Console.WriteLine(channelInfo.MessageStats.Uncommitted);
            //    Console.WriteLine(channelInfo.MessageStats.AcknowledgesUncommitted);
            //}
        }
    }
}
