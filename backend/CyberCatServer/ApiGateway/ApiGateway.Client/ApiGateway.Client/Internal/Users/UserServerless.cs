using System.Threading.Tasks;
using ApiGateway.Client.Internal.Players;
using ApiGateway.Client.Models;

namespace ApiGateway.Client.Internal.Users
{
    internal class ServerlessUser : IUser
    {
        public Task<IPlayer> SignInAsPlayer()
        {
            return Task.FromResult<IPlayer>(new ServerlessPlayer());
        }

        public Task Remove(string password)
        {
            return Task.CompletedTask;
        }
    }
}