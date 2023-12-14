using ApiGateway.Client.Application;
using Cysharp.Threading.Tasks;
using UniMob;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public interface IAuthorizationState : ILifetimeScope
{
    string Error { get; }
}

public class LoginState : IAuthorizationState
{
    [Atom] public string Email { get; set; } = string.Empty;
    [Atom] public string Password { get; set; } = string.Empty;
    [Atom] public string Error { get; set; }

    public Lifetime Lifetime { get; }

    private readonly ApiGatewayClient _client;

    public LoginState(ApiGatewayClient client, Lifetime lifetime)
    {
        _client = client;
        Lifetime = lifetime;
    }

    public async UniTaskVoid Login()
    {
        var result = await _client.LoginPlayer(Email, "213knuiqbhwe");
        if (!result.IsSuccess)
            Error = result.Error;
    }
}

public class AuthorizationPresenter
{
    public async UniTaskVoid Show()
    {
        await SceneManager.LoadSceneAsync("AuthorizationScene", LoadSceneMode.Additive);
    }

    public async UniTaskVoid Hide()
    {
        await SceneManager.UnloadSceneAsync("AuthorizationScene").ToUniTask();
    }
}

public class AuthorizationWidget : UIBehaviour, ILifetimeScope
{
    [SerializeField] private Text _errorText;
    [SerializeField] private InputField _emailInputField;
    [SerializeField] private Button _login;
    [SerializeField] private string _test;

    [Atom] private IAuthorizationState State { get; set; }

    public Lifetime Lifetime => _lifetimeController.Lifetime;

    private readonly LifetimeController _lifetimeController = new LifetimeController();
    private ApiGatewayClient _client;

    [Inject]
    public void Constructor(ApiGatewayClient client)
    {
        _client = client;
        SwitchToLoginState();
    }

    protected override void Start()
    {
        Atom.Reaction(Lifetime, OnUpdate, debugName: $"{GetType().Name}.{nameof(OnUpdate)}");

        _emailInputField.onValueChanged.AddListener(email =>
        {
            if (State is LoginState loginState) loginState.Email = email;
        });
        _login.onClick.AddListener(() =>
        {
            if (State is LoginState loginState) loginState.Login().Forget();
        });
    }

    protected override void OnDestroy()
    {
        _lifetimeController.Dispose();

        _emailInputField.onValueChanged.RemoveAllListeners();
        _login.onClick.RemoveAllListeners();
    }

    private void OnUpdate()
    {
        if (State == null)
            return;

        _errorText.text = State.Error;
        if (State is LoginState loginState) _test = loginState.Email;
    }

    private void SwitchToLoginState()
    {
        State = new LoginState(_client, Lifetime);
    }
}