using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using StudentHome.Api.Net;

namespace StudentHome.Client.Net
{
    public class TcpClient
    {
        private readonly EndPoint endPoint;

        internal TcpClient()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            endPoint = new IPEndPoint(ipAddress, 11000);
        }
        internal Message Execute(Message request)
        {
            try
            {
                Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(endPoint);
                Trace.WriteLine("Socket connected");

                byte[] bytesRequest = request.SerializeToByteArray();
                socket.Send(bytesRequest);
                Trace.WriteLine("Request sent");

                byte[] bytes = new byte[10000];
                int bytesRec = socket.Receive(bytes);
                Message response = bytes.Deserialize<Message>();
                Trace.WriteLine("Response received");

                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                return response;
            }
            catch (SocketException)
            {
                Console.WriteLine("Socket exception");
                throw;
            }
        }
    }
}
