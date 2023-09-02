using ApiGateway.Client.Models;
using Shared.Models.Dto.Data;

namespace ApiGateway.Client.Internal.Tasks.Statuses
{
    public class HaveSolution : ITaskProgressStatus
    {
        public string Solution { get; }

        public HaveSolution(TaskData data)
        {
            Solution = data.Solution;
        }
    }
}