using AuthService;
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
        var serverClient = ServerAPIFacade.Create();
        var token = LocalStorage.Player?.Token.Value;
        if (!string.IsNullOrEmpty(token))
        {
            serverClient.AddAuthorizationToken(token);
        }

        TaskRepository = new TaskRepositoryProxy(serverClient);
        AuthService = new AuthServiceProxy(serverClient);
        CodeEditor = new CodeEditorServiceProxy(serverClient, TaskRepository);
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