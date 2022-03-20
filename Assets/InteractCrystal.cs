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
    
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        
        var isHackModePressed = Input.GetKeyDown(KeyCode.F);
        if (isHackModePressed && GameMode.HackMode == Mode.HackMode)
        {
            var pauseObserver = new PauseTimeScaleObserver<Scene>();

            var result = CodeEditorController.Open(Task);
            result.WhileOpen(pauseObserver);
            result.ProgressObservable().ViaLoadingScreen();
        }
    }
}
