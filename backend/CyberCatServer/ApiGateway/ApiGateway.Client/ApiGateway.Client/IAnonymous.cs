using System.Threading.Tasks;
using ApiGateway.Client.Models;

namespace ApiGateway.Client
{
    public interface IAnonymous
    {
        Task SignUp(string email, string password, string name);
        Task<IUser> SignIn(string email, string password);
    }
}