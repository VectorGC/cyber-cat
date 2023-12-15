using Zenject;

public static class AuthorizationInstaller
{
    public static void InstallBindings(DiContainer container)
    {
        container.BindInterfacesAndSelfTo<VkAuthService>().AsSingle();
        container.BindInterfacesAndSelfTo<AuthorizationPresenter>().AsSingle();
    }
}