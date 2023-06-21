using ServerAPI;

namespace Repositories.TaskRepositories
{
    /// <summary>
    /// Хранилище задач. Под капотом общается с сервером и никто снаружи об это не знает.
    /// </summary>
    public class TaskRepository2Proxy : ITaskRepository2
    {
        private readonly IServerAPI _serverAPI;

        public TaskRepository2Proxy(IServerAPI serverAPI)
        {
            _serverAPI = serverAPI;
        }
    }
}