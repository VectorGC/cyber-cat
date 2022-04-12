using System;
using Cysharp.Threading.Tasks;
using TasksData;

public interface ITaskUnit
{
    UniTask<ITaskData> GetTask(string token, IProgress<float> progress = null);
    UniTask<bool?> IsTaskSolved(string token, IProgress<float> progress = null);
}