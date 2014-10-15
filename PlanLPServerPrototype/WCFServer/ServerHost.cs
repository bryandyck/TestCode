using System;
using System.ServiceModel;
using System.Threading;
using System.ServiceModel.Channels;

namespace Palantir.Plan.LPSolver
{
    /// <summary>
    /// Creates the LP Solver server
    /// </summary>
    public class ServerHost
    {
        public enum ChannelType
        {
            Tcp = 1,
            NamedPipes = 2
        }

        static AutoResetEvent _StopFlag = new AutoResetEvent(false);

        static void Main()
        {
            AutoResetEvent serverStartedFlag = new AutoResetEvent(false);
            Thread server = new Thread(() => StartServer(serverStartedFlag));
            server.Start();
            serverStartedFlag.WaitOne(); //wait for the server to start up

            Console.WriteLine();
            Console.WriteLine("Service is available. Press <ENTER> to exit.");
            Console.ReadLine();

            StopServer();
        }

        public static void StartServer(AutoResetEvent serverStartSignal)
        {
            Properties.Settings settings = new Properties.Settings();
            int port = settings.Port;
            string server = settings.Server;

            StartServer(serverStartSignal, server, port, "PlanLPSolver", ChannelType.Tcp);
        }

        public static void StartServer(AutoResetEvent serverStartSignal, string serverName, int port, string channelName, ChannelType type)
        {
            string uriAddress = null;
            Binding hostBinding = null;

            switch (type)
            {
                case ChannelType.NamedPipes:
                    uriAddress = string.Format("net.pipe://{0}/{1}", serverName, channelName);
                    hostBinding = new NetNamedPipeBinding();
                    break;
                case ChannelType.Tcp:
                    uriAddress = string.Format("net.tcp://{0}:{1}", serverName, port);
                    hostBinding = new NetTcpBinding();
                    break;
            }

            using (ServiceHost host = new ServiceHost(
                typeof(Solver),
                new Uri[]{                                            
                new Uri(uriAddress)
                }))
            {
                
                host.AddServiceEndpoint(typeof(ILPSolverServer),
                  hostBinding,
                  channelName);

                Console.WriteLine("Plan LP Server - Starting...");
                host.Open();
                Console.WriteLine("Plan LP Server - Started");

                if (serverStartSignal != null) serverStartSignal.Set();
                _StopFlag.WaitOne(); //wait for the stop signal

                Console.WriteLine("Plan LP Server - Shutting down...");
                host.Close();
                Console.WriteLine("Plan LP Server - Shutdown");
            }
        }

        public static void StopServer()
        {
            _StopFlag.Set();
        }

    }
}
