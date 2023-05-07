using ApiGateway.Models;
using ApiGateway.Repositories.Models;
using MongoDB.Driver;
using Shared.Exceptions;

namespace ApiGateway.Repositories;

public interface IAuthUserRepository
{
    Task<string?> GetTokenOrEmpty(IUser user);
    Task<string> CreateToken(IUser user);
}

public class AuthUserRepositoryMongoDb : IAuthUserRepository
{
    private readonly IMongoCollection<UserDbModel> _userCollection;

    public AuthUserRepositoryMongoDb(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        var client = new MongoClient(connectionString);
        var cyberCatDb = client.GetDatabase("CyberCat");
        _userCollection = cyberCatDb.GetCollection<UserDbModel>("Users");
    }

    public async Task<string?> GetTokenOrEmpty(IUser user)
    {
        var userDbModel = await _userCollection.Find(u => u.Email == user.Email).FirstOrDefaultAsync();
        return userDbModel.Token;
    }

    public async Task<string> CreateToken(IUser user)
    {
        var token = Guid.NewGuid().ToString();
        var update = Builders<UserDbModel>.Update.Set(u => u.Token, token);

        try
        {
            await _userCollection.FindOneAndUpdateAsync(u => u.Email == user.Email, update);
            return token;
        }
        catch (MongoException e)
        {
            throw new UnprocessableTokenException(e);
        }
    }
}