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
            var task = await RestClient.Post(request).ToUniTask();
            return task.Text;
        }

        private enum HttpRequest
        {
            GET,
            SET
        }
    }
}