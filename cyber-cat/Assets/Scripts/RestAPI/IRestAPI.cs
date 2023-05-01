using System;
using Cysharp.Threading.Tasks;

public interface IRestAPI
{
    UniTask<string> GetTasks(IProgress<float> progress = null);
}