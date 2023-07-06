using ServerAPI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;

namespace UI
{
    public class MainMenuController : UIBehaviour
    {
        [SerializeField] private TMP_Text _greetingsText;
        [SerializeField] private Button _tutorialButton;
        [SerializeField] private Button _inGameButton;
        [SerializeField] private Button _signOut;

        protected override async void Start()
        {
            _greetingsText.text = $"Доступ получен: <color=green>Cat";
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
            SceneManager.LoadSceneAsync("Features/Authorization/AuhtorizationScene");
        }
    }
}