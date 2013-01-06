// Copyright 2012-2013 Albert L. Hives, Chris Patterson, Rajesh Gande, et al.
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
    using System.Collections.Generic;
    using Model;
    using NUnit.Framework;

    [TestFixture]
    public class ConnectivityTests :
        HareDuTestBase
    {
        [Test]
        public void Verify_Can_Return_All_Channels()
        {
            var response = Client.GetAllChannels();

            foreach (var channel in response.Result)
            {
                Console.WriteLine("Name: {0}", channel.Name);
                Console.WriteLine("Node: {0}", channel.Node);
                Console.WriteLine("User: {0}", channel.User);
                Console.WriteLine("Confirm: {0}", channel.Confirm);
                Console.WriteLine("Client Flow Blocked: {0}", channel.IsClientFlowBlocked);
                Console.WriteLine("Is Transactional: {0}", channel.IsTransactional);
                Console.WriteLine("Idle Since: {0}", channel.IdleSince.ToString());
                Console.WriteLine("Virtual Host: {0}", channel.VirtualHostName);
                Console.WriteLine("Prefetch Count: {0}", channel.PrefetchCount);
                Console.WriteLine("Unacknowledged: {0}", channel.Unacknowledged);
                Console.WriteLine("Uncommitted: {0}", channel.Uncommitted);
                Console.WriteLine("Unconfirmed: {0}", channel.Unconfirmed);
                Console.WriteLine("Acknowledges Uncommitted: {0}", channel.AcknowledgesUncommitted);
                Console.WriteLine("Consumer Count: {0}", channel.ConsumerCount);

                if (!channel.MessageStats.IsNull())
                {
                    Console.WriteLine("Acknowledged: {0}", channel.MessageStats.Acknowledged);
                    Console.WriteLine("Published: {0}", channel.MessageStats.Published);
                    Console.WriteLine("Delivered: {0}", channel.MessageStats.Delivered);
                    Console.WriteLine("Delivered/Get: {0}", channel.MessageStats.DeliveredOrGet);
                    Console.WriteLine("Acknowledged: {0}", channel.MessageStats.Acknowledged);
                }

                Console.WriteLine("****************************************************");
                Console.WriteLine();
            }
        }

        [Test]
        public void Verify_Can_Return_All_Connections()
        {
            var response = Client.GetAllConnections();

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

        [Test]
        public void Verify_Can_Return_Channel()
        {
            var request = Client.GetChannel(Settings.Default.Channel);
            //Assert.AreEqual(true, request.Result.IsSuccessStatusCode);
        }

        [Test]
        public void Verify_Can_Return_Connection()
        {
            var connection = Client.GetConnection(Settings.Default.Connection).Result;

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

        [Test]
        public void Verify_RabbitMQ_Is_Alive()
        {
            var request = Client.IsAlive(Settings.Default.VirtualHost);
            Assert.AreEqual(true, request.Result.Status);
        }
    }
}