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
    string Title { get; }
    string Error { get; }
    void OnEnter();
    void OnExit();
}

public class LoginState : IAuthorizationState
{
    [Atom] public string Email { get; set; } = string.Empty;
    [Atom] public string Password { get; set; } = string.Empty;
    [Atom] public string Error { get; private set; }

    public Lifetime Lifetime => _widget.Lifetime;
    public string Title => "Войти в учетную запись";

    private readonly ApiGatewayClient _client;
    private readonly AuthorizationWidget _widget;

    public LoginState(ApiGatewayClient client, AuthorizationWidget widget)
    {
        _widget = widget;
        _client = client;
    }

    public async UniTaskVoid Login()
    {
        var result = await _client.LoginPlayer(Email, "213knuiqbhwe");
        if (!result.IsSuccess)
            Error = result.Error;
    }

    public void OnEnter()
    {
        _widget.FirstInputField.onValueChanged.AddListener(text => Email = text);

        _widget.Button1.Click += () => Login().Forget();
        _widget.Button2.Click += () => _widget.SwitchToRegisterState();

        _widget.Button1.Label = "Войти";
        _widget.Button2.Label = "Зарегестироваться";
    }

    public void OnExit()
    {
        _widget.FirstInputField.onValueChanged.RemoveAllListeners();
        _widget.Button1.UnsubscribeAll();
        _widget.Button2.UnsubscribeAll();
    }
}

public class RegisterAccountState : IAuthorizationState
{
    [Atom] public string Email { get; set; } = string.Empty;
    [Atom] public string Password { get; set; } = string.Empty;
    [Atom] public string UserName { get; set; } = string.Empty;
    [Atom] public string Error { get; private set; }

    public string Title => "Создание учетной записи";
    public Lifetime Lifetime => _widget.Lifetime;

    private readonly ApiGatewayClient _client;
    private readonly AuthorizationWidget _widget;

    public RegisterAccountState(ApiGatewayClient client, AuthorizationWidget widget)
    {
        _widget = widget;
        _client = client;
    }

    public async UniTaskVoid Register()
    {
        var result = await _client.RegisterPlayer(Email, "213knuiqbhwe", UserName);
        if (!result.IsSuccess)
            Error = result.Error;
    }

    public void OnEnter()
    {
        _widget.FirstInputField.onValueChanged.AddListener(text => Email = text);

        _widget.Button1.Click += () => Register().Forget();
        _widget.Button2.Click += () => _widget.SwitchToLoginState();

        _widget.Button1.Label = "Зарегестрироваться";
        _widget.Button2.Label = "Уже есть учетная запись?";
    }

    public void OnExit()
    {
        _widget.FirstInputField.onValueChanged.RemoveAllListeners();
        _widget.Button1.UnsubscribeAll();
        _widget.Button2.UnsubscribeAll();
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
    [SerializeField] private Text _title;
    [SerializeField] private Text _errorText;

    public Text FirstInputFieldLabel;
    public InputField FirstInputField;
    public SimpleButton Button1;
    public SimpleButton Button2;

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

    protected override void OnDestroy()
    {
        _lifetimeController.Dispose();

        _emailInputField.onValueChanged.RemoveAllListeners();
        _login.onClick.RemoveAllListeners();
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
        _createAccount.onClick.AddListener(() =>
        {
            switch (State)
            {
                case LoginState loginState:
                    SwitchToRegisterState();
                    break;
                case RegisterAccountState register:
                    register.Register().Forget();
                    break;
            }
        });
    }

    private void OnUpdate()
    {
        if (State == null)
            return;

        _title.text = State.Title;
        _errorText.text = State.Error;

        _button1.UnsubscribeAll();
        _button2.UnsubscribeAll();

        if (State.ButtonAction1 != null)
        {
            _button1.Clicked +=
                _login.onClick.AddListener(State.ButtonAction1);
            _login.
        }

        switch (State)
        {
            case LoginState loginState:
                _login.gameObject.SetActive(true);
                _createAccount.gameObject.SetActive(true);
                break;
            case RegisterAccountState registerAccountState:
                _login.gameObject.SetActive(false);
                break;
        }
    }

    public void SwitchToLoginState()
    {
        State = new LoginState(_client, Lifetime);
    }

    public void SwitchToRegisterState()
    {
        State = new RegisterAccountState(_client, Lifetime);
    }
}