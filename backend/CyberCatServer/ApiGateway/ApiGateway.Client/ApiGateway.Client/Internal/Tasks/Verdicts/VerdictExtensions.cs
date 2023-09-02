using System.Threading.Tasks;
using ApiGateway.Client.Internal.Tasks.Statuses;
using ApiGateway.Client.Models;

namespace ApiGateway.Client.Internal.Tasks.Verdicts
{
    public static class VerdictExtensions
    {
        public static async Task<string> GetLastSavedSolution(this ITask task)
        {
            switch (await task.GetStatus())
            {
                case Complete complete:
                    return complete.Solution;
                case HaveSolution haveSolution:
                    return haveSolution.Solution;
                case NotStarted notStarted:
                    return string.Empty;
                default:
                    return string.Empty;
            }
        }
    }
}