using ApiGateway.Client.Application;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class SettingsController : UIBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Toggle _musicToggle;
    [SerializeField] private Image _warningSaveIcon;

    private AuthorizationPresenter _authorizationPresenter;
    private ApiGatewayClient _client;
    private MusicAudioManager _musicAudioManager;

    [Inject]
    public void Construct(PlayerInventory playerInventory, AuthorizationPresenter authorizationPresenter, ApiGatewayClient client, MusicAudioManager musicAudioManager)
    {
        _musicAudioManager = musicAudioManager;
        _client = client;
        _authorizationPresenter = authorizationPresenter;
        _button.onClick.AddListener(OnSettingsClick);
        _musicToggle.onValueChanged.AddListener(OnMusicToggled);
    }

    protected override void OnDestroy()
    {
        _button.onClick.RemoveListener(OnSettingsClick);
        _musicToggle.onValueChanged.RemoveListener(OnMusicToggled);
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

    private void OnMusicToggled(bool value)
    {
        _musicAudioManager.SetActiveMusic(value);
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