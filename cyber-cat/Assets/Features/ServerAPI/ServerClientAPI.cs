using ApiGateway.Client;
using Cysharp.Threading.Tasks;
using Shared.Models.Dto;
using Shared.Models.Models;

namespace ServerAPI
{
    public class ServerClientAPI : IServerAPI
    {
        private string _uri;
        private IServerAPI _serverless = new Serverless();
        private string _token;

        public ServerClientAPI(string uri)
        {
            _uri = uri;
        }

        public async UniTask<TaskDto> GetTask(string taskId)
        {
            var client = ServerClient.Create(_uri, _token);
            return await client.Tasks.GetTask(taskId);
        }

        public async UniTask<string> Authenticate(string email, string password)
        {
            var client = ServerClient.Create(_uri);
            return await client.Authorization.GetAuthenticationToken(email, password);
        }

        public UniTask<string> AuthorizePlayer(string token)
        {
            return UniTask.FromResult("Cat");
        }

        public void AddAuthorizationToken(string token)
        {
            _token = token;
        }

        public async UniTask<string> GetSavedCode(string taskId)
        {
            var client = ServerClient.Create(_uri, _token);
            return await client.SolutionService.GetSavedCode(taskId);
        }

        public async UniTask<IVerdict> VerifySolution(string taskId, string sourceCode)
        {
            var client = ServerClient.Create(_uri, _token);
            return await client.JudgeService.VerifySolution(taskId, sourceCode);
        }
    }
}