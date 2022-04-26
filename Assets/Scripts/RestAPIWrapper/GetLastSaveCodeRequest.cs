using System;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Proyecto26;

namespace RestAPIWrapper
{
    public class GetLastSaveCodeRequest : IGetLastSaveCodeRequest
    {
        public async UniTask<string> GetLastSavedCode(string token, string taskId,
            IProgress<float> progress = null)
        {
            var request = new RequestHelper
            {
                Uri = Endpoint.SOLUTION,
                Params =
                {
                    ["token"] = token,
                    ["task_id"] = taskId
                },
                ProgressCallback = value => progress?.Report(value),
                EnableDebug = true
            };

            var jObj = await RestClient.Get<JObject>(request).ToUniTask();
            var code = jObj["text"].ToString();

            return code;
        }
    }
}