using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Proyecto26;
using ServerAPIBase;
using System;

namespace RestAPIWrapper.V1
{
    public class TasksGetterV1 : ITasksGetter<string>
    {
        public void Request(ITasksGetterData data, Action<string> callback)
        {
            SendRequest(data).Done(x => Done(x, callback));
        }

        public async UniTask<string> RequestAsync(ITasksGetterData data, IProgress<float> progress = null)
        {
            var response = await SendRequest(data, progress).ToUniTask();
            return response.Text;
        }

        private RSG.IPromise<ResponseHelper> SendRequest(ITasksGetterData data, IProgress<float> progress = null)
        {
            var request = new RequestHelper
            {
                Uri = ServerData.URL + "/tasks/flat",
                Params =
                {
                    ["token"] = data.Token
                },
                ProgressCallback = value => progress?.Report(value),
                EnableDebug = ServerData.DebugBuild
            };

            return RestClient.Get(request);
        }

        private void Done(ResponseHelper response, Action<string> callback)
        {
            callback?.Invoke(response.Text);
        }
    }
}