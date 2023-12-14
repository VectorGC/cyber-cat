using Zenject;

public static class AuthorizationInstaller
{
    public static void InstallBindings(DiContainer container)
    {
        container.BindInterfacesAndSelfTo<AccountState>().AsSingle();
        container.BindInterfacesAndSelfTo<AuthorizationPresenter>().AsSingle();
    }
}