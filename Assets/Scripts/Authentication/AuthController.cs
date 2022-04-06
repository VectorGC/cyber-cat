using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Authentication
{
    public class AuthController : MonoBehaviour
    {
        [SerializeField] private TMP_InputField loginTextField;
        [SerializeField] private TMP_InputField passwordTextField;
        [SerializeField] private ErrorMessageView errorText;

        [SerializeField] private UnityEvent onComplete;

        public async void Authenticate()
        {
            var login = loginTextField.text;
            var password = passwordTextField.text;

            try
            {
                var tocken = await TokenSession.RequestAndSaveFromServer(login, password);
                onComplete.Invoke();
            }
            catch (RequestTokenException ex)
            {
                errorText.OnError(new RequestTokenException(ex.Message));
                errorText.SetBadColor();
            }
        }
    }
}