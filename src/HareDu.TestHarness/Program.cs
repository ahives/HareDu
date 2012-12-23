using System;
using System.Collections.Generic;
using HareDu.Model;

namespace HareDu.TestHarness
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string url;
            string username;
            string password;
            const int port = 55672;
            const bool promptForParameters = true;
            if (promptForParameters)
            {
                Console.Write("URL: ");
                url = Console.ReadLine();

                Console.Write("Username: ");
                username = Console.ReadLine();

                Console.Write("Password: ");
                password = Console.ReadLine();
            }
            else
            {
                url = "http://localhost";
                username = "guest";
                password = "guest";
            }

            outputVirtualHostInfo(url, port, username, password);
            outputOpenChannelInfo(url, port, username, password);
        }

        public static void outputVirtualHostInfo(string url, int port, string username, string password)
        {
            Console.WriteLine("************ VIRTUAL HOSTS *************");
            var client = new HareDuClient(url, port, username, password);
            var requestTask = client.GetListOfVirtualHosts();
            var responseTask = requestTask.ContinueWith(x =>
                                                            {
                                                                var response = x.Result;

                                                                foreach (
                                                                    var virtualHost in
                                                                        response.GetResponse<IEnumerable<VirtualHost>>()
                                                                    )
                                                                {
                                                                    Console.WriteLine(virtualHost.Name);
                                                                    Console.WriteLine(virtualHost.Tracing);
                                                                }
                                                            });
            responseTask.Wait();
        }

        public static void outputOpenChannelInfo(string url, int port, string username, string password)
        {
            Console.WriteLine("************ Open Channels *************");
            var client = new HareDuClient(url, port, username, password);
            var requestTask = client.GetListOfAllOpenChannels();
            var responseTask = requestTask.ContinueWith(x =>
                                                            {
                                                                var response = x.Result;

                                                                foreach (
                                                                    var channel in
                                                                        response.GetResponse<IEnumerable<Channel>>())
                                                                {
                                                                    Console.WriteLine(channel.Name);
                                                                    Console.WriteLine(channel.Node);
                                                                }
                                                            });
            responseTask.Wait();
        }
    }
}
