using Cysharp.Threading.Tasks;
using Proyecto26;
using UnityEngine;

namespace RestAPIWrapper.Server
{
    [CreateAssetMenu(fileName = "Base server", menuName = "ScriptableObjects/BaseServer")]
    public class BaseServer : ScriptableObject
    {
        [field: SerializeField]
        public string URL { get; private set; }

        [SerializeField]
        private HttpRequest _requestType;

        public async UniTask<string> GetActualServerURL()
        {
            return await AskURL();
        }

        private async UniTask<string> AskURL()
        {
            var request = new RequestHelper
            {
                Uri = URL,
                EnableDebug = Debug.isDebugBuild
            };
            ResponseHelper task = null;
            if (_requestType == HttpRequest.POST)
            {
                task = await RestClient.Post(request).ToUniTask();
            }
            else if (_requestType == HttpRequest.GET)
            {
                task = await RestClient.Get(request).ToUniTask();
            }
            return task.Text;
        }

        private enum HttpRequest
        {
            GET,
            POST
        }
    }
}