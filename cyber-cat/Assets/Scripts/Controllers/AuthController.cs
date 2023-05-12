using System;
using AuthService;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controllers
{
    public class AuthController : UIBehaviour
    {
        [SerializeField] private TMP_InputField _loginTextField;
        [SerializeField] private TMP_InputField _passwordTextField;
        [SerializeField] private TMP_Text _errorMessage;
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
                var login = _loginTextField.text;
                var password = _passwordTextField.text;
                var token = await _authService.Authenticate(login, password);
                var player = await _authService.AuthorizeAsPlayer(token);

                _localStorage.Player = player;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                _errorMessage.text = e.ToString();
                return;
            }

            SceneManager.LoadSceneAsync("MainMenu");
        }
    }
}