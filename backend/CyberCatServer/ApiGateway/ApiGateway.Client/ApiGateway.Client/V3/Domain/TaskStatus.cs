using Shared.Models.Domain.Tasks;
using Shared.Models.Enums;

namespace ApiGateway.Client.V3.Domain
{
    public class TaskStatus
    {
        public bool IsComplete => _progress.Status == TaskProgressStatus.Complete;
        public bool IsStarted => _progress.Status == TaskProgressStatus.HaveSolution;

        public string LastSolution => _progress.Solution;

        private readonly TaskProgressData _progress;

        public TaskStatus(TaskProgressData progress)
        {
            _progress = progress;
        }
    }
}