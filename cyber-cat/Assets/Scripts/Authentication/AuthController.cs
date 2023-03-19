using TMPro;
using UnityEngine;
using UnityEngine.Events;
using RequestAPI.Proxy;

namespace Authentication
{
    public class AuthController : MonoBehaviour
    {
        [SerializeField] private TMP_InputField loginTextField;
        [SerializeField] private TMP_InputField passwordTextField;

        [SerializeField] private UnityEvent onComplete;

        public async void Authenticate()
        {
            var login = loginTextField.text;
            var password = passwordTextField.text;

            await RequestAPIProxy.Authenticate(login, password);

            var isCartoonWatched = PlayerPrefs.GetInt("isCartoonWatched") == 1;
            if (!isCartoonWatched)
            {
                //await IntroCartoon.Play();
            }

            onComplete.Invoke();
        }
    }
}