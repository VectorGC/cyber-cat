using System.Threading.Tasks;
using ApiGateway.Client.Internal.Users;
using ApiGateway.Client.Models;

namespace ApiGateway.Client.Internal.Anonymous
{
    public class AnonymousServerless : IAnonymous
    {
        public Task SignUp(string email, string password, string name)
        {
            return Task.CompletedTask;
        }

        public Task<IUser> SignIn(string email, string password)
        {
            return Task.FromResult<IUser>(new ServerlessUser());
        }
    }
}