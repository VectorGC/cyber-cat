using System;
using System.Threading.Tasks;
using Authentication;
using Cysharp.Threading.Tasks;
using MonoBehaviours;
using Newtonsoft.Json.Linq;
using RestAPIWrapper;
using TasksData;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Serialization;
using Scene = UnityEngine.SceneManagement.Scene;

public enum Mode
{
    Default = 0,
    HackMode,
    Editor
}

public static class GameMode
{
    public static Mode HackMode = 0;
}

[Serializable]
public struct TaskFolder
{
    [SerializeField] private string _unit;
    [SerializeField] private string _task;

    public TaskFolder(string unit, string task)
    {
        _unit = unit;
        _task = task;
    }

    public async UniTask<ITaskTicket> GetTask(string token, IProgress<float> progress = null)
    {
        var folders = await RestAPI.GetTaskFolders(token, progress);

        var taskJToken = GetTaskJToken(folders);
        var task = taskJToken.ToObject<TaskData>();

        return task;
    }

    private JToken GetTaskJToken(JObject jObject)
    {
        var taskJToken = jObject["sample_tests"]["units"][_unit]["tasks"][_task];
        return taskJToken;
    }
}

public class InteractCrystal : MonoBehaviour
{
    private SphereCollider _collider;

    [SerializeField] private TaskFolder taskFolder;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<SphereCollider>();
        _collider.OnCollisionStayAsObservable().Subscribe(x => Debug.Log("123"));
    }

    private async void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        var isHackModePressed = Input.GetKeyDown(KeyCode.F);
        if (isHackModePressed && GameMode.HackMode == Mode.HackMode)
        {
            var progress = new ScheduledNotifier<float>();
            progress.ViaLoadingScreen();

            Time.timeScale = 0f;
            await CodeEditor.OpenSolution(taskFolder, progress);
            Time.timeScale = 1f;
        }
    }
}