using System;
using System.Collections.Generic;
using System.Net;
using Observers;
using UnityEngine;
using UnityEngine.Networking;
using WebRequests.Observers;
using WebRequests.Requests.GetTasksData;

namespace WebRequests.Extensions
{
    public class WebRequestBuilder<TResponse>
    {
        private readonly IWebRequest _webRequest;

        private event Action<TResponse> _onResponse;

        public WebRequestBuilder(IWebRequest webRequest)
        {
            _webRequest = webRequest;
        }

        public WebRequestBuilder<TResponse> OnResponse(Action<TResponse> onResponse)
        {
            _onResponse += onResponse;
            return this;
        }

        public void SendRequest(string domain = WebRequestExt.DEFAULT_DOMAIN)
        {
            var observable = _webRequest.SendRequest();
            var observer = new WebRequestObserver();
            observer.OnResponse<TResponse>(responseObj =>
            {
                _onResponse?.Invoke(responseObj);
                _onResponse = null;
            });

            //observable.Subscribe(observer);
        }
    }

    public static class WebRequestBuilderExt
    {
        public static WebRequestBuilder<TResponse> OnResponse<TResponse>(
            this IWebReturnedResponse<TResponse> webRequest, Action<TResponse> onResponse)
        {
            var builder = new WebRequestBuilder<TResponse>(webRequest);
            builder.OnResponse(onResponse);

            return builder;
        }

        public static WebRequestBuilder<TResponse> OnResponse<TResponse>(this IWebRequest webRequest,
            Action<TResponse> onResponse)
        {
            var builder = new WebRequestBuilder<TResponse>(webRequest);
            builder.OnResponse(onResponse);

            return builder;
        }

        public static WebRequestBuilder<string> OnResponse(this IWebRequest webRequest, Action<string> onResponse)
        {
            var builder = new WebRequestBuilder<string>(webRequest);
            builder.OnResponse(onResponse);

            return builder;
        }
    }

    public class UnityWebRequester
    {
        public IObservable<AsyncOperation> SendGetRequest(IWebRequest webRequest)
        {
            var uri = webRequest.GetUri();
            var unityWebRequest = UnityWebRequest.Get(uri);

            var observable = new UnityWebRequestObservable(unityWebRequest);
            observable.StartOperation();

            return observable;
        }
    }

    public class UnityWebRequestObservable : IObservable<UnityWebRequestAsyncOperation>,
        IObserver<UnityWebRequestAsyncOperation>
    {
        private readonly UnityWebRequest _webRequest;

        private readonly ObserversList<UnityWebRequestAsyncOperation> _observers =
            new ObserversList<UnityWebRequestAsyncOperation>();

        public UnityWebRequestObservable(UnityWebRequest webRequest)
        {
            _webRequest = webRequest;
        }

        public void StartOperation()
        {
            var webAsyncOperation = _webRequest.SendWebRequest();
            var asyncOperationObservable = new AsyncOperationObservable<UnityWebRequestAsyncOperation>(webAsyncOperation);

            asyncOperationObservable.Subscribe(this);
            
            asyncOperationObservable.StartOperation();
        }

        public IDisposable Subscribe(IObserver<UnityWebRequestAsyncOperation> observer)
        {
            if (_webRequest.isDone)
            {
                observer.OnCompleted();
            }

            return _observers.Subscribe(observer);
        }

        public void OnCompleted()
        {
            switch (_webRequest.result)
            {
                case UnityWebRequest.Result.Success:
                    _observers.OnCompleted();
                    break;
                default:
                    OnError(new WebException(_webRequest.error));
                    break;
            }
        }

        public void OnError(Exception error)
        {
            _observers.OnError(error);
        }

        public void OnNext(UnityWebRequestAsyncOperation value)
        {
            _observers.OnNext(value);
        }
    }

    public static class WebRequestExt
    {
        public const string DEFAULT_DOMAIN = "https://kee-reel.com/cyber-cat";

        public static IObservable<AsyncOperation> SendRequest(this IWebRequest webRequest,
            string domain = DEFAULT_DOMAIN)
        {
            var uri = webRequest.GetUri();
            //var unityWebRequest = webRequest.GetWebRequestHandler(uri);
            return null;
        }

        private static UnityWebRequest GetWebRequestHandler(this IWebRequest webRequest, string url)
        {
            if (webRequest is IUnityWebRequestHandler webRequestHandler)
            {
                return webRequestHandler.GetWebRequestHandler(url);
            }

            // Fallback to default web request handle implementation.
            return UnityWebRequest.Get(url);
        }

        public static Uri GetUri(this IWebRequest webRequest, string domain = DEFAULT_DOMAIN)
        {
            var builder = new UriBuilder(domain)
            {
                Query = webRequest.QueryParams.ToString()
            };

            return builder.Uri;
        }
    }
}