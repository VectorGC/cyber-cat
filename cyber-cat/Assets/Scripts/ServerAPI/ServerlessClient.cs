using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Models;
using Shared.Dto;
using Shared.Models;
using ITask = Shared.Models.ITask;
using IVerdict = Shared.Models.IVerdict;

namespace ServerAPI
{
    public class ServerlessClient : IServerAPI
    {
        public Task<string> Authenticate(string email, string password, IProgress<float> progress)
        {
            return Task.FromResult("serverless");
        }

        public Task<string> AuthorizePlayer(string token, IProgress<float> progress = null)
        {
            return Task.FromResult("Cyber Cat");
        }

        public Task<ITask> GetTask(string taskId, IProgress<float> progress = null)
        {
            var task = new TaskDto()
            {
                Name = "Hello world",
                Description = "Вы играете без сервера. Чтобы решать задачи - включите интернет"
            };

            return Task.FromResult<ITask>(task);
        }

        public void AddAuthorizationToken(string token)
        {
        }

        public void RemoveAuthorizationToken()
        {
        }

        public UniTask<string> Authenticate(string email, string password)
        {
            return UniTask.FromResult("Cyber Cat");
        }

        public UniTask<string> AuthorizePlayer(string token)
        {
            return UniTask.FromResult("Cyber Cat");
        }

        public UniTask<Models.ITask> GetTask(string taskId)
        {
            var task = new TaskModel("Hello world", "Вы играете без сервера. Чтобы решать задачи - включите интернет");
            return UniTask.FromResult<Models.ITask>(task);
        }

        public Task<string> GetSavedCode(string taskId)
        {
            return Task.FromResult("Hello World!");
        }

        public Task SaveCode(string taskId, string sourceCode)
        {
            return Task.CompletedTask;
        }

        public Task RemoveSavedCode(string taskId)
        {
            return Task.CompletedTask;
        }

        public Task<IVerdict> VerifySolution(string taskId, string sourceCode)
        {
            var verdict = new VerdictDto()
            {
                Status = VerdictStatus.Success
            };
            return Task.FromResult<IVerdict>(verdict);
        }

        public void Dispose()
        {
        }
    }
}