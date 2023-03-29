using UnityEngine;
using Authentication;
using MonoBehaviours.PropertyFields;
using RequestAPI.Proxy;

public class StartController : MonoBehaviour
{
    [SerializeField] private SceneField authScene;
    [SerializeField] private SceneField menu;

    private void Start()
    {
        if (string.IsNullOrEmpty(RequestAPIProxy.GetTokenFromPlayerPrefs(false).Token))
        {
            authScene.LoadAsyncViaLoadingScreen();
            return;
        }

        menu.LoadAsyncViaLoadingScreen();
    }
}