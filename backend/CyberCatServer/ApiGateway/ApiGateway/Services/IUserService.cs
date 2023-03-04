namespace ApiGateway.Controllers;

public interface IUserService
{
    Task Add(IUserDto user);
    void Get(string id);
    void Delete(string id);
}