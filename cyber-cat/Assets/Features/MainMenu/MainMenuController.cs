using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;

namespace UI
{
    public class MainMenuController : UIBehaviour
    {
        [SerializeField] private Button _tutorialButton;
        [SerializeField] private Button _inGameButton;
        [SerializeField] private Button _signOut;

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
            Application.Quit();
        }
    }
}