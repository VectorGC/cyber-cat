using System;
using Cysharp.Threading.Tasks;
using TasksData;

public interface ITaskUnit
{
    UniTask<ITaskData> GetTask(string token, IProgress<float> progress = null);
}