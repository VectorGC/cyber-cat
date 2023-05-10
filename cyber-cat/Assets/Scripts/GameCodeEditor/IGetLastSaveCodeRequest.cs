using System;
using Cysharp.Threading.Tasks;

namespace RestAPIWrapper
{
    public interface IGetLastSaveCodeRequest
    {
        UniTask<string> GetLastSavedCode(string token, string taskId,
            IProgress<float> progress = null);
    }
}