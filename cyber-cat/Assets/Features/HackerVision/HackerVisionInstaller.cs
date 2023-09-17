using Zenject;

public static class HackerVisionInstaller
{
    public static void InstallBindings(DiContainer container)
    {
        container.BindInterfacesAndSelfTo<HackerVision>().AsSingle();
    }
}