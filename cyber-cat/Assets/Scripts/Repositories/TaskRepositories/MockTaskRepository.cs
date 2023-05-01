using System;
using Cysharp.Threading.Tasks;
using Models;

namespace Repositories.TaskRepositories
{
    public class MockTaskRepository : ITaskRepository
    {
        public UniTask<ITask> GetTask(string taskId, IProgress<float> progress = null)
        {
            return UniTask.FromResult<ITask>(new MockSimpleTask());
        }
    }
}