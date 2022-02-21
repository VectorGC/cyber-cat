using System;
using System.Linq;
using Authentication;
using Observers;
using UnityEngine;
using UnityEngine.Networking;
using WebRequests.Extensions;
using WebRequests.Observers;
using WebRequests.Requests.GetTasksData;

public class TestTaskSetuper : MonoBehaviour
{
    void Start()
    {
        var request = new GetTasksRequest(TokenSession.FromPlayerPrefs());
        var requester = new UnityWebRequester();

        var observable = requester.SendGetRequest(request);

        

        var y = new AdapterObservable<UnityWebRequestAsyncOperation, AsyncOperation>(operation =>
            (UnityWebRequestAsyncOperation) operation);
        observable.Subscribe(y);
        
        var t = new CompletionObservable<UnityWebRequestAsyncOperation>(y);
        
        var obs = new WebResponseTextObserver();
        t.Subscribe(obs);
        


        // new GetTasksRequest(TokenSession.FromPlayerPrefs())
        //     .OnResponse(tasks =>
        //     {
        //         var firstTaskId = tasks.Tasks.FirstOrDefault().Key;
        //         CodeEditor.OpenSolutionForTask(firstTaskId);
        //     })
        //     .SendRequest();
    }

    public void OnCompleted()
    {
        throw new NotImplementedException();
    }

    public void OnError(Exception error)
    {
        throw new NotImplementedException();
    }

    public void OnNext(UnityWebRequestAsyncOperation value)
    {
        throw new NotImplementedException();
    }
}

// TObservableValue -> UnityWebRequestAsyncOperation
// TObserverValue -> AsyncOperation
public class AdapterObservable<TObservableValue, TObserverValue> : IObservable<TObservableValue>,
    IObserver<TObserverValue>
{
    private readonly ObserversList<TObservableValue>
        _observers = new ObserversList<TObservableValue>();

    private readonly Func<TObserverValue, TObservableValue> _convertMethod;

    public AdapterObservable(Func<TObserverValue, TObservableValue> convertMethod)
    {
        _convertMethod = convertMethod;
    }

    public IDisposable Subscribe(IObserver<TObservableValue> observer) => _observers.Subscribe(observer);

    public void OnCompleted()
    {
        _observers.OnCompleted();
    }

    public void OnError(Exception error)
    {
        _observers.OnError(error);
    }

    public void OnNext(TObserverValue value) => _observers.OnNext(_convertMethod.Invoke(value));
}