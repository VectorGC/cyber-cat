using Zenject;

public static class AccountInstaller
{
    public static void InstallBindings(DiContainer container)
    {
        container.BindInterfacesAndSelfTo<AccountState>().AsSingle();
    }
}