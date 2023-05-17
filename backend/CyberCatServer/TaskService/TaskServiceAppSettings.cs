using Shared.Configurations;

namespace TaskService;

public class TaskServiceAppSettings
{
    public MongoRepositorySettings MongoRepository { get; set; }
}