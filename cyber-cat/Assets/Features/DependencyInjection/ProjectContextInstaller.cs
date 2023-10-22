using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "Create ProjectContextInstaller", fileName = "ProjectContextInstaller", order = 0)]
public class ProjectContextInstaller : ScriptableObjectInstaller<ProjectContextInstaller>
{
    public override void InstallBindings()
    {
        ServerAPIInstaller.InstallBindings(Container);
        AccountInstaller.InstallBindings(Container);
        UIInstaller.InstallBindings(Container);
    }
}