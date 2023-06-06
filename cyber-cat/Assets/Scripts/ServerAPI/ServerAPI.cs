using ApiGateway.Client;
using Cysharp.Threading.Tasks;
using Models;

namespace ServerAPI
{
    public class ServerAPI : Client, IServerAPI
    {
        private readonly Client _client;

        public ServerAPI(string uri) : base(uri)
        {
            _client = new Client(uri);
        }

        public async UniTask<ITask> GetTask(string taskId)
        {
            var task = await _client.GetTask(taskId);
            return new TaskModel(task.Name, task.Description);
        }

        public async UniTask<string> Authenticate(string email, string password)
        {
            return await _client.Authenticate(email, password);
        }

        public async UniTask<string> AuthorizePlayer(string token)
        {
            return await _client.AuthorizePlayer(token);
        }
    }
}