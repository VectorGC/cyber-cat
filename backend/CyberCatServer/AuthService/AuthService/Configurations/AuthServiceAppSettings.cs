using Shared.Server.Configurations;

namespace AuthService.Configurations;

public class AuthServiceAppSettings
{
    public MongoRepositorySettings MongoRepository { get; set; }
}