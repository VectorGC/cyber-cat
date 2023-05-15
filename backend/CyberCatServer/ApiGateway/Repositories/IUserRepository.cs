using ApiGateway.Models;

namespace ApiGateway.Repositories;

public interface IUserRepository
{
    Task<IUser> GetUser(UserId id);
    Task Add(IEnumerable<IUser> users);
    Task<long> GetEstimatedCount();
    Task<UserId> FindByEmailSlowly(string email);
    Task<UserId> FindByTokenSlowly(string token);
}