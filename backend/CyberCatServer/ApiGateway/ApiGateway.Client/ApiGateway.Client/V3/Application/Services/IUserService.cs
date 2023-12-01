using System.Threading.Tasks;
using ApiGateway.Client.V3.Infrastructure;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.V3.Application.Services
{
    public interface IUserService
    {
        Task<Result> RegisterPlayer(string email, string password, string userName);
        Task<Result<AuthorizationToken>> LoginUser(string email, string password);
        UserModel GetUserByToken(AuthorizationToken token);
        Result RemoveUserByToken(AuthorizationToken token, string password);
    }
}