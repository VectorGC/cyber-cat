namespace AuthService;

public class AuthServiceAppSettings
{
    public class IdentityMongoDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public IdentityMongoDatabaseSettings IdentityMongoDatabase { get; set; }
}