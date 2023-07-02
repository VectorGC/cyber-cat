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

    private void Start()
    {
        //StartCoroutine(GetStringAsyncUniTask());
        GetStringAsync();
    }

    /*
    private IEnumerator GetStringAsyncUniTask()
    {
        yield return GetStringAsync();
    }
    */

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