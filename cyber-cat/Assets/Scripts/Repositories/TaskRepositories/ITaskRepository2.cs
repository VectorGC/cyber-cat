using Cysharp.Threading.Tasks;
using Models;

namespace Repositories.TaskRepositories
{
    public interface ITaskRepository2
    {
        public UniTask<ITask> GetTask(string taskId);
    }
}