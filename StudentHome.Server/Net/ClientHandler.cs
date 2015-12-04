using System.Diagnostics;
using System.Net.Sockets;
using StudentHome.Api.Net;
using StudentHome.Api.Service;
using StudentHome.Server.Service;

namespace StudentHome.Server.Net
{
    internal class ClientHandler
    {
        private Socket clientSocket;
        private ServiceImpl service;

        internal ClientHandler(Socket clientSocket, IService service)
        {
            this.clientSocket = clientSocket;
            this.service = (ServiceImpl) service;
        }

        internal void Handle()
        {
            try
            {
                Trace.WriteLine("Handle client request");
                byte[] bytes = new byte[10000];
                int bytesRec = clientSocket.Receive(bytes);
                Trace.WriteLine("Request received");

                Message request = bytes.Deserialize<Message>();
                Message result = service.Process(request);
                
                clientSocket.Send(result.SerializeToByteArray());
                Trace.WriteLine("Response sent");
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
            catch (SocketException)
            {
                Trace.WriteLine("Handle client socket exception");
            }
        }
    }
}
