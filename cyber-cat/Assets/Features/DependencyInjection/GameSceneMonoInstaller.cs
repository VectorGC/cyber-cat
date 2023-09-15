using ApiGateway.Client.Models;
using Features.ServerConfig;
using Zenject;

public class GameSceneMonoInstaller : MonoInstaller<GameSceneMonoInstaller>
{
    public override void InstallBindings()
    {
        Container.BindAsync<IPlayer>().FromMethod(ServerAPI.CreatePlayerClient).AsSingle();
        
        //Container.Bind<InteractHandler>().AsSingle();
    }
}
