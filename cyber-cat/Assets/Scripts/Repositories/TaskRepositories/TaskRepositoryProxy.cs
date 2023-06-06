using System;
using ApiGateway.Client;
using Cysharp.Threading.Tasks;
using Models;

namespace Repositories.TaskRepositories
{
    /// <summary>
    /// Хранилище задач. Под капотом общается с сервером и никто снаружи об это не знает.
    /// </summary>
    public class TaskRepositoryProxy : ITaskRepository
    {
        private readonly IClient _client;

        public TaskRepositoryProxy(IClient client)
        {
            _client = client;
        }

        public async UniTask<ITask> GetTask(string taskId)
        {
            var taskDto = await _client.GetTask(taskId);
            return new TaskCode(taskDto);
        }
    }
}