using Shared.Models.Domain.Tasks;

namespace ApiGateway.Client.Domain
{
    public class TaskProgressStatus
    {
        public bool IsComplete => _progress.StatusType == TaskProgressStatusType.Complete;
        public bool IsStarted => !string.IsNullOrEmpty(LastSolution) && (_progress.StatusType == TaskProgressStatusType.HaveSolution || IsComplete);
        public string LastSolution => _progress.Solution;

        private readonly TaskProgress _progress;

        public TaskProgressStatus(TaskProgress progress)
        {
            _progress = progress;
        }
    }
}