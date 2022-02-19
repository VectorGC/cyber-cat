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

        private readonly T _asyncOperation;

        public AsyncOperationObservable(T asyncOperation)
        {
            _asyncOperation = asyncOperation;
            ObserveOperation(asyncOperation).StartCoroutine();
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            _observers.Add(observer);
            if (_asyncOperation.isDone)
            {
                CallOnNext(_asyncOperation);
                CallCompleted(_asyncOperation);
            }

            return new Unsubscriber<T>(_observers, observer);
        }

        private IEnumerator ObserveOperation(T asyncOperation)
        {
            do
            {
                CallOnNext(asyncOperation);
                yield return null;
            } while (!asyncOperation.isDone);

            CallCompleted(asyncOperation);
        }

        private void CallOnNext(T asyncOperation)
        {
            _observers.ForEach(observer => observer.OnNext(asyncOperation));
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