using System;
using Cysharp.Threading.Tasks;
using Models;
using Newtonsoft.Json;

namespace Repositories.TaskRepositories
{
    /// <summary>
    /// Хранилище задач. Под капотом общается с сервером и никто снаружи об это не знает.
    /// </summary>
    public class TaskRepositoryRestProxy : ITaskRepository
    {
        public async UniTask<ITask> GetTask(string taskId, IProgress<float> progress = null)
        {
            var tasksJson = await RestAPI_v1.GetTasks(progress);
            var tasks = JsonConvert.DeserializeObject<TaskModels>(tasksJson);

            return tasks.Tasks[taskId];
        }
    }
}