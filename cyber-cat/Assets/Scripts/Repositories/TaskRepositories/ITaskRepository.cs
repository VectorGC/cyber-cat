using Cysharp.Threading.Tasks;
using Models;

namespace Repositories.TaskRepositories
{
    public interface ITaskRepository
    {
        public UniTask<ITask> GetTask(string taskId);
    }
}