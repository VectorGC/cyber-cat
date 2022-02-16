using System;
using System.Collections;
using System.Collections.Generic;
using Authentication;
using UnityEngine;
using UnityEngine.Networking;
using UnityEventTypes;

public class TestPostRequest : MonoBehaviour
{
    public UnityStringEvent PostReceived;
    
    public void Upload()
    {
        StartCoroutine(Post());
    }

    public const string token =
        "S92DeuJksmAH0jVfpFn7dBVgQkw8kqrPrqJUBtQ8ZtN1PeLbeft9FSsk8M7NGZReGtz0RfQFihQcm2AldJRynkVZ4lSPHAIzhtF68UPNb16WqIKBtaOnvdnLY68Z8oVq3Xaq4gjvHMeppYnH1IleqXnWhqujSrfl2W2vNCEhQW68jXVu9aJqYYmyNX0PZHqDQPNTH7o7o4i6D9r7NkWFheUpn9SKIROcFYXV9yhRwdGXlsRvHrPjGecltv3yez54";

    public IEnumerator Post()
    {
        var formData = new List<IMultipartFormSection>();

        // UnityWebRequest www = UnityWebRequest.Get("https://kee-reel.com/solution?token="+token+"&gen_doc=0");

        var formSection = new MultipartFormDataSection("token", token);
        formData.Add(formSection); 
        
        var sectionTaskId = new MultipartFormDataSection("tasks", "93");
        formData.Add(sectionTaskId);

        var data = System.Text.Encoding.UTF8.GetBytes("#include <stdio.h>\nint main(){return 0;}");
        formData.Add(new MultipartFormFileSection("source_93", data, "test.c", "application/octet-stream"));

        UnityWebRequest www = UnityWebRequest.Post("https://kee-reel.com/solution", formData);
        
        var t = www.SendWebRequest();
        while (!t.isDone)
        {
            Debug.Log($"uploadProgress {t.webRequest.uploadProgress}");
            Debug.Log($"progress {t.progress}");
            Debug.Log($"downloadProgress {t.webRequest.downloadProgress}");
            yield return null;
        }

        Debug.Log($"uploadProgress {t.webRequest.uploadProgress}");
        Debug.Log($"progress {t.progress}");
        Debug.Log($"downloadProgress {t.webRequest.downloadProgress}");
        
        //yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            PostReceived.Invoke(www.downloadHandler.text);
        }
    }
}
