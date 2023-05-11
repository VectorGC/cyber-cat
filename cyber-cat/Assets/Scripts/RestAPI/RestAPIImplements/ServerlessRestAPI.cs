using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Models;
using RestAPI.Dto;

namespace RestAPI
{
    public class ServerlessRestAPI : IRestAPI
    {
        public UniTask<ITokenSession> Authenticate(string login, string password, IProgress<float> progress = null)
        {
            var token = new TokenSessionDto
            {
                Token = "serverless"
            };

            return UniTask.FromResult<ITokenSession>(token);
        }

        public Task<ITasks> GetTasks(IProgress<float> progress = null)
        {
            throw new NotImplementedException();
        }
    }
}