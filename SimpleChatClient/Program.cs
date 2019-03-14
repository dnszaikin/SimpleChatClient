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
        static bool threadStart = false;
        public static void MyChatClient(String server, Int32 port)
        {
            try {
                TcpClient client = new TcpClient(server, port);
                String message = String.Empty;
                NetworkStream stream = client.GetStream();         
                Thread responeThread = new Thread(() => {
                    threadStart = true;
                    while (threadStart) {
                        Byte[] data = new Byte[256];
                        String responseData = String.Empty;
                        if (stream.DataAvailable) {
                            Int32 bytes = stream.Read(data, 0, data.Length);
                            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                            Console.WriteLine("Received: {0}", responseData);
                        } else {
                            Thread.Sleep(100);
                        }                        
                    }
                });
                responeThread.Start();
                while (message != "bye!") {
                    message = Console.ReadLine();
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                }
                threadStart = false;
                responeThread.Join();
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e) {
                Console.WriteLine("ArgumentNullException: {0}", e);
                throw e;
            }
            catch (SocketException e) {
                Console.WriteLine("SocketException: {0}", e);
                throw e;
            }
        }

        static void Main(string[] args)
        {
            MyChatClient("10.44.1.234", 8181);
            Console.WriteLine("\nPress Enter to exit...");
            Console.Read();
        }
    }
}
