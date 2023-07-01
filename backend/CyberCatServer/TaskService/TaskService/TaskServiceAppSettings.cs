using Shared.Server.Configurations;

namespace TaskService;

public class TaskServiceAppSettings
{
    public MongoRepositorySettings MongoRepository { get; set; }
}