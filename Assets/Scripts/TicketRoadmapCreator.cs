using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public interface ITicketRoadmapCreator
{
    void CreateRoadmap(IReadOnlyCollection<ITicket> tickets);
}

public abstract class BaseTicketRoadmapCreator : MonoBehaviour, ITicketRoadmapCreator
{
    public abstract void CreateRoadmap(IReadOnlyCollection<ITicket> tickets);
}

public class TicketRoadmapCreator : BaseTicketRoadmapCreator
{
    public TaskNodeView TaskNodeViewPrefab;
    public Transform TaskNodeContainer;

    private Canvas _canvas;

    private void Start()
    {
        _canvas = GameObject.FindObjectOfType<Canvas>();
    }

    public override void CreateRoadmap(IReadOnlyCollection<ITicket> tickets)
    {
        foreach (var ticket in tickets)
        {
            var taskNodeView = Instantiate(TaskNodeViewPrefab,  Vector3.zero, Quaternion.identity, TaskNodeContainer);
            taskNodeView.UpdateView(ticket);
        }
    }
}
