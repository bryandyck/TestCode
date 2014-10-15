using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Palantir.Plan.LPSolver
{
    [ServiceContract(SessionMode = SessionMode.Required,
       CallbackContract = typeof(ISolverFeedback))]
    public interface ILPSolverServer
    {
        [OperationContract]
        LPCompletionResult Solve(LPProblem value);
    }

    public interface ISolverFeedback
    {
        /// <summary>
        /// Calls back to the client to provide a progress update.
        /// </summary>
        /// <param name="updateInfo"></param>
        /// <returns>True if the client wishes to cancel the current operation, otherwise it returns False.</returns>
        [OperationContract()]
        bool ProgressUpdate(LPProgressUpdate updateInfo);

        /// <summary>
        /// Informs the client of an exception thrown by the service.
        /// </summary>
        /// <param name="exceptionInfo">An exception report with debug information.</param>
        [OperationContract(IsOneWay = true)]
        void ExceptionThrown(LPExceptionReport exceptionInfo);
    }

}
