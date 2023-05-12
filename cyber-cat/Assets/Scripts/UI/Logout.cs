using Patch3rdParty.ConfigurationUI.LoadingScreen;
using RestAPIWrapper;
using UnityEngine;

public class Logout : MonoBehaviour
{
    public void LogoutAsync()
    {
        PlayerPrefs.DeleteKey(PlayerPrefsInfo.Key);
        UIDialogs.Instance.StartScene.LoadAsyncViaLoadingScreen();
    }
}