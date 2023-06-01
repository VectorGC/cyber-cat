using System;
using System.Threading.Tasks;
using Shared.Dto;

namespace ApiGateway.Client
{
    public interface IClient
    {
        Task<string> Authenticate(string email, string password, IProgress<float> progress = null);
        Task<string> AuthorizePlayer(string token, IProgress<float> progress = null);
        Task<TaskDto> GetTask(string taskId, IProgress<float> progress = null);
        void AddAuthorizationToken(string token);
        void RemoveAuthorizationToken();
    }
}