using UnityEngine;
using Authentication;
using MonoBehaviours.PropertyFields;

public class StartController : MonoBehaviour
{
    [SerializeField] private SceneField authScene;
    [SerializeField] private SceneField menu;

    private void Start()
    {
        if (TokenSession.IsNoneToken())
        {
            authScene.LoadAsyncViaLoadingScreen();
            return;
        }

        menu.LoadAsyncViaLoadingScreen();
    }
}