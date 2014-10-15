using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Palantir.Plan.LPSolver;
using System.Threading;
using System.ServiceModel.Channels;

namespace PalantirPlan
{
    //SIMULATES WHAT WOULD BE CALLED FROM THE ACTUAL APPLICATION
    class Program
    {
        static void Main(string[] args)
        {
            Properties.Settings settings = new Properties.Settings();
            
            int port = settings.Port;
            string serverName = settings.Server;
            bool useLocal = settings.UseLocal;
            string channelName = "PlanLPSolver";
            string uriAddress = null;
            Binding hostBinding = null;

            if (useLocal)
            {
                //to make sure the server channel is named uniquely, append the processID when it is run local
                channelName += System.Diagnostics.Process.GetCurrentProcess().Id;
                AutoResetEvent serverStartedFlag = new AutoResetEvent(false);
                Thread server = new Thread(() => ServerHost.StartServer(serverStartedFlag, "localhost", 0, channelName, ServerHost.ChannelType.NamedPipes));
                server.Start();
                serverStartedFlag.WaitOne(); //wait for the server to start up

                uriAddress = string.Format("net.pipe://{0}/{1}/{1}", serverName, channelName);
                hostBinding = new NetNamedPipeBinding();
            }
            else
            {
                uriAddress = string.Format("net.tcp://{0}:{1}/{2}", serverName, port, channelName);
                hostBinding = new NetTcpBinding();
            }

            SolverFeedback callback = new SolverFeedback();

            //create the pipe
            DuplexChannelFactory<ILPSolverServer> pipeFactory =
               new DuplexChannelFactory<ILPSolverServer>(
                  callback,
                  hostBinding,
                  new EndpointAddress(uriAddress));
                     //string.Format("net.tcp://{0}:{1}/{2}", serverName, port, channelName)));
            //create the proxy and open the channel
            Palantir.Plan.LPSolver.ILPSolverServer pipeProxy =
              pipeFactory.CreateChannel();

            Console.WriteLine();
            Console.WriteLine("Enter some text and the server will reverse it. Enter an empty string to simulate an exception or EXIT to quite the application.");


            while (true)
            {
                string str = Console.ReadLine();
                if (str == "EXIT") break;
                LPCompletionResult result = pipeProxy.Solve(new LPProblem() { Input = str }); //send the input to the server
                if (result != null)
                    Console.WriteLine("Solver completed!!\nResult: {0}", result.Arg1);
                else
                    Console.WriteLine("Exception detected. Null result returned.");
            }
            
            if (useLocal)
            {
                ServerHost.StopServer();
            }
        }
    }

    class SolverFeedback : Palantir.Plan.LPSolver.ISolverFeedback
    {
        public bool ProgressUpdate(Palantir.Plan.LPSolver.LPProgressUpdate updateInfo)
        {
            Console.WriteLine("Message: {0}\tProgress: {1} %", updateInfo.Message, updateInfo.Progress);

            //change this to some appropriate abort condition
            if (updateInfo.Progress == 70 && updateInfo.Message.IndexOf("abort", StringComparison.InvariantCultureIgnoreCase) >= 0) 
                return true;

            return false;
        }

        public void ExceptionThrown(Palantir.Plan.LPSolver.LPExceptionReport exceptionInfo)
        {
            Console.WriteLine("Exception detected: {0}", exceptionInfo);
        }

    }
}
