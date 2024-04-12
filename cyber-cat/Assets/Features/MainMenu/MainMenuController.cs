using ApiGateway.Client.Application;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Zenject;
using Button = UnityEngine.UI.Button;

namespace UI
{
    public class MainMenuController : UIBehaviour
    {
        [SerializeField] private Button _tutorialButton;
        [SerializeField] private Button _inGameButton;
        [SerializeField] private Button _signOut;

        private ApiGatewayClient _client;
        private AuthorizationPresenter _authorizationPresenter;

        [Inject]
        private void Construct(ApiGatewayClient client, AuthorizationPresenter authorizationPresenter)
        {
            _authorizationPresenter = authorizationPresenter;
            _client = client;
        }

        protected override void OnEnable()
        {
            _tutorialButton.onClick.AddListener(LoadTutorial);
            _inGameButton.onClick.AddListener(LoadGame);
            _signOut.onClick.AddListener(SignOut);
        }

        protected override void OnDisable()
        {
            _tutorialButton.onClick.RemoveListener(LoadTutorial);
            _inGameButton.onClick.RemoveListener(LoadGame);
            _signOut.onClick.RemoveListener(SignOut);
        }

        private void LoadTutorial()
        {
            SceneManager.LoadSceneAsync("Tutorial");
        }

        private void LoadGame()
        {
            if (_client.Player == null)
            {
                SimpleModalWindow.Create()
                    .SetHeader("Внимание")
                    .SetBody("Вы не авторизованы. Ваш прогресс не будет сохраняться. При обновлении страницы брузера - вы потеряете прогрес. Чтобы не потерять прогрес авторизуйтесь." +
                             "\nВы так же можете сделать это позже, нажав на шестиренку в правом верхнем углу экрана")
                    .AddButton("Авторизоваться", () => _authorizationPresenter.Show().Forget())
                    .AddButton("Продолжить анонимно", () => SceneManager.LoadSceneAsync("Game"))
                    .Show();
            }
            else
            {
                SceneManager.LoadSceneAsync("Game");
            }
        }

        private void SignOut()
        {
            Application.Quit();
        }
    }
}