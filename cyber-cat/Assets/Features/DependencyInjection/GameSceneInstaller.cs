using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private PlayerInstaller _playerInstaller;

    public override void InstallBindings()
    {
        _playerInstaller.InstallBindings(Container);

        ServerAPIInstaller.InstallBindings(Container);
        UIInstaller.InstallBindings(Container);
        CodeEditorInstaller.InstallBindings(Container);
        HackerVisionInstaller.InstallBindings(Container);
    }
}