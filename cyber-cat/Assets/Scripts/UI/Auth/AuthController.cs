using System;
using AuthService;
using Services;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class AuthController : UIBehaviour
    {
        [SerializeField] private TextField _emailTextField;
        [SerializeField] private TextField _passwordTextField;
        [SerializeField] private TextField _errorMessage;
        [SerializeField] private Button _signIn;

        private IAuthService _authService;
        private ILocalStorageService _localStorage;

        protected override void Start()
        {
            _authService = GameManager.Instance.AuthService;
            _localStorage = GameManager.Instance.LocalStorage;
        }

        protected override void OnEnable()
        {
            _signIn.onClick.AddListener(SignIn);
        }

        protected override void OnDisable()
        {
            _signIn.onClick.RemoveListener(SignIn);
        }

        private async void SignIn()
        {
            try
            {
                var email = _emailTextField.Text;
                var password = _passwordTextField.Text;
                var token = await _authService.Authenticate(email, password);
                var player = await _authService.AuthorizePlayer(token);

                _localStorage.Player = player;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                _errorMessage.Text = e.Message;
                return;
            }

            SceneManager.LoadSceneAsync("MainMenu");
        }
    }
}