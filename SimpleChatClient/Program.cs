using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleChatClient
{
    public class Program
    {
        public interface IChatClient
        {
            void connect();
            void disconnect();
            void sendMessage(string message);
        }
        public class MyChatClient : IChatClient {
            bool threadStart = false;
            Thread responeThread;
            public TcpClient tcpClient { get; set; }
            public NetworkStream stream { get; set; }
            public String server { get; set;  }
            public Int32 port { get; set;  }
            public MyChatClient() { }
            public MyChatClient(String server, Int32 port) {
                this.server = server;
                this.port = port;
                tcpClient = new TcpClient();
            }
            private void listen() {
                responeThread = new Thread(() => {
                    threadStart = true;
                    while (threadStart) {
                        Byte[] data = new Byte[256];
                        String responseData = String.Empty;
                        if (stream.DataAvailable) {
                            Int32 bytes = stream.Read(data, 0, data.Length);
                            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                            Console.WriteLine("Received: {0}", responseData);
                        }
                        else {
                            Thread.Sleep(100);
                        }
                    }
                });
                responeThread.Start();
            }
            public void connect() {
                tcpClient.Connect(server, port);
                stream = tcpClient.GetStream();
                listen();
            }
            public void sendMessage(string message) {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
            public void disconnect() {
                threadStart = false;
                responeThread.Join();
                stream.Close();
                tcpClient.Close();
            }
            ~MyChatClient() {
                disconnect();
            }
        }

        static void Main(string[] args)
        {
            MyChatClient client = new MyChatClient("localhost", 8181);
            client.connect();
            String message = String.Empty;
            //while (message != "bye!") {
            //    message = Console.ReadLine();
            //    client.sendMessage(message);
            //}
            client.disconnect();
            Console.WriteLine("\nPress Enter to exit...");
            Console.Read();
        }
    }
}
