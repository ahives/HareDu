// Copyright 2013-2014 Albert L. Hives, Chris Patterson, et al.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace HareDu.Tests
{
    using System;
    using NUnit.Framework;
    using Resources;

    [TestFixture]
    public class ConnectionTests :
        HareDuTestBase
    {
        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Return_All_Connections()
        {
            var data = Client
                                .Factory<ConnectionResources>(
                    x => x.Credentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                //.Connection
                             .GetAll()
                             .Data();

            foreach (var connection in data)
            {
                Console.WriteLine("Name: {0}", connection.Name);
                Console.WriteLine("Node: {0}", connection.Node);
                Console.WriteLine("Port: {0}", connection.Port);
                Console.WriteLine("Protocol: {0}", connection.Protocol);
                Console.WriteLine("Peer Address: {0}", connection.PeerAddress);
                Console.WriteLine("Peer Port: {0}", connection.PeerPort);
                Console.WriteLine("Peer Certificate Issuer: {0}", connection.PeerCertificateIssuer);
                Console.WriteLine("Address: {0}", connection.Address);
                Console.WriteLine("Authentication Mechanism Used: {0}", connection.AuthenticationMechanismUsed);
                Console.WriteLine("ChannelsOpen: {0}", connection.ChannelsOpen);
                Console.WriteLine("Timeout: {0}", connection.Timeout);
                Console.WriteLine("Type: {0}", connection.Type);
                Console.WriteLine("User: {0}", connection.User);
                Console.WriteLine("Virtual Host: {0}", connection.VirtualHost);
                Console.WriteLine("State: {0}", connection.State);
                Console.WriteLine("SSL Cipher: {0}", connection.SslCipher);
                Console.WriteLine("SSL Hash: {0}", connection.SslHash);
                Console.WriteLine("SSL Key Exchange: {0}", connection.SslKeyExchange);
                Console.WriteLine("SSL Protocol: {0}", connection.SslProtocol);
                Console.WriteLine("****************************************************");
                Console.WriteLine();
            }
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Return_Connection()
        {
            var data = Client
                                .Factory<ConnectionResources>(
                    x => x.Credentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                //.Connection
                             .Get(Settings.Default.Connection)
                             .Data();

            Console.WriteLine("Name: {0}", data.Name);
            Console.WriteLine("Node: {0}", data.Node);
            Console.WriteLine("Port: {0}", data.Port);
            Console.WriteLine("Protocol: {0}", data.Protocol);
            Console.WriteLine("Peer Address: {0}", data.PeerAddress);
            Console.WriteLine("Peer Port: {0}", data.PeerPort);
            Console.WriteLine("Peer Certificate Issuer: {0}", data.PeerCertificateIssuer);
            Console.WriteLine("Address: {0}", data.Address);
            Console.WriteLine("Authentication Mechanism Used: {0}", data.AuthenticationMechanismUsed);
            Console.WriteLine("ChannelsOpen: {0}", data.ChannelsOpen);
            Console.WriteLine("Timeout: {0}", data.Timeout);
            Console.WriteLine("Type: {0}", data.Type);
            Console.WriteLine("User: {0}", data.User);
            Console.WriteLine("Virtual Host: {0}", data.VirtualHost);
            Console.WriteLine("State: {0}", data.State);
            Console.WriteLine("SSL Cipher: {0}", data.SslCipher);
            Console.WriteLine("SSL Hash: {0}", data.SslHash);
            Console.WriteLine("SSL Key Exchange: {0}", data.SslKeyExchange);
            Console.WriteLine("SSL Protocol: {0}", data.SslProtocol);
            Console.WriteLine("****************************************************");
            Console.WriteLine();
        }
    }
}