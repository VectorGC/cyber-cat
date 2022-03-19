using System;
using UnityEngine;

namespace MonoBehaviours
{
    public class PauseTimeScaleObserver<T> : IObserver<T>
    {
        public void OnCompleted()
        {
            Time.timeScale = 1f;
        }

        public void OnError(Exception error)
        {
            Time.timeScale = 1f;
        }

        public void OnNext(T value)
        {
            Time.timeScale = 0f;
        }
    }
}