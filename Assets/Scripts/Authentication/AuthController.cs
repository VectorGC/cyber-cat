using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEventTypes;

namespace Authentication
{
    public class AuthController : MonoBehaviour, IObservable<Exception>
    {
        [SerializeField] private TMP_InputField loginTextField;
        [SerializeField] private TMP_InputField passwordTextField;

        [SerializeField] private UnityStringEvent OnComplete;

        private readonly Subject<Exception> _exceptionSubject = new Subject<Exception>();

        public void Authenticate()
        {
            var login = loginTextField.text;
            var password = passwordTextField.text;

            TokenSession.ReceiveFromServer(login, password)
                .DoOnError(e => _exceptionSubject.OnNext(e))
                .Do(token => OnComplete.Invoke(token))
                .Subscribe();
        }

        public IDisposable Subscribe(IObserver<Exception> observer) => _exceptionSubject.Subscribe(observer);
    }
}