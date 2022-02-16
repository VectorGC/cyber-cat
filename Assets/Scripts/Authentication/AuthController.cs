using Authentication.Receivers;
using Authentication.Requests;
using TMPro;
using UI;
using UnityEngine;

namespace Authentication
{
    public class AuthController : MonoBehaviour
    {
        [SerializeField] private AuthenticationRequest authenticationRequest;
        [SerializeField] private AuthenticationReceiver authenticationReceiver;

        [SerializeField] private TMP_InputField loginTextField;
        [SerializeField] private TMP_InputField passwordTextField;

        [SerializeField] private CircleProgressBar progressBar;

        public void Authenticate()
        {
            var login = loginTextField.text;
            var password = passwordTextField.text;

            var data = new AuthenticationData(login, password);
            var webRequestObservable = authenticationRequest.Authenticate(data);

            progressBar.Observe(webRequestObservable);
            authenticationReceiver.Subscribe(webRequestObservable);
        }
    }
}