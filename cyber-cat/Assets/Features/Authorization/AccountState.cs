using System;
using Assets.SimpleVKSignIn.Scripts;
using UniMob;

public class AccountState : ILifetimeScope, IDisposable
{
    [Atom] private UserInfo UserInfo { get; set; }

    public Lifetime Lifetime => _lifetimeController.Lifetime;
    private readonly LifetimeController _lifetimeController = new LifetimeController();

    public string UserName => UserInfo?.first_name;
    public bool IsSignedIn => UserInfo != null;

    public void SetUserInfo(UserInfo userInfo)
    {
        UserInfo = userInfo;
    }

    public void Dispose()
    {
        _lifetimeController.Dispose();
    }
}