using Cysharp.Threading.Tasks;
using System;

namespace ServerAPIBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1">Data type</typeparam>
    /// <typeparam name="T2">Request return type</typeparam>
    public interface IWebApiRequester<T1, T2>
    {
        void Request(T1 data, Action<T2> callback);
        UniTask<T2> RequestAsync(T1 data, IProgress<float> progress = null);
    }
}