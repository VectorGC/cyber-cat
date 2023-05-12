using Services;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : UIBehaviour
    {
        [SerializeField] private TextMeshPro_TextShared _greetingsText;
        [SerializeField] private Button _tutorialButton;
        [SerializeField] private Button _inGameButton;
        [SerializeField] private Button _signOut;

        private ILocalStorageService _localStorage;

        protected override void Start()
        {
            _localStorage = GameManager.Instance.LocalStorage;

            var playerName = _localStorage.Player.Name;
            _greetingsText.text = $"Доступ получен: <color=green>{playerName}";
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
            SceneManager.LoadSceneAsync("Game");
        }

        private void SignOut()
        {
            _localStorage.RemoveAll();

            SceneManager.LoadSceneAsync("AuthScene");
        }
    }
}