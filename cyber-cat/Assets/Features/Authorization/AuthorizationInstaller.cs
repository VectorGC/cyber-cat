using Zenject;

public static class AuthorizationInstaller
{
    public static void InstallBindings(DiContainer container)
    {
        container.BindInterfacesAndSelfTo<AuthorizationPresenter>().AsSingle();
        container.BindInterfacesAndSelfTo<AuthWithVkService>().AsSingle();
    }
}