using System;
using System.Threading.Tasks;
using ApiGateway.Client;
using Shared.Dto;

namespace ServerAPI
{
    public class ServerlessClient : IClient
    {
        public Task<string> Authenticate(string email, string password, IProgress<float> progress)
        {
            return Task.FromResult("serverless");
        }

        public Task<string> AuthorizePlayer(string token, IProgress<float> progress = null)
        {
            return Task.FromResult("Cyber Cat");
        }

        public Task<TaskDto> GetTask(string taskId, IProgress<float> progress = null)
        {
            var task = new TaskDto()
            {
                Name = "Hello world",
                Description = "Вы играете без сервера. Чтобы решать задачи - включите интернет"
            };

            return Task.FromResult(task);
        }

        public void AddAuthorizationToken(string token)
        {
        }

        public void RemoveAuthorizationToken()
        {
        }

        public Task<string> Authenticate(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<string> AuthorizePlayer(string token)
        {
            throw new NotImplementedException();
        }

        public Task<TaskDto> GetTask(string taskId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetSavedCode(string taskId)
        {
            throw new NotImplementedException();
        }

        public Task SaveCode(string taskId, string sourceCode)
        {
            throw new NotImplementedException();
        }

        public Task RemoveSavedCode(string taskId)
        {
            throw new NotImplementedException();
        }

        public Task<VerdictDto> VerifySolution(string taskId, string sourceCode)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}