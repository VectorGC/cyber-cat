using System.Threading.Tasks;

namespace ApiGateway.Client.Models
{
    public interface IUser
    {
        Task<IPlayer> SignInAsPlayer();
        Task Remove(string password);
    }
}