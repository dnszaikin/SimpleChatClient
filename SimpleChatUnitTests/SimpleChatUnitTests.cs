using System;
using System.Net.Sockets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleChatClient;

namespace SimpleChatUnitTests
{
    [TestClass]
    public class BigUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(SocketException))]
        public void TestConnectionNotValidEndpoint()
        {
            Program p = new Program();
            Program.MyChatClient("localhost",8080,"Test");
        }

        [TestMethod]
        public void TestConnectionValidEndpoint()
        {
            Program p = new Program();
            Program.MyChatClient("localhost", 8181, "Test");
        }

    }
}
