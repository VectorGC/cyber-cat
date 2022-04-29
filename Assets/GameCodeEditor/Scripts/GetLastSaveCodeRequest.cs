using System;
using Cysharp.Threading.Tasks;
using RestAPIWrapper;

namespace GameCodeEditor.Scripts
{
    public class GetLastSaveCodeRequest : IGetLastSaveCodeRequest
    {
        public async UniTask<string> GetLastSavedCode(string token, string taskId,
            IProgress<float> progress = null)
        {
            return await RestAPI.Instance.GetLastSavedCode(token, taskId, progress);
        }
    }
}