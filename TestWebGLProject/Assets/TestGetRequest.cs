using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

[Serializable]
public class UnityStringEvent : UnityEvent<string>
{
    
}

public class TestGetRequest : MonoBehaviour
{
    public UnityStringEvent Requested;

    public void GetRequest()
    {
        // A correct website page.
        StartCoroutine(GetRequest2("https://kee-reel.com/cyber-cat"));
        //StartCoroutine(GetRequest2("https://www.example.com"));
    }
    
    public IEnumerator GetRequest2(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    Requested.Invoke(webRequest.downloadHandler.text);
                    break;
            }
        }
    }
}
