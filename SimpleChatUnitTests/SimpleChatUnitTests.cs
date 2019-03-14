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
        public void Test_Connecting_To_Bad_Server()
        {
            Program p = new Program();
            Program.MyChatClient("localhost_doesnot_exist",65535);
        }
    }
}
