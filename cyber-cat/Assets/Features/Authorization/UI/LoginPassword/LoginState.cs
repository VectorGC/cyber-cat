using ApiGateway.Client.Application;
using Cysharp.Threading.Tasks;
using UniMob;

public class LoginState : IAuthorizationState, ISecondAuthorizationButton
{
    [Atom] public string Error { get; private set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public Lifetime Lifetime => _widget.Lifetime;
    public string EmailInputLabel => "Email";
    public string ButtonText => "Войти";
    public string Title => "Войти в учетную запись";
    public string SecondButtonText => "Зарегестрироваться";


    private readonly ApiGatewayClient _client;
    private readonly AuthorizationWidget _widget;

    public LoginState(ApiGatewayClient client, AuthorizationWidget widget)
    {
        _widget = widget;
        _client = client;
    }

    public void OnButtonClicked()
    {
        Login().Forget();
    }

    private async UniTaskVoid Login()
    {
        var result = await _client.LoginPlayer(Email, Password);
        if (!result.IsSuccess)
        {
            Error = result.Error;
            return;
        }

        await AuthorizationWidget.SaveDataToPlayerPrefs(Email, Password);
        _widget.Hide();
    }

    public void OnSecondButtonClicked()
    {
        _widget.SwitchToRegisterState();
    }
}