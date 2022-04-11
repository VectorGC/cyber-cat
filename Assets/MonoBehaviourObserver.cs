using System;
using UnityEngine;

public abstract class MonoBehaviourObserver<T> : MonoBehaviour, IObserver<T>
{
    public abstract void OnCompleted();
    public abstract void OnError(Exception error);
    public abstract void OnNext(T value);
}