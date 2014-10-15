using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Palantir.Plan.LPSolver
{
    //[CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession,
        ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class Solver : ILPSolverServer
    {

        public LPCompletionResult Solve(LPProblem value)
        {
            //get the callback class to pass into the function
            ISolverFeedback callbacks =
               OperationContext.Current.GetCallbackChannel<ISolverFeedback>();

            try
            {
                if (value == null) throw new ArgumentNullException("value");

                //sample function that does something long running and provids feedback
                string result = SampleFunction(value.Input, callbacks);

                return new LPCompletionResult() {Arg1 = result, Arg2 = 123, Arg3 = DateTime.Now};
            }
            catch (Exception ex)
            {
                //return the exception thru a callback to enable us to add any additional information 
                //we may need/want in the LPExceptionReport class.
                if (callbacks != null)
                {
                    LPExceptionReport callbackException = new LPExceptionReport(ex);
                    callbacks.ExceptionThrown(callbackException);
                    return null;
                }
                else
                    throw;
            }

        }


        /// <summary>
        /// Simple function that reverses a string
        /// </summary>
        string SampleFunction(string value, ISolverFeedback feedback)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException("lp problem input not defined.");

            Console.WriteLine(value);

            char[] retVal = value.ToCharArray();
            int idx = 0;
            for (int i = value.Length - 1; i >= 0; i--)
                retVal[idx++] = value[i];

            //this just simulates a long running operation to provide feedback
            for (int i = 0; i < 10; i++)
            {
                LPProgressUpdate update = new LPProgressUpdate((i + 1f) / 10f * 100, string.Format("Processing iteration {0}.\tSolving for: {1}", i + 1, value));
                if(feedback.ProgressUpdate(update))
                {
                    return "ABORTED!!";
                }

                System.Threading.Thread.Sleep(500); //some long running operation
            }

            return new string(retVal);
        }

    }
}
