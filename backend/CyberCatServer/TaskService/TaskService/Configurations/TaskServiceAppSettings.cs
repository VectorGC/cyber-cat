using Shared.Server.Configurations;

namespace TaskService.Configurations;

public class TaskServiceAppSettings
{
    public MongoRepositorySettings MongoRepository { get; set; }
}