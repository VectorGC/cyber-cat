using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Observers
{
    public class AsyncOperationObservable<T> : IObservable<T> where T : AsyncOperation
    {
        private readonly List<IObserver<T>> _observers =
            new List<IObserver<T>>();

        public AsyncOperationObservable(T asyncOperation)
        {
            ObserveOperation(asyncOperation).StartCoroutine();
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            _observers.Add(observer);
            return new Unsubscriber<T>(_observers, observer);
        }

        private IEnumerator ObserveOperation(T asyncOperation)
        {
            do
            {
                _observers.ForEach(observer => observer.OnNext(asyncOperation));
                yield return null;
            } while (!asyncOperation.isDone);

            CallCompleted(asyncOperation);
        }

        private void CallCompleted(T asyncOperation)
        {
            _observers.ForEach(observer => OnCompleted(asyncOperation, observer));
        }

        protected virtual void OnCompleted(T asyncOperation, IObserver<T> observer)
        {
            observer.OnCompleted();
        }
    }
}