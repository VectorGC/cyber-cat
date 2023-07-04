using System.Net.Http;

namespace UnityEngine.Networking
{
    public class UnityWebRequest
    {
        public enum Result
        {
            InProgress,
            Success,
            ConnectionError,
            ProtocolError,
            DataProcessingError,
        }

        public Result result;
        public DownloadHandler downloadHandler = new DownloadHandler();
        private string _uri;

        private UnityWebRequest(string uri)
        {
            _uri = uri;
        }

        public static UnityWebRequest Get(string uri)
        {
            return new UnityWebRequest(uri);
        }

        public UnityWebRequestAsyncOperation SendWebRequest()
        {
            var client = new HttpClient();
            //var client = new WebClient();
            // var task = client.DownloadStringTaskAsync(_uri);
            var task = client.GetStringAsync(_uri);
            //client.DownloadStringAsync(new Uri(_uri));

            return new UnityWebRequestAsyncOperation(this, task);
        }
    }
}