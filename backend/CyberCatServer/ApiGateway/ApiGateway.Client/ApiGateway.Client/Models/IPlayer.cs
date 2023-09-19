using System.Threading.Tasks;

namespace ApiGateway.Client.Models
{
    public interface IPlayer
    {
        ITaskRepository Tasks { get; }
        Task Remove();
    }
}