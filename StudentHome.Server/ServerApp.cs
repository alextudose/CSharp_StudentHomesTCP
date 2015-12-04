using System;
using System.Diagnostics;
using StudentHome.Api.Service;
using StudentHome.Server.Net;
using StudentHome.Server.Service;

namespace StudentHome.Server
{
    public class ServerApp
    {
        static void Main(string[] args)
        {
            try
            {
                Trace.Listeners.Add(new TextWriterTraceListener("Logger.log", "FileListener"));
                Trace.Listeners.Add(new ConsoleTraceListener());
                Constants.SetPaths();

                TcpServer tcpServer = new TcpServer();
                IService service = new ServiceImpl();
                tcpServer.Manager = service;
                tcpServer.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine("Server App has encountered an error. Sorry.");
                Trace.WriteLine(e.Message);
                Trace.WriteLine(e.StackTrace);
                Console.ReadKey();
            }
        }
    }
}
