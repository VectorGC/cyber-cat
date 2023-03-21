using Authentication;
using UnityEngine;

public class Logout : MonoBehaviour
{
    public void LogoutAsync()
    {
        TokenSession.DeleteFromPlayerPrefs();
        UIDialogs.Instance.StartScene.LoadAsyncViaLoadingScreen();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}