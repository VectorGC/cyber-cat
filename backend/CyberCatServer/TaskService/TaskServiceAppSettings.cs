namespace TaskService;

public class TaskServiceAppSettings
{
    public class MongoTaskRepositorySettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public MongoTaskRepositorySettings MongoTaskRepository { get; set; }
}