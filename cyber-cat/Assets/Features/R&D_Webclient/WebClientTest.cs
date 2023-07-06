using System;
using System.Collections;
using System.Net;
using System.Threading.Tasks;
using ApiGateway.Client;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class WebClientTest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _response;

    private async void Start()
    {
        //StartCoroutine(GetStringAsyncUniTask());
        var client = ServerClient.Create("http://localhost:5000");
        //var response = await GetStringAsync();
        var response = await client.Authorization.GetAuthenticationToken("cat", "cat");
        Debug.Log(response);
        _response.text = response;
    }
/*
    private async Task<string> GetStringAsyncUniTask()
    {
        var tcs = new UniTaskCompletionSource<string>();
        var enumerator = GetStringAsync(tcs);


        while (enumerator.MoveNext())
        {
            await UniTask.Yield();
        }


        return await tcs.Task;
    }
    */


    private async Task<string> GetStringAsync()
    {
        //var request = await RestClient.Get("http://localhost:5000/auth/simple").ToUniTask();
        var request = UnityWebRequest.Get("http://localhost:5000/auth/simple");

        var tcs = new TaskCompletionSource<string>();
        //tcs.TrySetResult(request.Text);

        var enumerable = request.SendWebRequest();
        enumerable.GetAwaiter().GetResult();
        enumerable.completed += (obj) =>
        {
            var request2 = (UnityWebRequestAsyncOperation) obj;

            var isNetworkError = (request2.webRequest.result == UnityWebRequest.Result.ConnectionError);
            var isHttpError = (request2.webRequest.result == UnityWebRequest.Result.ProtocolError);

            if (request2.isDone && !isNetworkError && !isHttpError)
            {
                tcs.TrySetResult(request2.webRequest.downloadHandler.text);
            }
            else
            {
                tcs.TrySetResult("123");
            }
        };

        return await tcs.Task;
    }

    private void EnumerableOncompleted(AsyncOperation obj)
    {
        throw new System.NotImplementedException();
    }

    private IEnumerator Wait(Task<string> task)
    {
        while (!task.IsCompleted)
        {
            Debug.Log("e.ToString()");
            _response.text = "e.ToString()";

            yield return new WaitForSeconds(1);
        }

        Debug.Log("e.ToString()123");
        _response.text = "e.ToString()123";
    }

    private void ClientOnDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
    {
        Debug.Log(e.ToString());
        _response.text = e.ToString();
    }
}