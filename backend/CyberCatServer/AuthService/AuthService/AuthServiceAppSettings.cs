using Shared.Server.Configurations;

namespace AuthService;

public class AuthServiceAppSettings
{
    public MongoRepositorySettings MongoRepository { get; set; }
}