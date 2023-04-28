using Repositories.TaskRepositories;

public class GameManager
{
    public readonly ITaskRepository TaskRepository = FeatureFlags.UseMockTaskRepository
        ? (ITaskRepository) new MockTaskRepository()
        : new TaskRepositoryRestProxy();

    // TODO: Удали это, после рефаторинга айдишников задач.
    public static string GetTaskIdFromUnitAndTask(string unit, string task)
    {
        // Конвертирование составного айдишника в один какой-то.
        return $"{unit}{task}";
    }

    public static GameManager Instance
    {
        get
        {
            _instance ??= new GameManager();
            return _instance;
        }
    }

    private static GameManager _instance;
}