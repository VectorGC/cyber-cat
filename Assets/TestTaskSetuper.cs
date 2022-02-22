using Authentication;
using TasksData.Requests;
using UniRx;
using UnityEngine;

public class TestTaskSetuper : MonoBehaviour
{
    void Start()
    {
        var req = new GetTaskRequest("10", TokenSession.FromPlayerPrefs());
        req.SendRequest().Subscribe(x => Debug.Log(x.Name));
    }
}