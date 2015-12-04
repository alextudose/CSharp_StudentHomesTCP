using System;
using System.Diagnostics;
using StudentHome.Client.Net;
using StudentHome.Client.Service;
using StudentHome.Client.UI;

namespace StudentHome.Client
{
    public class ClientApp
    {
        static void Main(string[] args)
        {
            try
            {
                Trace.Listeners.Add(new TextWriterTraceListener("Logger.log", "FileListener"));
                Trace.Listeners.Add(new ConsoleTraceListener());
                
                UserInterface appUi = new UserInterface();
                ServiceProxy proxy = new ServiceProxy();
                TcpClient tcpClient = new TcpClient();
                proxy.TcpClient = tcpClient;
                appUi.Manager = proxy;
                appUi.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine("Client App has encountered an error. Sorry.");
                Trace.WriteLine(e.Message);
                Trace.WriteLine(e.StackTrace);
                Console.ReadKey();
            }

        }
    }
}
