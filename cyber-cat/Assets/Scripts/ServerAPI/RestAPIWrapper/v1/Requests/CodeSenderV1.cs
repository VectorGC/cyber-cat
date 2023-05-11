using Cysharp.Threading.Tasks;
using Proyecto26;
using ServerAPIBase;
using System;
using UnityEngine;

namespace RestAPIWrapper.V1
{
    public class CodeSenderV1 : ICodeSender<string>
    {
        public void Request(ICodeSenderData data, Action<string> callback)
        {
            SendRequest(data).Done(x => DoneRequest(x, callback));
        }

        public async UniTask<string> RequestAsync(ICodeSenderData data, IProgress<float> progress = null)
        {
            var response = await SendRequest(data, progress).ToUniTask();
            return response.Text;
        }

        private RSG.IPromise<ResponseHelper> SendRequest(ICodeSenderData data, IProgress<float> progress = null)
        {
            var formData = new WWWForm();

            formData.AddField("task_id", data.TaskData.Id);
            formData.AddField("source_text", data.Code);
            formData.AddField("lang", data.CodeLanguage);
            var request = new RequestHelper
            {
                Uri = ServerData.URL + "/solution",
                Params =
                {
                    ["token"] = data.Token
                },
                FormData = formData,
                ProgressCallback = value => progress?.Report(value),
                EnableDebug = true
            };
            return RestClient.Post(request);
        }

        private void DoneRequest(ResponseHelper response, Action<string> callback)
        {
            callback?.Invoke(response.Text);
        }
    }
}