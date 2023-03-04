using MongoDB.Bson;
using MongoDB.Driver;

namespace ApiGateway.Controllers;

public class UserCollectionMongoDb : IUserCollection
{
    private readonly IMongoCollection<UserDbDto> _userCollection;

    public UserCollectionMongoDb(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        var client = new MongoClient(connectionString);
        var cyberCatDb = client.GetDatabase("CyberCat");
        _userCollection = cyberCatDb.GetCollection<UserDbDto>("Users");
    }

    public async Task Add(IUserDto user)
    {
        var userDto = new UserDbDto(user);
        var count = await _userCollection.CountDocumentsAsync(FilterDefinition<UserDbDto>.Empty);
        userDto.Id = count;
        await _userCollection.InsertOneAsync(userDto);
    }

    public async Task<IUserDto?> Get(int id)
    {
        return await _userCollection.Find(user => user.Id == id).FirstOrDefaultAsync();
    }

    public void Delete(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyCollection<IUserDto>> GetAll()
    {
        var cursor = await _userCollection.FindAsync(FilterDefinition<UserDbDto>.Empty);
        return await cursor.ToListAsync();
    }
}