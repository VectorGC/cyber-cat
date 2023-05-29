using Shared.Configurations;

namespace AuthService.Service;

public class AuthServiceAppSettings
{
    public MongoRepositorySettings MongoRepository { get; set; }
}