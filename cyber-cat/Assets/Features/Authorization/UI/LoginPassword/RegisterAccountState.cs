using ApiGateway.Client.Application;
using Cysharp.Threading.Tasks;
using UniMob;

public class RegisterAccountState : IAuthorizationState, IConfirmedPasswordInput, IUserNameInput
{
    [Atom] public string Error { get; private set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmedPassword { get; set; }
    public string UserName { get; set; } = string.Empty;

    public string Title => "Создание учетной записи";
    public string EmailInputLabel => "Email";
    public string ButtonText => "Зарегестрироваться";
    public Lifetime Lifetime => _widget.Lifetime;

    private readonly ApiGatewayClient _client;
    private readonly AuthorizationWidget _widget;

    public RegisterAccountState(ApiGatewayClient client, AuthorizationWidget widget)
    {
        _widget = widget;
        _client = client;
    }

    private async UniTaskVoid Register()
    {
        var result = await _client.RegisterPlayer(Email, Password, UserName);
        if (!result.IsSuccess)
        {
            Error = result.Error;
            return;
        }

        // Try login
        _widget.SwitchToLoginState();
        var loginState = _widget.State as LoginState;
        loginState?.OnButtonClicked();
    }

    public void OnEmailInput(string text)
    {
        Email = text;
    }

    public void OnButtonClicked()
    {
        if (Password != ConfirmedPassword)
        {
            Error = "Пароли не совпадают";
            return;
        }

        Register().Forget();
    }
}