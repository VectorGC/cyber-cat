using AuthService;
using Repositories.TaskRepositories;
using ServerAPI;
using Services;
using Services.AuthService;

public class GameManager
{
    public readonly ITaskRepository TaskRepository = new TaskRepositoryProxy(RestAPIFacade.Create());
    public readonly IAuthService AuthService = new AuthServiceProxy(RestAPIFacade.Create());
    public readonly ILocalStorageService LocalStorage = new PlayerPrefsStorage();

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