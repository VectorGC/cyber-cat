using System.Linq;
using Authentication;
using TasksData.Requests;
using UnityEngine;
using WebRequests.Requesters;

public class GlobalMapStartup : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Hello Cyber Cat");

        var serviceProvider = ServiceLocator.Instance;
        //var tickets2 = serviceProvider.TicketRequester.GetTickets();

        new GetTasksRequest(TokenSession.FromPlayerPrefs()).SendWWWGetObject(tickets =>
        {
            GlobalMap.OpenTaskList(tickets);
            //serviceProvider.TicketRoadmapCreator.CreateRoadmap(tickets);
        });

        // Uncomment to test load window.
        //serviceProvider.LoadWindow.StartLoading();
    }
}