using System;
using System.Net;
using System.Threading.Tasks;

namespace UnityEngine.Networking
{
    public class UnityWebRequestAsyncOperation : AsyncOperation
    {
        public event Action<AsyncOperation> completed;

        public UnityWebRequest webRequest;

        private WebClient _webClient;
        private Task<string> _task;

        public UnityWebRequestAsyncOperation(UnityWebRequest webRequest, WebClient webClient, Task<string> task)
        {
            _webClient = webClient;
            this.webRequest = webRequest;

            _task = task;
            _task.GetAwaiter().OnCompleted(WebClientOnDownloadStringCompleted);
            //_task.ConfigureAwait(false);

            _webClient.DownloadStringCompleted += WebClientOnDownloadStringCompleted;
        }

        public UnityWebRequestAsyncOperation(UnityWebRequest webRequest, Task<string> task)
        {
            this.webRequest = webRequest;

            _task = task;
            _task.GetAwaiter().OnCompleted(WebClientOnDownloadStringCompleted);
        }

        private void WebClientOnDownloadStringCompleted()
        {
            webRequest.downloadHandler.text = _task.Result;
            isDone = true;
            webRequest.result = UnityWebRequest.Result.Success;
            completed?.Invoke(this);
        }

        private void WebClientOnDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            _webClient.DownloadStringCompleted -= WebClientOnDownloadStringCompleted;

            webRequest.downloadHandler.text = e.Result;
            isDone = true;
            webRequest.result = UnityWebRequest.Result.Success;
            completed?.Invoke(this);
        }
    }
}