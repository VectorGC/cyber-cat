using System;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Proyecto26;
using RestAPIWrapper;

namespace GameCodeEditor.Scripts
{
    public class GetLastSaveCodeRequest : IGetLastSaveCodeRequest
    {
        private const string Solution = Endpoint.MainEndpoint.Uri + "/solution";
        
        public async UniTask<string> GetLastSavedCode(string token, string taskId,
            IProgress<float> progress = null)
        {
            var request = new RequestHelper
            {
                Uri = Solution,
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