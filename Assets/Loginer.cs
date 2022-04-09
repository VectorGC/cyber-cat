using System;
using UnityEngine;
using Authentication;
using Cysharp.Threading.Tasks;
using RestAPIWrapper;

public class Loginer : MonoBehaviour
{
    [SerializeField] private MonoBehaviours.SceneLoader _loginForm;

    async void Start()
    {
        var good = await TryToLogin();
        if (!good)
        {
            _loginForm.LoadSceneAsync();
        }
    }

    public async UniTask<bool> TryToLogin()
    {

        var t = TokenSession.FromPlayerPrefs();

        try
        {
            var response = await RestAPIWrapper.RestAPI.AutoLogin(t.Token);
        }
        catch (RequestTokenException ex)
        {
            Enum.TryParse<WebError>(ex.Message, out var er);
            var isLoginError = er.HasFlag(WebError.LoginError);

            if (isLoginError)
            {
                return false;
            }
        }
        
        return true;
    }



}
