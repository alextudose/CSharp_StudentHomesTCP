using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using StudentHome.Api.Service;

namespace StudentHome.Server.Net
{
    internal class TcpServer
    {
        public IService Manager { get; set; }

        private readonly IPEndPoint endPoint;

        public TcpServer()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            endPoint = new IPEndPoint(ipAddress, 11000);
        }

        internal void Start()
        {
            try
            {
                Socket socketListner = new Socket(SocketType.Stream, ProtocolType.Tcp);
                socketListner.Bind(endPoint);
                socketListner.Listen(10);
                Trace.WriteLine("Server listens");
                while (true)
                {
                    Trace.WriteLine("Waiting for connections");
                    Socket clientSocket = socketListner.Accept();
                    Trace.WriteLine("Connection accepted");
                    Task.Factory.StartNew(new ClientHandler(clientSocket, Manager).Handle);
                }
            }
            catch (SocketException)
            {
                Console.WriteLine("Socket exception");
                throw;
            }
        }
    }
}
