using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChatClient
{
    public class Program
    {
        // TCPClient example https://docs.microsoft.com/ru-ru/dotnet/api/system.net.sockets.tcpclient?view=netframework-4.7.2
        public static void MyChatClient(String server, Int32 port, String message)
        {
            try
            {
                TcpClient client = new TcpClient(server, port);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", message);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);

                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
                throw e;
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
                throw e;
            }

            //Console.WriteLine("\n Press Enter to continue...");
            //Console.Read();
        }

        static void Main(string[] args)
        {
            MyChatClient("localhost", 8081, "Hi chat!  I'm a new user!");
        }
    }
}
