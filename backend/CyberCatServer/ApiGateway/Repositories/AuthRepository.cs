using ApiGateway.Exceptions;
using ApiGateway.Models;
using ApiGateway.Repositories.Models;
using MongoDB.Driver;

namespace ApiGateway.Repositories;

public interface IAuthUserRepository
{
    Task<string?> GetTokenOrEmpty(IUser user);
    Task<string?> CreateToken(IUser user);
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
        var token = await _userCollection
            .Find(u => u.Email == user.Email)
            .Project(u => u.Token).FirstOrDefaultAsync();

        return token;
    }

    public async Task<string?> CreateToken(IUser user)
    {
        var token = Guid.NewGuid().ToString();
        var update = Builders<UserDbModel>.Update.Set(u => u.Token, token);

        try
        {
            var resultModel = await _userCollection.FindOneAndUpdateAsync(u => u.Email == user.Email, update);
            return resultModel.Token;
        }
        catch (MongoException e)
        {
            throw new UnprocessableTokenException(e);
        }
    }
}