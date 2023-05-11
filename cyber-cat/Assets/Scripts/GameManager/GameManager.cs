using AuthService;
using Repositories.TaskRepositories;
using Services.AuthService;

public class GameManager
{
    public readonly ITaskRepository TaskRepository = new TaskRepositoryRestProxy(RestAPIFacade.Create());
    public readonly IAuthService AuthService = new AuthServiceRestProxy(RestAPIFacade.Create());

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