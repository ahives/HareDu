using System;
using System.Collections.Generic;
using System.Net.Http;
using HareDu.Model;

namespace HareDu.TestHarness
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("HareDu Test Harness");
            string url;
            string username;
            string password;
            string hostToDelete;
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

                Console.Write("Host To Delete: ");
                hostToDelete = Console.ReadLine();
            }
            else
            {
                url = "http://localhost";
                username = "guest";
                password = "guest";
            }

            var hareDuClientParameters = new HareDuClientParameters(url, port, username, password);

            outputWhoAmIInfo(hareDuClientParameters);

            outputVirtualHostInfo(hareDuClientParameters);

            outputOpenChannelInfo(hareDuClientParameters);

            deleteVirtualHost(hareDuClientParameters, hostToDelete);
        }

        private static void outputWhoAmIInfo(HareDuClientParameters hareDuClientParameters)
        {
            Console.WriteLine("************ WhoAmI *************");
            var client = CreateHareDuClient(hareDuClientParameters);
            var myrequestTask = client.WhoAmI();
            var responseTask = myrequestTask.ContinueWith((requestTask) =>
                            {
                                HttpResponseMessage response = requestTask.Result;
                                response.EnsureSuccessStatusCode();

                                var r = response.GetResponse<WhoAmI>();
                                Console.WriteLine("Name:" + r.Name);
                                Console.WriteLine("Tags:" + r.Tags);
                                Console.WriteLine("AuthBackend:" + r.AuthBackend);
                            });
            responseTask.Wait();
        }

        private static HareDuClient CreateHareDuClient(HareDuClientParameters hareDuClientParameters)
        {
            var client = new HareDuClient(hareDuClientParameters.Url, hareDuClientParameters.Port,
                                          hareDuClientParameters.Username, hareDuClientParameters.Password);
            return client;
        }

        private static void deleteVirtualHost(HareDuClientParameters hareDuClientParameters, string hostToDelete)
        {
            if (string.IsNullOrWhiteSpace(hostToDelete))
            {
                Console.WriteLine("No virtual host to delete specified - deleting skipped");
                return;
            }
            var client = CreateHareDuClient(hareDuClientParameters);
            var requestTask = client.DeleteVirtualHost(hostToDelete);
            var responseTask = requestTask.ContinueWith(x =>
                                                            {
                                                                HttpResponseMessage response = x.Result;
                                                                Console.WriteLine("response.IsSuccessStatusCode" +
                                                                                  response.IsSuccessStatusCode);
                                                            });
            responseTask.Wait();
        }

        public static void outputVirtualHostInfo(HareDuClientParameters hareDuClientParameters)
        {
            Console.WriteLine("************ VIRTUAL HOSTS *************");
            var client = CreateHareDuClient(hareDuClientParameters);
            var requestTask = client.GetListOfVirtualHosts();
            var responseTask = requestTask.ContinueWith(x =>
                                                            {
                                                                var response = x.Result;

                                                                foreach (
                                                                    var virtualHost in
                                                                        response.GetResponse<IEnumerable<VirtualHost>>()
                                                                    )
                                                                {
                                                                    Console.WriteLine("-------------------");
                                                                    Console.WriteLine("START");
                                                                    Console.WriteLine("Virtual Host Name:" +
                                                                                      virtualHost.Name);
                                                                    Console.WriteLine("Virtual Host tracingflag: " +
                                                                                      virtualHost.Tracing);
                                                                    Console.WriteLine("END");
                                                                }
                                                            });
            responseTask.Wait();
        }

        public static void outputOpenChannelInfo(HareDuClientParameters hareDuClientParameters)
        {
            Console.WriteLine("************ Open Channels *************");
            var client = CreateHareDuClient(hareDuClientParameters);
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
