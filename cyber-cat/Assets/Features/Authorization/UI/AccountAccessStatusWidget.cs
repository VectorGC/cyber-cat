using ApiGateway.Client.Application;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class AccountAccessStatusWidget : UIBehaviour
{
    [SerializeField] private Text _greetings;
    [SerializeField] private SimpleButton _button;

    private ApiGatewayClient _client;
    private AuthorizationPresenter _authorizationPresenter;

    [Inject]
    public void Construct(ApiGatewayClient client, AuthorizationPresenter authorizationPresenter)
    {
        _authorizationPresenter = authorizationPresenter;
        _client = client;
        _button.OnClick += OnClick;
    }

    protected override void OnDestroy()
    {
        _button.OnClick -= OnClick;
    }

    private void Update()
    {
        var textWidget = _greetings.W();
        if (_client.Player != null)
        {
            textWidget.Text = $"Доступ получен: {_client.Player.User.FirstName}";
            textWidget.Color = Color.green;
            _button.Text = "Выйти из учетной записи";
        }
        else
        {
            textWidget.Text = "Доступ ограничен";
            textWidget.Color = Color.red;
            _button.Text = "Авторизоваться";
        }
    }

    private void OnClick()
    {
        if (_client.Player != null)
        {
            _client.Player.Logout();
        }
        else
        {
            _authorizationPresenter.Show().Forget();
        }
    }
}