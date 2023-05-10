using UnityEngine;
using Authentication;
using MonoBehaviours.PropertyFields;
using RestAPIWrapper;

public class StartController : MonoBehaviour
{
    [SerializeField] private SceneField authScene;
    [SerializeField] private SceneField menu;

    private void Start()
    {
        if (string.IsNullOrEmpty(PlayerPrefsInfo.Token))
        {
            authScene.LoadAsyncViaLoadingScreen();
            return;
        }

        menu.LoadAsyncViaLoadingScreen();
    }
}