// Copyright 2012-2013 Albert L. Hives, Chris Patterson, et al.
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

    [TestFixture]
    public class ConnectionTests :
        HareDuTestBase
    {
        [Test, Category("Integration")]
        public void Verify_Can_Return_All_Connections()
        {
            var response = Client.Connection.GetAll();

            foreach (var connection in response.Result)
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
                Console.WriteLine("Channels: {0}", connection.Channels);
                Console.WriteLine("Timeout: {0}", connection.Timeout);
                Console.WriteLine("Type: {0}", connection.Type);
                Console.WriteLine("User: {0}", connection.User);
                Console.WriteLine("Virtual Host: {0}", connection.VirtualHostName);
                Console.WriteLine("State: {0}", connection.State);
                Console.WriteLine("SSL Cipher: {0}", connection.SslCipher);
                Console.WriteLine("SSL Hash: {0}", connection.SslHash);
                Console.WriteLine("SSL Key Exchange: {0}", connection.SslKeyExchange);
                Console.WriteLine("SSL Protocol: {0}", connection.SslProtocol);
                Console.WriteLine("****************************************************");
                Console.WriteLine();
            }
        }

        [Test, Category("Integration")]
        public void Verify_Can_Return_Connection()
        {
            var connection = Client.Connection.Get(Settings.Default.Connection).Result;

            Console.WriteLine("Name: {0}", connection.Name);
            Console.WriteLine("Node: {0}", connection.Node);
            Console.WriteLine("Port: {0}", connection.Port);
            Console.WriteLine("Protocol: {0}", connection.Protocol);
            Console.WriteLine("Peer Address: {0}", connection.PeerAddress);
            Console.WriteLine("Peer Port: {0}", connection.PeerPort);
            Console.WriteLine("Peer Certificate Issuer: {0}", connection.PeerCertificateIssuer);
            Console.WriteLine("Address: {0}", connection.Address);
            Console.WriteLine("Authentication Mechanism Used: {0}", connection.AuthenticationMechanismUsed);
            Console.WriteLine("Channels: {0}", connection.Channels);
            Console.WriteLine("Timeout: {0}", connection.Timeout);
            Console.WriteLine("Type: {0}", connection.Type);
            Console.WriteLine("User: {0}", connection.User);
            Console.WriteLine("Virtual Host: {0}", connection.VirtualHostName);
            Console.WriteLine("State: {0}", connection.State);
            Console.WriteLine("SSL Cipher: {0}", connection.SslCipher);
            Console.WriteLine("SSL Hash: {0}", connection.SslHash);
            Console.WriteLine("SSL Key Exchange: {0}", connection.SslKeyExchange);
            Console.WriteLine("SSL Protocol: {0}", connection.SslProtocol);
            Console.WriteLine("****************************************************");
            Console.WriteLine();
        }
    }
}