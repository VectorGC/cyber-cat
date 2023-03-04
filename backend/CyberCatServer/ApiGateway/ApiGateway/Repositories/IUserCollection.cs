namespace ApiGateway.Controllers;

public interface IUserCollection
{
    Task Add(IUserDto user);
    Task<IUserDto?> Get(int id);
    void Delete(string id);
    Task<IReadOnlyCollection<IUserDto>> GetAll();
}