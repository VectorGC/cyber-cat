using Authentication;
using UnityEngine;

public class Logout : MonoBehaviour
{
    public void LogoutAsync()
    {
        TokenSession.DeleteFromPlayerPrefs();
        UIDialogs.Instance.StartScene.LoadAsync();
    }
}