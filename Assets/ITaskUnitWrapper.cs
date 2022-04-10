using System;
using Cysharp.Threading.Tasks;
using TasksData;

public interface ITaskUnit : IObservable<ITaskData>
{
    UniTask<ITaskData> GetTask(IProgress<float> progress = null);
    UniTask<bool> IsTaskSolved(IProgress<float> progress = null);
    UniTask CallTaskChanged();
}