using DefaultNamespace;
using UnityEngine;

public interface ISeviceProvider
{
    ITicketRequester TicketRequester { get; }
    ITicketRoadmapCreator TicketRoadmapCreator { get; }
    ILoadWindow LoadWindow { get; }
}

public class ServiceProvider : ISeviceProvider
{
    private static ServiceProvider _instance;
    private ISeviceProvider _seviceProviderImplementation;

    public static ServiceProvider Instance => _instance ??= new ServiceProvider();

    private ServiceProvider()
    {
        TicketRequester = new MockTicketRequester();
        TicketRoadmapCreator = GameObject.FindObjectOfType<BaseTicketRoadmapCreator>();
        LoadWindow = new LoadWindow();
    }

    public ITicketRequester TicketRequester { get; }
    public ITicketRoadmapCreator TicketRoadmapCreator { get; }
    public ILoadWindow LoadWindow { get; }
}