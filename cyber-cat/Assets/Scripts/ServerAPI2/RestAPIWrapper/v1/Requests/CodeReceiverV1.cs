using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Proyecto26;
using ServerAPIBase;
using System;

namespace RestAPIWrapper.V1
{
    public class CodeReceiverV1 : ICodeReceiver<string>
    {
        public void Request(ICodeReceiverData data, Action<string> callback)
        {
            SendRequest(data).Done(x => callback?.Invoke(x["text"].ToString()));
        }

        public async UniTask<string> RequestAsync(ICodeReceiverData data, IProgress<float> progress = null)
        {
            var obj = await SendRequest(data, progress).ToUniTask();
            var code = obj["text"].ToString();
            return code;
        }

        private RSG.IPromise<JObject> SendRequest(ICodeReceiverData data, IProgress<float> progress = null)
        {
            var request = new RequestHelper
            {
                Uri = ServerData.URL + "/solution",
                Params =
                {
                    ["token"] = data.Token,
                    ["task_id"] = data.TaskID
                },
                ProgressCallback = value => progress?.Report(value),
                EnableDebug = true
            };

            return RestClient.Get<JObject>(request);
        }
    }
}