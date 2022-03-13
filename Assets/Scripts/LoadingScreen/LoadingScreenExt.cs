using System;
using UniRx;
using UnityEngine;

public static class LoadingScreenExt
{
    public static IObservable<float> ViaLoadingScreen(this IObservable<float> progressObservable)
    {
        progressObservable.Subscribe(LoadingScreen.Instance);
        return LoadingScreen.Instance;
    }

    public static IObservable<AsyncOperation> ViaLoadingScreenObservable(this AsyncOperation asyncOperation,
        ScheduledNotifier<float> progress = null) =>
        LoadingScreen.Instance.ViaLoadingScreen(asyncOperation, progress);

    public static IDisposable ViaLoadingScreen(this IObservable<AsyncOperation> asyncOperationObservable,
        ScheduledNotifier<float> progress = null) =>
        asyncOperationObservable.Do(LoadingScreen.Instance).Subscribe();
}