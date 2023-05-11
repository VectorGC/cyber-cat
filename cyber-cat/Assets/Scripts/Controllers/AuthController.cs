using System;
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

        protected override void OnEnable()
        {
            _signIn.onClick.AddListener(SignIn);
        }

        protected override void OnDisable()
        {
            _signIn.onClick.RemoveListener(SignIn);
        }
        
        public void Construct(IAuthService authService)
        {
            throw new NotImplementedException();
        }

        private async void SignIn()
        {
            var login = _loginTextField.text;
            var password = _passwordTextField.text;

            //var token = await GameManager.Instance.AuthService.Authenticate(login, password);
            //PlayerPrefs.SetString("token", token.Value);
            //PlayerPrefs.Save();
        }
    }
}