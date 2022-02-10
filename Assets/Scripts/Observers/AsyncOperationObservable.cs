using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Observers
{
    public class AsyncOperationObservable<T> : IObservable<T>, IEnumerator where T : AsyncOperation
    {
        private readonly T _asyncOperation;

        private readonly List<IObserver<T>> _observers =
            new List<IObserver<T>>();

        private IEnumerator _asyncOperationRoutine;

        private IEnumerator AsyncOperationRoutine =>
            _asyncOperationRoutine ??= ObserveOperation();

        public AsyncOperationObservable(T asyncOperation)
        {
            _asyncOperation = asyncOperation;
            AsyncOperationRoutine.StartCoroutine();
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            _observers.Add(observer);

            if (_asyncOperation.isDone)
            {
                CallCompleted(_asyncOperation);
            }

            return new Unsubscriber<T>(_observers, observer);
        }

        public IEnumerator ObserveOperation()
        {
            var lastProgress = 0f;
            do
            {
                var currentProgress = _asyncOperation.progress;
                if (!Mathf.Approximately(currentProgress, lastProgress))
                {
                    lastProgress = currentProgress;
                    _observers.ForEach(observer => observer.OnNext(_asyncOperation));
                }

                yield return null;
            } while (!_asyncOperation.isDone);

            CallCompleted(_asyncOperation);
        }

        private void CallCompleted(T asyncOperation)
        {
            _observers.ForEach(observer => observer.OnNext(asyncOperation));
            _observers.ForEach(observer => CallCompleted(asyncOperation, observer));
        }

        protected virtual void CallCompleted(T asyncOperation, IObserver<T> observer)
        {
            if (asyncOperation.isDone)
            {
                observer.OnCompleted();
            }
        }

        public bool MoveNext()
        {
            return AsyncOperationRoutine.MoveNext();
        }

        public void Reset()
        {
            AsyncOperationRoutine.Reset();
        }

        public object Current => AsyncOperationRoutine.Current;
    }
}