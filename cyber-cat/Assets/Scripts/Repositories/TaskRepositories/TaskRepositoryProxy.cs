using System;
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
        private readonly IServerAPI serverAPI;

        public TaskRepositoryProxy(IServerAPI serverAPI)
        {
            this.serverAPI = serverAPI;
        }

        public async UniTask<ITask> GetTask(string taskId, IProgress<float> progress = null)
        {
            var task = await serverAPI.GetTask(taskId, progress);
            return task;
        }
    }
}