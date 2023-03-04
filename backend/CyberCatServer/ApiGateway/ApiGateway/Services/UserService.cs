namespace ApiGateway.Controllers;

public class UserService : IUserService
{
    private readonly IUserCollection _userCollection;

    public UserService(IUserCollection userCollection)
    {
        _userCollection = userCollection;
    }

    public async Task Add(IUserDto user)
    {
        await _userCollection.Add(user);
    }

    public void Get(string id)
    {
        throw new NotImplementedException();
    }

    public void Delete(string id)
    {
        throw new NotImplementedException();
    }
}