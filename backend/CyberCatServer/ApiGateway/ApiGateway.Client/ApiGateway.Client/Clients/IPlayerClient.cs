using System.Threading.Tasks;
using ApiGateway.Client.Services;

namespace ApiGateway.Client.Clients
{
    public interface IPlayerClient
    {
        ITaskRepository Tasks { get; }
        ISolutionService SolutionService { get; }
        IPlayerService PlayerService { get; }
        Task RemovePlayer();
    }
}