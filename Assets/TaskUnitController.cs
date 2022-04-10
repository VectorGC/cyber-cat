using System;
using Cysharp.Threading.Tasks;
using TasksData;
using UnityEngine;

public abstract class TaskUnitController : MonoBehaviour, ITaskUnit
{
    public abstract UniTask<bool> IsTaskSolved(IProgress<float> progress = null);

    public abstract UniTask CallTaskChanged();

    public abstract UniTask<ITaskData> GetTask(IProgress<float> progress = null);

    public abstract IDisposable Subscribe(IObserver<ITaskData> observer);

    public abstract void OpenCodeEditor();
}