using System;
using ApiGateway.Client;
using Cysharp.Threading.Tasks;
using Models;
using Shared.Models.Models;
using IVerdict = Models.IVerdict;
using VerdictStatus = Models.VerdictStatus;

namespace ServerAPI
{
    public class ServerAPI : IServerAPI
    {
        private readonly Client _client;

        public ServerAPI(string uri)
        {
            _client = new Client(uri);
        }

        public async UniTask<ITask> GetTask(string taskId)
        {
            var task = await _client.GetTask(taskId);
            throw new NotImplementedException();
            //return new TaskModel(task.Name, task.Description);
        }

        public async UniTask<string> Authenticate(string email, string password)
        {
            return await _client.Authenticate(email, password);
        }

        public async UniTask<string> AuthorizePlayer(string token)
        {
            return await _client.AuthorizePlayer(token);
        }

        public void AddAuthorizationToken(string token)
        {
            _client.AddAuthorizationToken(token);
        }

        public async UniTask<string> GetSavedCode(string taskId)
        {
            return await _client.GetSavedCode(taskId);
        }

        public async UniTask<IVerdict> VerifySolution(string taskId, string sourceCode)
        {
            var verdictDto = await _client.VerifySolution(taskId, sourceCode);

            var status = (int) verdictDto.Status;
            return new Verdict((VerdictStatus) status, verdictDto.Error, verdictDto.TestsPassed);
        }
    }
}