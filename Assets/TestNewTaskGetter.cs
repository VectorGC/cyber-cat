using Authentication;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestAPIWrapper;
using UnityEngine;

public class TestNewTaskGetter : MonoBehaviour
{
    async void Start()
    {
        //var token = await TokenSession.Register("karim.kimsanbaev@gmail.com", "123456", "Karim");
        //var token = await TokenSession.RequestAndSaveFromServer("karim.kimsanbaev@gmail.com", "123456");
        var token = TokenSession.FromPlayerPrefs();
        var response = await RestAPI.GetTaskFolders(token);
        var text = response.Text;

        var obj = JsonConvert.DeserializeObject(text);
        var jobj = JsonConvert.DeserializeObject<JObject>(text);

        var t = 10;
    }
}
