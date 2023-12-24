using ApiGateway.Client.Application;
using Features.ServerConfig;
using Zenject;

public static class ServerAPIInstaller
{
    public static void InstallBindings(DiContainer container)
    {
        var client = new ApiGatewayClient(ServerAPI.ServerEnvironment);
        container.BindInterfacesAndSelfTo<ApiGatewayClient>().FromInstance(client).AsSingle();
    }
}