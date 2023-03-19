using ApiGateway.Exceptions;
using ApiGateway.Models;
using ApiGateway.Repositories.Migrations;
using ApiGateway.Repositories.Models;
using MongoDB.Driver;

namespace ApiGateway.Repositories;

public interface IUserRepository
{
    Task<IUser> GetUser(UserId id);
    Task ApplyMigration(IUserRepositoryMigration migration);
    Task Add(IEnumerable<IUser> users);
    Task<long> GetEstimatedCount();
    Task<UserId> FindByEmailSlowly(string email);
    Task<UserId> FindByTokenSlowly(string token);
}

public class UserRepositoryMongoDb : IUserRepository
{
    private readonly IMongoCollection<UserDbModel> _userCollection;

    public UserRepositoryMongoDb(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        var client = new MongoClient(connectionString);
        var cyberCatDb = client.GetDatabase("CyberCat");
        _userCollection = cyberCatDb.GetCollection<UserDbModel>("Users");
    }

    public async Task<IUser> GetUser(UserId id)
    {
        var user = await _userCollection.Find(u => u.Email == id).FirstOrDefaultAsync();
        if (user == null)
        {
            throw new UserNotFound();
        }

        return user;
    }

    public async Task ApplyMigration(IUserRepositoryMigration migration)
    {
        await migration.Apply(this);
    }

    public async Task Add(IUser user)
    {
        var userDto = new UserDbModel(user);
        await _userCollection.InsertOneAsync(userDto, cancellationToken: GetCT());
    }

    public async Task Add(IEnumerable<IUser> users)
    {
        var userDbModels = users.Select(u => new UserDbModel(u));
        await _userCollection.InsertManyAsync(userDbModels, cancellationToken: GetCT());
    }

    public async Task<UserId> FindByEmailSlowly(string email)
    {
        var user = await _userCollection.Find(u => u.Email == email).FirstOrDefaultAsync(GetCT());
        if (user == null)
        {
            throw new UserNotFound();
        }

        return new UserId(user.Email);
    }

    public async Task<UserId> FindByTokenSlowly(string token)
    {
        var user = await _userCollection.Find(u => u.Token == token).FirstOrDefaultAsync(GetCT());
        if (user == null)
        {
            throw new UserNotFound();
        }

        return new UserId(user.Email);
    }

    public async Task<long> GetEstimatedCount()
    {
        // Используем Estimated... вместо Count. Потому что Estimated работает в разы быстрее на больших коллекциях. Но иногда имеет неточности.
        return await _userCollection.EstimatedDocumentCountAsync(cancellationToken: GetCT());
    }

    private CancellationToken GetCT()
    {
        // Ни одни запрос не должен превышать 5 секунд. Если превышает - вызывается отмена таски и эксепшн.
        return new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token;
    }
}