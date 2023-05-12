using System;
using Cysharp.Threading.Tasks;
using Models;
using RestAPI.InternalDto;

namespace RestAPI.RestAPIImplements
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

        public UniTask<ITasks> GetTasks(IProgress<float> progress = null)
        {
            throw new NotImplementedException();
        }

        public UniTask<IPlayer> AuthorizeAsPlayer(ITokenSession token)
        {
            var player = new PlayerDto
            {
                Name = "Player",
                Token = token
            };

            return UniTask.FromResult<IPlayer>(player);
        }
    }
}