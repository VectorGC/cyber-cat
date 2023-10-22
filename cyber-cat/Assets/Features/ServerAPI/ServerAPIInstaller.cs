using ApiGateway.Client.Models;
using ApiGateway.Client.V2;
using Features.ServerConfig;
using Zenject;

public static class ServerAPIInstaller
{
    public static void InstallBindings(DiContainer container)
    {
        container.BindAsync<IUser>().FromMethod(ServerAPI.CreateUserClient);
        container.BindAsync<IPlayer>().FromMethod(ServerAPI.CreatePlayerClient).AsSingle();

        container.Bind<User>().FromMethod(ServerAPI.CreateUserProxy).AsSingle();
    }
}