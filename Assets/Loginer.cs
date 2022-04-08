using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Authentication;
using Cysharp.Threading.Tasks;
using Extensions.RestClientExt;

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
            if (ErrorMessageView.IsLoginError(ex.Message))
            {
                return false;
            }
        }
        return true;
    }



}
