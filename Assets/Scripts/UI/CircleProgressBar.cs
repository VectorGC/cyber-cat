using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CircleProgressBar : MonoBehaviour, IObserver<float>
    {
        [SerializeField] private Image circleImage;

        private IObservable<float> _progressHandler;

        private void Construct(IObservable<float> progressHandler)
        {
            _progressHandler = progressHandler;
            progressHandler.Subscribe(this);
        }

        private void OnDestroy()
        {
            if (_progressHandler is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        public void OnCompleted()
        {
            Debug.Log("Progress operation completed");
            circleImage.gameObject.SetActive(false);
        }

        public void OnError(Exception error)
        {
            Debug.LogError($"Progress operation ended with error '{error}'");
            circleImage.gameObject.SetActive(false);
        }

        public void OnNext(float value)
        {
            Debug.Log($"Progress changed to {value:P0}");

            circleImage.fillAmount = value;
            circleImage.gameObject.SetActive(true);
        }
    }
}