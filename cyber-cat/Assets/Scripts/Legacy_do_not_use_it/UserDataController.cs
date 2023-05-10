using TMPro;
using UnityEngine;
using RestAPIWrapper;

namespace Authentication
{
    public class UserDataController : MonoBehaviour
    {
        [SerializeField] private TMP_InputField loginTextField;
        [SerializeField] private TMP_InputField nameTextField;
        [SerializeField] private TMP_InputField passwordTextField;
        [SerializeField] private TMP_InputField passwordAgainTextField;

        private readonly UniRx.Diagnostics.Logger _logger = new UniRx.Diagnostics.Logger(nameof(UserDataController));

        public async void Register()
        {
            if (!IsValidInputData())
            {
                return;
            }

            var login = loginTextField.text;
            var password = passwordTextField.text;
            var userName = nameTextField.text;

            await RestAPI.Instance.RegisterUser(login, password, userName);

            _logger.Log("Вам на почту пришло сообщение с подтверждением!");
        }

        public async void Restore()
        {
            if (!IsValidInputData())
            {
                return;
            }

            var login = loginTextField.text;
            var password = passwordTextField.text;

            await RestAPI.Instance.RestorePassword(login, password);

            _logger.Log("Вам на почту пришло сообщение с подтверждением!");
        }

        private bool IsValidInputData()
        {
            if (loginTextField.IsNullOrEmpty())
            {
                _logger.Error("Введите почтовый адрес");
                return false;
            }

            if (nameTextField && nameTextField.IsNullOrEmpty())
            {
                _logger.Error("Введите ваше имя");
                return false;
            }

            if (passwordTextField.IsNullOrEmpty())
            {
                _logger.Error("Введите пароль");
                return false;
            }

            if (passwordAgainTextField.text != passwordTextField.text)
            {
                _logger.Error("Пароли не совпадают");
                return false;
            }

            return true;
        }
    }
}