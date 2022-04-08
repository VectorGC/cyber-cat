using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCartoon : MonoBehaviour
{
    public static async UniTask Play()
    {
        var introCartoonScene = UIDialogs.Instance.IntroCartoon;
        await SceneManager.LoadSceneAsync(introCartoonScene)
            .ViaLoadingScreenObservable()
            .ToUniTask();
        
        var inputInAnimatorState = FindObjectOfType<InputInAnimatorState>();
        await inputInAnimatorState.ToUniTask();
    }

    private async void OnGUI()
    {
        if (GUILayout.Button("Test Intro Scene"))
        {
            await Play();
        }
    }
}
