using System;
using System.Net;
using Newtonsoft.Json;
using Observers;
using UniRx;

namespace WebRequests.Requesters
{
    public static class WWWRequesterExt
    {
        public static IObservable<TResponse> SendWWWGetObject<TResponse>(this IGetWebRequest<TResponse> webRequest,
            IProgress<float> progress = null)
        {
            return webRequest.SendWWWGet(progress).ResponseHandle().JsonDeserialize<TResponse>();
        }

        private static IObservable<string> SendWWWGet(this IWebRequest webRequest, IProgress<float> progress = null)
        {
            var requester = new WWWRequester();
            return requester.SendGetRequest(webRequest, progress);
        }
    }

    public static class ResponseHandleObserverExt
    {
        public static IObservable<string> ResponseHandle(
            this IObservable<string> textObservable)
        {
            var responseHandleObserver = new ResponseHandleObserver();
            textObservable.Do(responseHandleObserver).Subscribe();

            return responseHandleObserver;
        }
    }

    public struct WebErrorException
    {
        [JsonProperty] public string Error { get; set; }

        public bool IsNone => string.IsNullOrEmpty(Error);

        public WebException Exception => new WebException(Error);

        public override string ToString() => Error;
    }

    public class ResponseHandleObserver : IObserver<string>, IObservable<string>
    {
        private readonly Subject<string> _subject = new Subject<string>();

        public void OnCompleted() => _subject.OnCompleted();

        public void OnError(Exception error) => _subject.OnError(error);

        public void OnNext(string value)
        {
            var error = JsonConvert.DeserializeObject<WebErrorException>(value);
            if (error.IsNone)
            {
                _subject.OnNext(value);
                return;
            }

            OnError(error.Exception);
        }

        public IDisposable Subscribe(IObserver<string> observer) => _subject.Subscribe(observer);
    }
}