using AuthService;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Controllers
{
    public class AuthController : UIBehaviour
    {
        [SerializeField] private TMP_InputField _loginTextField;
        [SerializeField] private TMP_InputField _passwordTextField;

        [SerializeField] private Button _signIn;

        private IAuthService _authService = GameManager.Instance.AuthService;

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
            var login = _loginTextField.text;
            var password = _passwordTextField.text;

            var token = await _authService.Authenticate(login, password);

            PlayerPrefs.SetString("token", token.Value);
            PlayerPrefs.Save();
        }
    }
}