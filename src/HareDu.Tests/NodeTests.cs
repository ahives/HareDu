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
    public class NodeTests :
        HareDuTestBase
    {
        [Test]
        public void Verify_Can_Return_All_Nodes_On_Cluster()
        {
            var request = Client.Node.GetAll();

            foreach (var node in request.Result)
            {
                Console.WriteLine("Name: {0}", node.Name);
                Console.WriteLine("Type: {0}", node.Type);
                Console.WriteLine("Running: {0}", node.Running);
                Console.WriteLine("Operating System PID: {0}", node.OperatingSystemPID);
                Console.WriteLine("Memory Ets: {0}", node.MemoryEts);
                Console.WriteLine("Memory Binary: {0}", node.MemoryBinary);
                Console.WriteLine("Memory Proc: {0}", node.MemoryProc);
                Console.WriteLine("Memory Proc Used: {0}", node.MemoryProcUsed);
                Console.WriteLine("Memory Code: {0}", node.MemoryCode);
                Console.WriteLine("Fd Used: {0}", node.FdUsed);
                Console.WriteLine("Fd Total: {0}", node.FdTotal);
                Console.WriteLine("Sockets Used: {0}", node.SocketsUsed);
                Console.WriteLine("Sockets Total: {0}", node.SocketsTotal);
                Console.WriteLine("Memory Used: {0}", node.MemoryUsed);
                Console.WriteLine("Memory Limit: {0}", node.MemoryLimit);
                Console.WriteLine("Memory Alarm: {0}", node.MemoryAlarm);
                Console.WriteLine("Disk Free Limit: {0}", node.DiskFreeLimit);
                Console.WriteLine("Disk Free: {0}", node.DiskFree);
                Console.WriteLine("Disk Free Alarm: {0}", node.DiskFreeAlarm);
                Console.WriteLine("Proc Used: {0}", node.ProcUsed);
                Console.WriteLine("Proc Total: {0}", node.ProcTotal);
                Console.WriteLine("Statistics Level: {0}", node.StatisticsLevel);
                Console.WriteLine("Erlang Version: {0}", node.ErlangVersion);
                Console.WriteLine("Uptime: {0}", node.Uptime);
                Console.WriteLine("Run Queue: {0}", node.RunQueue);
                Console.WriteLine("Processors: {0}", node.Processors);

                Console.WriteLine("******************** Exchange Types ********************");
                foreach (var exchangeType in node.ExchangeTypes)
                {
                    Console.WriteLine("Name: {0}", exchangeType.Name);
                    Console.WriteLine("Description: {0}", exchangeType.Description);
                    Console.WriteLine("Enabled: {0}", exchangeType.Enabled);
                }

                Console.WriteLine("******************** Authentication Mechanisms ********************");
                foreach (var authenticationMechanism in node.AuthenticationMechanisms)
                {
                    Console.WriteLine("Name: {0}", authenticationMechanism.Name);
                    Console.WriteLine("Description: {0}", authenticationMechanism.Description);
                    Console.WriteLine("Enabled: {0}", authenticationMechanism.Enabled);
                }

                Console.WriteLine("******************** Applications ********************");
                foreach (var application in node.Applications)
                {
                    Console.WriteLine("Name: {0}", application.Name);
                    Console.WriteLine("Description: {0}", application.Description);
                    Console.WriteLine("Version: {0}", application.Version);
                }

                Console.WriteLine("****************************************************");
                Console.WriteLine();
            }
        }

        [Test]
        public void Verify_Can_Return_Node_On_Cluster()
        {
            var node =
                Client.Node.Get(string.Format("rabbit@{0}", Environment.GetEnvironmentVariable("COMPUTERNAME"))).Result;

            Console.WriteLine("Name: {0}", node.Name);
            Console.WriteLine("Type: {0}", node.Type);
            Console.WriteLine("Running: {0}", node.Running);
            Console.WriteLine("Operating System PID: {0}", node.OperatingSystemPID);
            Console.WriteLine("Memory Ets: {0}", node.MemoryEts);
            Console.WriteLine("Memory Binary: {0}", node.MemoryBinary);
            Console.WriteLine("Memory Proc: {0}", node.MemoryProc);
            Console.WriteLine("Memory Proc Used: {0}", node.MemoryProcUsed);
            Console.WriteLine("Memory Code: {0}", node.MemoryCode);
            Console.WriteLine("Fd Used: {0}", node.FdUsed);
            Console.WriteLine("Fd Total: {0}", node.FdTotal);
            Console.WriteLine("Sockets Used: {0}", node.SocketsUsed);
            Console.WriteLine("Sockets Total: {0}", node.SocketsTotal);
            Console.WriteLine("Memory Used: {0}", node.MemoryUsed);
            Console.WriteLine("Memory Limit: {0}", node.MemoryLimit);
            Console.WriteLine("Memory Alarm: {0}", node.MemoryAlarm);
            Console.WriteLine("Disk Free Limit: {0}", node.DiskFreeLimit);
            Console.WriteLine("Disk Free: {0}", node.DiskFree);
            Console.WriteLine("Disk Free Alarm: {0}", node.DiskFreeAlarm);
            Console.WriteLine("Proc Used: {0}", node.ProcUsed);
            Console.WriteLine("Proc Total: {0}", node.ProcTotal);
            Console.WriteLine("Statistics Level: {0}", node.StatisticsLevel);
            Console.WriteLine("Erlang Version: {0}", node.ErlangVersion);
            Console.WriteLine("Uptime: {0}", node.Uptime);
            Console.WriteLine("Run Queue: {0}", node.RunQueue);
            Console.WriteLine("Processors: {0}", node.Processors);

            Console.WriteLine("******************** Exchange Types ********************");
            foreach (var exchangeType in node.ExchangeTypes)
            {
                Console.WriteLine("Name: {0}", exchangeType.Name);
                Console.WriteLine("Description: {0}", exchangeType.Description);
                Console.WriteLine("Enabled: {0}", exchangeType.Enabled);
            }

            Console.WriteLine("******************** Authentication Mechanisms ********************");
            foreach (var authenticationMechanism in node.AuthenticationMechanisms)
            {
                Console.WriteLine("Name: {0}", authenticationMechanism.Name);
                Console.WriteLine("Description: {0}", authenticationMechanism.Description);
                Console.WriteLine("Enabled: {0}", authenticationMechanism.Enabled);
            }

            Console.WriteLine("******************** Applications ********************");
            foreach (var application in node.Applications)
            {
                Console.WriteLine("Name: {0}", application.Name);
                Console.WriteLine("Description: {0}", application.Description);
                Console.WriteLine("Version: {0}", application.Version);
            }
        }
    }
}