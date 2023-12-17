using ApiGateway.Client.Application;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class SettingsController : UIBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _warningSaveIcon;

    private AuthorizationPresenter _authorizationPresenter;
    private ApiGatewayClient _client;

    [Inject]
    public void Construct(PlayerInventory playerInventory, AuthorizationPresenter authorizationPresenter, ApiGatewayClient client)
    {
        _client = client;
        _authorizationPresenter = authorizationPresenter;
        _button.onClick.AddListener(OnSettingsClick);
    }

    protected override void OnDestroy()
    {
        _button.onClick.RemoveListener(OnSettingsClick);
    }

    private void OnSettingsClick()
    {
        if (_client.Player == null)
        {
            SimpleModalWindow.Create()
                .SetHeader("Настройки")
                .SetBody("Доступ ограничен. Ваш прогресс может быть утерян. Авторизуйтесь для сохранения прогресса.")
                .AddButton("Продолжить")
                .AddButton("Авторизоваться", () => _authorizationPresenter.Show().Forget())
                .AddButton("Выйти в главное меню", () => SceneManager.LoadSceneAsync("MainMenu"))
                .Show();
        }
        else
        {
            SimpleModalWindow.Create()
                .SetHeader("Настройки")
                .SetBody($"Доступ получен: {_client.Player.User.FirstName}. Ваш прогресс сохраняется.")
                .AddButton("Продолжить")
                .AddButton("Выйти в главное меню", () => SceneManager.LoadSceneAsync("MainMenu"))
                .Show();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Player.CanInput)
        {
            OnSettingsClick();
        }

        _warningSaveIcon.gameObject.SetActive(_client.Player == null);
    }
}