using System;
using AuthService;
using Cysharp.Threading.Tasks;
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

        protected override void Start()
        {
            _authService = GameManager.Instance.AuthService;
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
            ITokenSession token;
            try
            {
                var login = _loginTextField.text;
                var password = _passwordTextField.text;
                token = await _authService.Authenticate(login, password);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                _errorMessage.text = e.ToString();
                return;
            }

            PlayerPrefs.SetString("token", token.Value);
            PlayerPrefs.Save();

            SceneManager.LoadSceneAsync("MainMenu").ToUniTask().Forget();
        }
    }
}