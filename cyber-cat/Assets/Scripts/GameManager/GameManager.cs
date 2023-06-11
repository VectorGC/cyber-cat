using AuthService;
using Models;
using Repositories.TaskRepositories;
using ServerAPI;
using Services;
using Services.AuthService;

public class GameManager
{
    public ILocalStorageService LocalStorage { get; } = new PlayerPrefsStorage();
    public ITaskRepository TaskRepository { get; }
    public IAuthService AuthService { get; }
    public ICodeEditorService CodeEditor { get; }

    private GameManager()
    {
        var serverAPIClient = ServerAPIFacade.Create();
        AuthService = new AuthServiceProxy(serverAPIClient);

        LocalStorage.Player = Authorize(serverAPIClient);

        TaskRepository = new TaskRepositoryProxy(serverAPIClient);
        CodeEditor = new CodeEditorServiceProxy(serverAPIClient, TaskRepository);
    }

    private IPlayer Authorize(IServerAPI serverAPI)
    {
        var token = AuthService.Authenticate("cat", "cat").GetAwaiter().GetResult();
        serverAPI.AddAuthorizationToken(token.Value);
        var player = AuthService.AuthorizePlayer(token).GetAwaiter().GetResult();

        return player;
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