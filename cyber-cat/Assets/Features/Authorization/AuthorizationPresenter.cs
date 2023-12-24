using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class AuthorizationPresenter
{
    public async UniTaskVoid Show()
    {
        await SceneManager.LoadSceneAsync("AuthorizationScene", LoadSceneMode.Additive);
        var scene = SceneManager.GetSceneByName("AuthorizationScene");
        SceneManager.SetActiveScene(scene);
    }

    public async UniTaskVoid Hide()
    {
        await SceneManager.UnloadSceneAsync("AuthorizationScene").ToUniTask();
    }
}