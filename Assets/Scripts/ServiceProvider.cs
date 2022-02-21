using UnityEngine;

public interface IServiceLocator
{
    ITicketRequester TicketRequester { get; }
    ITicketRoadmapCreator TicketRoadmapCreator { get; }
    ILoadWindow LoadWindow { get; }
}

public class ServiceLocator : IServiceLocator
{
    private static ServiceLocator _instance;
    private IServiceLocator _seviceProviderImplementation;

    public static ServiceLocator Instance => _instance ??= new ServiceLocator();

    private ServiceLocator()
    {
        TicketRequester = new MockTicketRequester();
        TicketRoadmapCreator = GameObject.FindObjectOfType<BaseTicketRoadmapCreator>();
        LoadWindow = new LoadWindow();
    }

    public ITicketRequester TicketRequester { get; }
    public ITicketRoadmapCreator TicketRoadmapCreator { get; }
    public ILoadWindow LoadWindow { get; }
}