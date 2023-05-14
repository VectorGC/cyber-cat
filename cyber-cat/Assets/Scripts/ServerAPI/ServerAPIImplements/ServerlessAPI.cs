using System;
using Cysharp.Threading.Tasks;
using Models;
using ServerAPI.InternalDto;

namespace ServerAPI.ServerAPIImplements
{
    public class ServerlessAPI : IServerAPI
    {
        public UniTask<ITokenSession> Authenticate(string login, string password, IProgress<float> progress = null)
        {
            var token = new TokenSessionDto
            {
                AccessToken = "serverless"
            };

            return UniTask.FromResult<ITokenSession>(token);
        }

        public UniTask<ITasks> GetTasks(IProgress<float> progress = null)
        {
            throw new NotImplementedException();
        }

        public UniTask<IPlayer> AuthorizePlayer(ITokenSession token, IProgress<float> progress = null)
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