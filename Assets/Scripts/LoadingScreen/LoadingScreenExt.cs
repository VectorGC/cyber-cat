using System;
using UniRx;
using UnityEngine;

public static class LoadingScreenExt
{
    public static IDisposable ViaLoadingScreen(this IObservable<float> progressObservable)
    {
        return progressObservable.Subscribe(LoadingScreen.Instance);
    }

    public static IDisposable ViaLoadingScreen(this AsyncOperation asyncOperation,
        ScheduledNotifier<float> progress = null) =>
        asyncOperation.ViaLoadingScreenObservable(progress).Subscribe();
    
    public static IObservable<AsyncOperation> ViaLoadingScreenObservable(this AsyncOperation asyncOperation,
        ScheduledNotifier<float> progress = null) =>
        LoadingScreen.Instance.ViaLoadingScreen(asyncOperation, progress);
}