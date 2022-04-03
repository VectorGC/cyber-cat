using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using MonoBehaviours;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
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

public class InteractCrystal : MonoBehaviour
{
    private SphereCollider _collider;
    public string Task;

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
            await CodeEditor.OpenSolution(Task, progress);
            Time.timeScale = 1f;
        }
    }
}
