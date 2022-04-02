using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Authentication
{
    public class RegisterController : MonoBehaviour
    {
        [SerializeField] private TMP_InputField loginTextField;
        [SerializeField] private TMP_InputField nameTextField;
        [SerializeField] private TMP_InputField passwordTextField;
        [SerializeField] private TMP_InputField passwordAgainTextField;
        [SerializeField] private ErrorMessageView errorText;

        [SerializeField] private UnityEvent onComplete;

        public async void Register()
        {
            if (!LoginIsOk())
            {
                string error = "Login is not OK!";
                errorText.OnError(new RequestTokenException(error));
                return;
            }
            if (!NameIsOk())
            {
                string error = "Name is not OK!";
                errorText.OnError(new RequestTokenException(error));
                return;
            }
            if (!PasswordIsOK())
            {
                string error = "Password is not OK!";
                errorText.OnError(new RequestTokenException(error));
                return;
            }
            if (passwordAgainTextField.text != passwordTextField.text)
            {
                string error = "Passwords are not equal!";
                errorText.OnError(new RequestTokenException(error));
                return;
            }

            var login = loginTextField.text;
            var password = passwordTextField.text;
            var name = nameTextField.text;

            await TokenSession.Register(login, password, name);
            onComplete.Invoke();
        }

        private bool LoginIsOk()
        {
            return !string.IsNullOrWhiteSpace(loginTextField.text);
        }

        private bool PasswordIsOK()
        {
            return !string.IsNullOrWhiteSpace(passwordAgainTextField.text);
        }

        private bool NameIsOk()
        {
            return !string.IsNullOrWhiteSpace(nameTextField.text);
        }
    }
}