using System;
using Observers;
using UI;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace DefaultNamespace.TestButtons
{
    public class AuthenticationForm : MonoBehaviour, IObserver<UnityWebRequestAsyncOperation>
    {
        [SerializeField] private InputField loginTextField;
        [SerializeField] private InputField passwordTextField;

        [SerializeField] private CircleProgressBar progressBar;

        [SerializeField] private AuthenticationService authenticationProvider;

        private UnityWebRequestAsyncOperation _receivedOperation;

        public void Authenticate()
        {
            var login = loginTextField.text;
            var password = passwordTextField.text;

            var data = new AuthenticationData(login, password);
            var webRequestObservable = authenticationProvider.Authenticate(data);

            var asyncRequestProgressObserver =
                progressBar.ToConvertObserver<UnityWebRequestAsyncOperation, float>(source => source.progress);

            webRequestObservable.Subscribe(asyncRequestProgressObserver);
            webRequestObservable.Subscribe(this);
        }

        public void OnCompleted()
        {
            Debug.Log($"Completed Request: {_receivedOperation.webRequest.downloadHandler.text}");
        }

        public void OnError(Exception error)
        {
            Debug.LogError($"Error Request: {error}");
        }

        public void OnNext(UnityWebRequestAsyncOperation receivedOperation)
        {
            _receivedOperation = receivedOperation;
        }
    }
}