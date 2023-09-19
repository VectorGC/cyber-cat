using Zenject;

public static class UIInstaller
{
    public static void InstallBindings(DiContainer container)
    {
        container.Bind<IModalFactory>().To<ModalWindowFactory>().AsSingle();
    }
}