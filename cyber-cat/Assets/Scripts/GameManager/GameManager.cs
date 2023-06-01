using ApiGateway.Client;
using AuthService;
using Repositories.TaskRepositories;
using ServerAPI;
using Services;
using Services.AuthService;

public class GameManager
{
    public readonly ILocalStorageService LocalStorage = new PlayerPrefsStorage();
    public readonly ITaskRepository TaskRepository;
    public readonly IAuthService AuthService;

    private readonly IClient _serverClient;

    private GameManager()
    {
        _serverClient = ServerAPIFacade.Create();
        var token = LocalStorage.Player?.Token.Value;
        if (!string.IsNullOrEmpty(token))
        {
            _serverClient.AddAuthorizationToken(token);
        }

        TaskRepository = new TaskRepositoryProxy(_serverClient);
        AuthService = new AuthServiceProxy(_serverClient);
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