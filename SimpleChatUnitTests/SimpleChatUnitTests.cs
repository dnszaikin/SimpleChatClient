using System;
using System.IO;
using System.Net.Sockets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleChatClient;
using static SimpleChatClient.Program;

namespace SimpleChatUnitTests
{
    class MyChatClientMock : IChatClient
    {
        public void connect() {}
        public void disconnect() { }
        public void sendMessage(string message) { }
    }


    [TestClass]
    public class BigUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(SocketException))]
        public void Test_Connecting_To_Bad_Server()
        {
            MyChatClient client = new MyChatClient("localhost_doesnot_exist", 65535);
            client.connect();
        }

        [TestMethod]
        public void Test_Sending_Message()
        {
            MyChatClientMock client = new MyChatClientMock();
            client.connect();
            client.sendMessage("FakeMessage");
            client.disconnect();
        }
    }
}
