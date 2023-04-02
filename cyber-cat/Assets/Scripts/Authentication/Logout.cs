using Authentication;
using RequestAPI.Proxy;
using UnityEngine;

public class Logout : MonoBehaviour
{
    public void LogoutAsync()
    {
        RequestAPIProxy.DeleteTokenFromPlayerPrefs();
        UIDialogs.Instance.StartScene.LoadAsyncViaLoadingScreen();
    }
}