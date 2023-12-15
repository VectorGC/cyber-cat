using System;
using ApiGateway.Client.Application;
using Cysharp.Threading.Tasks;
using Shared.Models.Infrastructure;
using UniMob;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class AuthorizationWidget : UIBehaviour, ILifetimeScope
{
    [SerializeField] private Text _title;
    [SerializeField] private Text _errorText;
    [SerializeField] private InputField _emailInputField;
    [SerializeField] private InputField _userNameInputField;
    [SerializeField] private GameObject _userNameInputFieldGo;
    [SerializeField] private InputField _passwordInputField;
    [SerializeField] private InputField _confrimPasswordInputField;
    [SerializeField] private GameObject _confrimPasswordInputFieldGo;
    [SerializeField] private SimpleButton _button1;
    [SerializeField] private SimpleButton _button2;
    [SerializeField] private Button _closeButton;

    [Atom] public IAuthorizationState State { get; set; }

    public Lifetime Lifetime => _lifetimeController.Lifetime;

    private readonly LifetimeController _lifetimeController = new LifetimeController();
    private ApiGatewayClient _client;
    private AuthorizationPresenter _presenter;

    [Inject]
    public async void Constructor(ApiGatewayClient client, AuthorizationPresenter presenter)
    {
        _presenter = presenter;
        _client = client;

        _emailInputField.onValueChanged.AddListener(text => State.Email = text);
        _userNameInputField.onValueChanged.AddListener(text =>
        {
            if (State is IUserNameInput userNameInput)
                userNameInput.UserName = text;
        });
        _passwordInputField.onValueChanged.AddListener(text => State.Password = text);
        _confrimPasswordInputField.onValueChanged.AddListener(text =>
        {
            if (State is IConfirmedPasswordInput confirmedPasswordInput)
                confirmedPasswordInput.ConfirmedPassword = text;
        });

        _button1.OnClick += () => State.OnButtonClicked();
        _button2.OnClick += () => (State as ISecondAuthorizationButton)?.OnSecondButtonClicked();
        _closeButton.onClick.AddListener(Hide);

        SwitchToLoginState();

        var (email, password) = await GetDataFromPlayerPrefs();
        _emailInputField.text = email;
        _passwordInputField.text = password;
    }

    protected override void OnDestroy()
    {
        _emailInputField.onValueChanged.RemoveAllListeners();
        _userNameInputField.onValueChanged.RemoveAllListeners();
        _passwordInputField.onValueChanged.RemoveAllListeners();
        _confrimPasswordInputField.onValueChanged.RemoveAllListeners();
        _button1.UnsubscribeAll();
        _button2.UnsubscribeAll();
        _closeButton.onClick.RemoveListener(Hide);

        _lifetimeController.Dispose();
    }

    private static async UniTask<(string email, string password)> GetDataFromPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey("email") || !PlayerPrefs.HasKey("keycode"))
            return (string.Empty, string.Empty);

        var email = PlayerPrefs.GetString("email");
        var cryptedPassword = PlayerPrefs.GetString("keycode");
        var password = await Crypto.DecryptAsync(cryptedPassword, "cryptedPassword");
        return (email, password);
    }

    public static async UniTask SaveDataToPlayerPrefs(string email, string password)
    {
        PlayerPrefs.SetString("email", email);
        var cryptedPassword = await Crypto.EncryptAsync(password, "cryptedPassword");
        PlayerPrefs.SetString("keycode", cryptedPassword);
    }

    protected override void Start()
    {
        Atom.Reaction(Lifetime, OnUpdate, debugName: $"{GetType().Name}.{nameof(OnUpdate)}");
    }

    public void Hide()
    {
        _presenter.Hide().Forget();
    }

    private void OnUpdate()
    {
        if (State == null)
            return;

        _title.text = State.Title;
        _errorText.text = State.Error;
        _button1.Text = State.ButtonText;

        var secondButton = State as ISecondAuthorizationButton;
        _button2.gameObject.SetActive(secondButton != null);
        _button2.Text = secondButton?.SecondButtonText;

        _confrimPasswordInputFieldGo.SetActive(State is IConfirmedPasswordInput);
        _userNameInputFieldGo.SetActive(State is IUserNameInput);
    }

    public void SwitchToLoginState()
    {
        State = new LoginState(_client, this);
    }

    public void SwitchToRegisterState()
    {
        State = new RegisterAccountState(_client, this);
    }
}