using UnityEngine;
using Authentication;
using MonoBehaviours.PropertyFields;

public class StartController : MonoBehaviour
{
    [SerializeField] private SceneField authScene;
    [SerializeField] private SceneField menu;

    private async void Start()
    {
        if (TokenSession.IsNoneToken)
        {
            await TokenSession.RequestAndSaveFromServer("user", "user");
            //authScene.LoadAsyncViaLoadingScreen();
        }

        menu.LoadAsyncViaLoadingScreen();
    }
}