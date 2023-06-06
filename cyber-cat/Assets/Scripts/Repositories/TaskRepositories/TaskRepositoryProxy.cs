using Cysharp.Threading.Tasks;
using Models;
using ServerAPI;

namespace Repositories.TaskRepositories
{
    /// <summary>
    /// Хранилище задач. Под капотом общается с сервером и никто снаружи об это не знает.
    /// </summary>
    public class TaskRepositoryProxy : ITaskRepository
    {
        private readonly IServerAPI _serverAPI;

        public TaskRepositoryProxy(IServerAPI serverAPI)
        {
            _serverAPI = serverAPI;
        }

        public async UniTask<ITask> GetTask(string taskId)
        {
            return await _serverAPI.GetTask(taskId);
        }
    }
}