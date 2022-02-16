using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CircleProgressBar : MonoBehaviour, IObserver<AsyncOperation>
    {
        [SerializeField] private Image circleImage;

        private void Start()
        {
            circleImage.gameObject.SetActive(false);
        }

        public void Observe(IObservable<AsyncOperation> asyncOperationObservable)
        {
            asyncOperationObservable.Subscribe(this);
        }

        public void OnCompleted()
        {
            circleImage.gameObject.SetActive(false);
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(AsyncOperation asyncOperation)
        {
            var progress = asyncOperation.progress;
            circleImage.fillAmount = progress;

            circleImage.gameObject.SetActive(true);
        }
    }
}