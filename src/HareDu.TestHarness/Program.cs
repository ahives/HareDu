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

            outputVirtualHostInfo(new HareDuClientParameters(url, port, username, password));
            outputOpenChannelInfo(new HareDuClientParameters(url, port, username, password));
        }

        public static void outputVirtualHostInfo(HareDuClientParameters hareDuClientParameters)
        {
            Console.WriteLine("************ VIRTUAL HOSTS *************");
            var client = new HareDuClient(hareDuClientParameters.Url, hareDuClientParameters.Port, hareDuClientParameters.Username, hareDuClientParameters.Password);
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

        public static void outputOpenChannelInfo(HareDuClientParameters hareDuClientParameters)
        {
            Console.WriteLine("************ Open Channels *************");
            var client = new HareDuClient(hareDuClientParameters.Url, hareDuClientParameters.Port, hareDuClientParameters.Username, hareDuClientParameters.Password);
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
