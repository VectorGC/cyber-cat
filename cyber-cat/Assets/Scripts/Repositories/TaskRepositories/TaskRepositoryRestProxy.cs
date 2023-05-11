using System;
using Cysharp.Threading.Tasks;
using Models;
using RestAPI;

namespace Repositories.TaskRepositories
{
    /// <summary>
    /// Хранилище задач. Под капотом общается с сервером и никто снаружи об это не знает.
    /// </summary>
    public class TaskRepositoryRestProxy : ITaskRepository
    {
        private readonly IRestAPI restAPI;

        public TaskRepositoryRestProxy(IRestAPI restAPI)
        {
            this.restAPI = restAPI;
        }

        public async UniTask<ITask> GetTask(string taskId, IProgress<float> progress = null)
        {
            var tasks = await restAPI.GetTasks(progress);
            return tasks[taskId];
        }
    }
}