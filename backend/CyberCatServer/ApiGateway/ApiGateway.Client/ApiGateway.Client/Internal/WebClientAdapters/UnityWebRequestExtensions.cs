using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace ApiGateway.Client
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

        public static void EnsureSuccessStatusCode(this UnityWebRequest webRequest)
        {
            var isNetworkError = webRequest.result == UnityWebRequest.Result.ConnectionError;
            var isHttpError = webRequest.result == UnityWebRequest.Result.ProtocolError;

            if (!webRequest.isDone || isNetworkError || isHttpError)
            {
                throw new UnityWebException(webRequest.responseCode);
            }
        }
    }
}