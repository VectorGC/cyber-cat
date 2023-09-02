using Shared.Server.Configurations;

namespace TaskService.Configurations;

public class TaskServiceAppSettings
{
    public class ConnectionStringsSettings
    {
        public string PlayerServiceGrpcEndpoint { get; set; }
        public Uri PlayerServiceGrpcAddress => new(PlayerServiceGrpcEndpoint);
    }

    public MongoRepositorySettings MongoRepository { get; set; }
    public ConnectionStringsSettings ConnectionStrings { get; set; }
}