using ApiGateway.Client.Models;

namespace ApiGateway.Client.Internal.Tasks.Statuses
{
    public class Complete : ITaskProgressStatus
    {
        public string Solution { get; }

        public Complete(string solution)
        {
            Solution = solution;
        }
    }
}