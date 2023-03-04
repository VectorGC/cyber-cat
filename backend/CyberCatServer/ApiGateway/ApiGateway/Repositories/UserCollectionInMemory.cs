namespace ApiGateway.Controllers;

public class UserCollectionInMemory : IUserCollection
{
    public readonly Dictionary<int, IUserDto> _users = new Dictionary<int, IUserDto>();

    public Task Add(IUserDto user)
    {
        var id = _users.Count;
        _users.Add(id, user);

        return Task.CompletedTask;
    }

    public Task<IUserDto> Get(int id)
    {
        return Task.FromResult(_users[id]);
    }

    public void Delete(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<IUserDto>> GetAll()
    {
        var readOnlyCollection = (IReadOnlyCollection<IUserDto>) _users.Values;
        return Task.FromResult(readOnlyCollection);
    }
}