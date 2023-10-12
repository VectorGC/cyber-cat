using ApiGateway.Client.Models;
using Features.ServerConfig;
using Zenject;

public static class ServerAPIInstaller
{
    public static void InstallBindings(DiContainer container)
    {
        container.BindAsync<IUser>().FromMethod(ServerAPI.CreateUserClient);
        container.BindAsync<IPlayer>().FromMethod(ServerAPI.CreatePlayerClient).AsSingle();
    }
}