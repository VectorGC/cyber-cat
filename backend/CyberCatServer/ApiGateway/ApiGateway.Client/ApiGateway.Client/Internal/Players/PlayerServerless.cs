using System.Threading.Tasks;
using ApiGateway.Client.Models;

namespace ApiGateway.Client.Internal.Players
{
    internal class ServerlessPlayer : IPlayer
    {
        public ITaskRepository Tasks => throw new System.NotImplementedException();

        public Task Remove()
        {
            return Task.CompletedTask;
        }
    }
}