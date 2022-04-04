using Authentication;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestAPIWrapper;
using UnityEngine;

public class TestNewTaskGetter : MonoBehaviour
{
    async void Start()
    {
        //var token = await TokenSession.Register("karim.kimsanbaev@gmail.com", "123456", "Karim"); // Здесь 103
        //var token = await TokenSession.RequestAndSaveFromServer("karim.kimsanbaev@gmail.com", "123456"); // Здесь 102
        var token = TokenSession.FromPlayerPrefs(); // Здесь 302
        var response = await RestAPI.GetTaskFolders(token);
        var text = response.Text;

        var obj = JsonConvert.DeserializeObject(text);
        var jobj = JsonConvert.DeserializeObject<JObject>(text);

        var t = 10;
    }
}
