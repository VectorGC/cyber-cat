using Shared.Server.Configurations;

namespace PlayerService;

public class PlayerServiceAppSettings
{
    public class ConnectionStringsSettings
    {
        public string JudgeServiceGrpcEndpoint { get; set; }
        public Uri JudgeServiceGrpcAddress => new(JudgeServiceGrpcEndpoint);
    }

    public MongoRepositorySettings MongoRepository { get; set; }
    public ConnectionStringsSettings ConnectionStrings { get; set; }
}