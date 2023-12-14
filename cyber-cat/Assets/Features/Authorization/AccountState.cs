using System;
using ApiGateway.Client.Application;
using Assets.SimpleVKSignIn.Scripts;
using Cysharp.Threading.Tasks;
using Shared.Models.Domain.Users;
using UniMob;
using UnityEngine;
using Zenject;

public class AccountState : ILifetimeScope, IInitializable, IDisposable
{
    [Atom] public UserModel User { get; set; }

    public Lifetime Lifetime => _lifetimeController.Lifetime;
    private readonly LifetimeController _lifetimeController = new LifetimeController();
    private readonly ApiGatewayClient _client;

    public bool IsSignedIn => User != null;

    public AccountState(ApiGatewayClient client)
    {
        _client = client;
        User = client.Player?.User;
    }

    public void Initialize() => SignIn();

    public void Dispose()
    {
        _lifetimeController.Dispose();
    }

    public void SignIn()
    {
        return;

        if (IsSignedIn)
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
    }

    private async UniTaskVoid OnSignedIn(string email, string name, long vkId)
    {
        var result = await _client.LoginPlayerWithVk(email, name, vkId.ToString());
        if (!result.IsSuccess)
        {
            Debug.LogError(result.Error);
            return;
        }

        User = result.Value.User;
        Debug.Log($"Vk Sign in: Auth success. Welcome {name}!");
    }

    public async UniTaskVoid SignOut()
    {
        var name = User.FirstName;

        VKAuth.SignOut();
        if (_client.Player != null)
        {
            var result = await _client.Player.Logout();
            if (!result.IsSuccess)
            {
                Debug.LogError(result.Error);
                return;
            }
        }

        User = null;
        Debug.Log($"Vk Sign out: Goodbye! See you soon '{name}'");
    }
}