using System;
using ApiGateway.Client.V2;
using ApiGateway.Client.V2.Access;
using Assets.SimpleVKSignIn.Scripts;
using Cysharp.Threading.Tasks;
using UniMob;
using UnityEngine;
using Zenject;

public class AccountState : ILifetimeScope, IInitializable, IDisposable
{
    [Atom] private User User { get; set; }

    public Lifetime Lifetime => _lifetimeController.Lifetime;
    private readonly LifetimeController _lifetimeController = new LifetimeController();

    public string UserName => User.Name;
    public bool IsSignedIn => User.Access<Vk>().IsSignedIn;

    public AccountState(User user)
    {
        User = user;
    }

    public void Initialize() => SignIn();

    public void Dispose()
    {
        _lifetimeController.Dispose();
    }

    public void SignIn()
    {
        if (User.Access<Vk>().IsSignedIn)
            return;

        var savedAuth = VKAuth.SavedAuth;
        if (savedAuth != null)
        {
            OnSignedIn(savedAuth.TokenResponse.email, savedAuth.UserInfo.first_name).Forget();
            return;
        }

        VKAuth.SignIn((success, error, userInfo) =>
        {
            if (!success)
            {
                Debug.LogError($"Vk Sign in: {error}");
                return;
            }

            OnSignedIn(VKAuth.SavedAuth.TokenResponse.email, userInfo.first_name).Forget();

            Debug.Log($"Vk Sign in: Welcome {userInfo.first_name}");
        });
    }

    private async UniTaskVoid OnSignedIn(string email, string name)
    {
        await User.Access<Vk>().SignIn(email, name);

        // Update ui.
        var user = User;
        User = null;
        User = user;

        Debug.Log($"Vk Sign in: Auth success. Welcome {User.Name}!");
    }

    public void SignOut()
    {
        var name = UserName;

        VKAuth.SignOut();
        User.Access<Vk>().SignOut();

        // Update ui.
        var user = User;
        User = null;
        User = user;

        Debug.Log($"Vk Sign out: Goodbye! See you soon '{name}'");
    }
}