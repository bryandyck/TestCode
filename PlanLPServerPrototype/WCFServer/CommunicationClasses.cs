using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Palantir.Plan.LPSolver
{

    /// <summary>
    /// Defines the LP problem for the server to solve.
    /// </summary>
    [Serializable]
    public class LPProblem
    {
        public string Input { get; set; }
    }

    /// <summary>
    /// For updating the caller with a progress update.
    /// </summary>
    [Serializable]
    public class LPProgressUpdate
    {
        /// <summary>
        /// % complete (between 0 and 100)
        /// </summary>
        public float Progress { get; private set; }
        /// <summary>
        /// Message from the solver to the caller.
        /// </summary>
        public string Message { get; private set; }

        public LPProgressUpdate(float progress, string message)
        {
            if (progress < 0 || progress > 100) throw new ArgumentOutOfRangeException("progress", progress, "Progress % must be between 0 and 100.");
            if (string.IsNullOrEmpty(message)) throw new ArgumentNullException("message");

            Progress = progress;
            Message = message;
        }

    }

    /// <summary>
    /// Updates the caller with an unhandled exception from the solver. This needs to be broken down to its parts as exceptions should never be passed across service boundries.
    /// </summary>
    [Serializable]
    public class LPExceptionReport
    {
        /// <summary>
        /// Exception thrown by the solver.
        /// </summary>
        public string Message{ get; private set; }

        public string Source { get; private set; }

        public string StackTrace { get; private set; }

        string _ToString;

        public LPExceptionReport(Exception ex)
        {
            Message = ex.Message;
            Source = ex.Source;
            StackTrace = ex.StackTrace;

            _ToString = ex.ToString();
        }

        public override string ToString()
        {
            return _ToString;
        }
    }

    /// <summary>
    /// Passes the results of the solver back to the caller.
    /// </summary>
    [Serializable]
    public class LPCompletionResult
    {
        //whatever result information that needs to be passed back to the caller

        public string Arg1 { get; set; }

        public int Arg2 { get; set; }

        public DateTime Arg3 { get; set; }
    }

}
