using Shared.Models.Domain.Tasks;

namespace ApiGateway.Client.V3.Domain
{
    public class TaskProgressStatus
    {
        public bool IsComplete => _progress.StatusType == Shared.Models.Domain.Tasks.TaskProgressStatusType.Complete;
        public bool IsStarted => _progress.StatusType == Shared.Models.Domain.Tasks.TaskProgressStatusType.HaveSolution;

        public string LastSolution => _progress.Solution;

        private readonly TaskProgress _progress;

        public TaskProgressStatus(TaskProgress progress)
        {
            _progress = progress;
        }
    }
}