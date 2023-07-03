using System;
using System.Collections;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class WebClientTest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _response;

    private async void Start()
    {
        //StartCoroutine(GetStringAsyncUniTask());
        //GetStringAsync();

        await TaskTest();
    }

    /*
    private IEnumerator GetStringAsyncUniTask()
    {
        yield return GetStringAsync();
    }
    */

    public async Task TaskTest()
    {
        _response.text = "Start";
        var result = await GetLabelAsync();
        _response.text = result;
    }

    public async Task<string> GetLabelAsync()
    {
        await Task.Delay(1000);
        return "Complete Task";
    }

    private void GetStringAsync()
    {
        var uri = "https://example.com/";

        var client = new HttpClient();
        var t = client.GetStringAsync(uri);
        // client.DownloadStringAsync(new Uri(uri));

        StartCoroutine(Wait(t));

        //client.DownloadStringCompleted += ClientOnDownloadStringCompleted;
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