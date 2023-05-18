using Shared.Configurations;

namespace JudgeService;

public class JudgeServiceAppSettings
{
    public class ConnectionStringsSettings
    {
        public string CppLauncherServiceGrpcEndpoint { get; set; }

        public Uri CppLauncherServiceGrpcAddress => new(CppLauncherServiceGrpcEndpoint);
    }

    public MongoRepositorySettings MongoRepository { get; set; }
    public ConnectionStringsSettings ConnectionStrings { get; set; }
}