using System;
using Cysharp.Threading.Tasks;
using Proyecto26;
using UnityEngine;

namespace RestAPIWrapper
{
    public struct CodeCheckingRequest : ICodeCheckingRequest
    {
        public async UniTask<string> SendCodeToChecking(string token, string taskId, string code,
            string progLanguage, IProgress<float> progress = null)
        {
            var formData = new WWWForm();

            formData.AddField("task_id", taskId);
            formData.AddField("source_text", code);
            formData.AddField("lang", progLanguage);

            var request = new RequestHelper
            {
                Uri = Endpoint.SOLUTION,
                Params =
                {
                    ["token"] = token
                },
                FormData = formData,
                ProgressCallback = value => progress?.Report(value),
                EnableDebug = true
            };

            var response = await RestClient.Post(request).ToUniTask();
            return response.Text;
        }
    }
}