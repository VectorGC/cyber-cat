#if UNITY_WEBGL
using System.Net;
using System.Threading.Tasks;
using UnityEngine;

namespace ApiGateway.Client.V3.Infrastructure.WebClientAdapters.UnityWebRequest
{
    internal static class UnityWebRequestExtensions
    {
        public static Task WaitAsync(this AsyncOperation asyncOperation)
        {
            var tcs = new TaskCompletionSource<AsyncOperation>();

            void OnCompleted(AsyncOperation completedOperation)
            {
                completedOperation.completed -= OnCompleted;
                tcs.SetResult(completedOperation);
            }

            asyncOperation.completed += OnCompleted;

            return tcs.Task;
        }

        public static void EnsureSuccessStatusCode(this UnityEngine.Networking.UnityWebRequest webRequest)
        {
            var isNetworkError = webRequest.result == UnityEngine.Networking.UnityWebRequest.Result.ConnectionError;
            var isHttpError = webRequest.result == UnityEngine.Networking.UnityWebRequest.Result.ProtocolError;

            if (!webRequest.isDone || isNetworkError || isHttpError || webRequest.responseCode != (int) HttpStatusCode.OK)
            {
                DebugOnly.LogError($"Throw Web Exception: Status: '{webRequest.responseCode}'. {webRequest.downloadHandler.text}");
                throw new UnityWebException((HttpStatusCode) webRequest.responseCode, webRequest.downloadHandler.text);
            }
        }
    }
}
#endif