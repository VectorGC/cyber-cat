using Zenject;

public static class CodeEditorInstaller
{
    public static void InstallBindings(DiContainer container)
    {
        container.Bind<ICodeEditor>().To<CodeEditor>().AsSingle();
    }
}