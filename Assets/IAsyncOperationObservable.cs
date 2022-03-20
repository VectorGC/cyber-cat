using System;
using UnityEngine;

public interface IAsyncOperationObservable : IObservable<AsyncOperation>, IObservable<float>
{
    IDisposable Subscribe();
}