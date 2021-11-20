using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using UnityEngine.Networking;

public class TestPostRequest : MonoBehaviour
{
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

        formData.Add(new MultipartFormFileSection("source_93", "твой_локально_сохранённый_файл.txt"));
        
        UnityWebRequest www = UnityWebRequest.Post("https://kee-reel.com/solution", formData);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }
}
