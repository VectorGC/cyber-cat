using ApiGateway.Client.Application;
using Assets.SimpleVKSignIn.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class AuthWithVkService : IInitializable, ITickable
{
    public bool IsLogginedWithVk { get; private set; }
    public string UserName { get; private set; }
    public bool IsWaitResponse { get; private set; }

    private bool NeedAutoLoginWithVk
    {
        get => PlayerPrefs.GetInt(nameof(NeedAutoLoginWithVk)) == 1;
        set => PlayerPrefs.SetInt(nameof(NeedAutoLoginWithVk), value ? 1 : 0);
    }

    private readonly ApiGatewayClient _client;

    public AuthWithVkService(ApiGatewayClient client)
    {
        _client = client;
    }

    public void Initialize()
    {
        if (NeedAutoLoginWithVk)
            SignIn();
    }

    public void Tick()
    {
        if (_client.Player == null && IsLogginedWithVk)
        {
            SignOut();
        }
    }

    public void SignIn()
    {
        if (IsLogginedWithVk)
            return;

        var savedAuth = VKAuth.SavedAuth;
        if (savedAuth != null)
        {
            OnSignedIn(savedAuth.TokenResponse.email, savedAuth.UserInfo.first_name, savedAuth.UserInfo.id).Forget();
            return;
        }

        VKAuth.SignIn((success, error, userInfo) =>
        {
            if (!success)
            {
                Debug.LogError($"Vk Sign in: {error}");
                return;
            }

            OnSignedIn(VKAuth.SavedAuth.TokenResponse.email, userInfo.first_name, userInfo.id).Forget();
            Debug.Log($"Vk Sign in: Welcome {userInfo.first_name}");
        });

        IsWaitResponse = true;
    }

    private void SignOut()
    {
        VKAuth.SignOut();
        Debug.Log($"Vk Sign out: Goodbye! See you soon '{UserName}'");
        IsLogginedWithVk = false;
        UserName = string.Empty;
        NeedAutoLoginWithVk = false;
    }

    private async UniTaskVoid OnSignedIn(string email, string name, long vkId)
    {
        IsWaitResponse = false;

        var result = await _client.LoginPlayerWithVk(email, name, vkId.ToString());
        if (!result.IsSuccess)
        {
            Debug.LogError(result.Error);
            return;
        }

        Debug.Log($"Vk Sign in: Auth success. Welcome {name}!");
        IsLogginedWithVk = true;
        UserName = name;
        NeedAutoLoginWithVk = true;
    }
}