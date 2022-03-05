using System.Linq;
using Authentication;
using TasksData.Requests;
using UniRx;
using UnityEngine;
using WebRequests.Requesters;

public class GlobalMapStartup : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Hello Cyber Cat");
        
        new GetTasksRequest(TokenSession.FromPlayerPrefs()).SendWWWGetObject().Subscribe(tickets =>
        {
            GlobalMap.OpenTaskList(tickets);
            //serviceProvider.TicketRoadmapCreator.CreateRoadmap(tickets);
        });

        // Uncomment to test load window.
        //serviceProvider.LoadWindow.StartLoading();
    }
}