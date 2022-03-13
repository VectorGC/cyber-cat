using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour, IObserver<float>, IObserver<AsyncOperation>, IObservable<float>
{
    [SerializeField] private Slider loadingBar;

    private readonly Subject<float> _subject = new Subject<float>();

    private ScheduledNotifier<float> _progress;
    private IDisposable _progressUnsubscriber;

    public static LoadingScreen Instance => UIDialogs.Instance.LoadingScreen;

    public void Awake()
    {
        Disable();
    }

    public IObservable<AsyncOperation> ViaLoadingScreen(AsyncOperation asyncOperation,
        ScheduledNotifier<float> progress = null)
    {
        _progress = progress ?? new ScheduledNotifier<float>();
        _progressUnsubscriber = _progress.Subscribe(this);

        return asyncOperation.AsAsyncOperationObservable(_progress).Do(this);
    }

    private void UpdateProgress(float progressValue)
    {
        loadingBar.value = progressValue;
        if (gameObject)
        {
            gameObject.SetActive(true);
        }
    }

    public IDisposable Subscribe(IObserver<float> observer) => _subject.Subscribe(observer);

    private void OnDisable() => _progressUnsubscriber?.Dispose();

    private void Disable() => gameObject.SetActive(false);

    public void OnCompleted()
    {
        Disable();
        _subject.OnCompleted();
    }

    public void OnError(Exception error)
    {
        Disable();
        _subject.OnError(error);
    }

    public void OnNext(float value)
    {
        UpdateProgress(value);
        _subject.OnNext(value);

        if (value >= 1)
        {
            OnCompleted();
        }
    }

    public void OnNext(AsyncOperation value)
    {
    }
}