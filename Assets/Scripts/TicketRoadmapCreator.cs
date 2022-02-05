using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private TaskNodeView _taskNodeViewPrefab;
    [SerializeField] private Transform _taskNodeContainer;
    [SerializeField] private HorizontalLayoutGroup _horizontalLayoutGroupPrefab;

    private Canvas _canvas;
    private NonBinarTree _tree;

    private void Start()
    {
        _canvas = GameObject.FindObjectOfType<Canvas>();
    }

    public override void CreateRoadmap(IReadOnlyCollection<ITicket> tickets)
    {
        if (tickets.Count == 0)
        {
            return;
        }
        _tree = new NonBinarTree(tickets.ElementAt(0));
        AddChildrenToTree(tickets, 0, 0, _tree.RootNode);
        TaskNodeView taskNodeView;
        var layout = Instantiate(_horizontalLayoutGroupPrefab, Vector3.zero, Quaternion.identity, _taskNodeContainer);
        taskNodeView = Instantiate(_taskNodeViewPrefab, Vector3.zero, Quaternion.identity, layout.gameObject.transform);
        taskNodeView.UpdateView(tickets.ElementAt(0));
        CreateLayoutsWithNodes();
    }

    private void CreateLayoutsWithNodes()
    {
        List<List<NodeTree>> NodesPerLevelOfTree = new List<List<NodeTree>>();
        List<NodeTree> nodes = new List<NodeTree>();
        int level = 0;
        while (true)
        {
            nodes = _tree.GetNodesOfLevel(level);
            if (nodes.Count == 0)
            {
                break;
            }
            NodesPerLevelOfTree.Add(new List<NodeTree>());
            NodesPerLevelOfTree[level] = new List<NodeTree>();
            NodesPerLevelOfTree[level].AddRange(nodes);
            level++;
        }

        TaskNodeView taskNodeView;
        HorizontalLayoutGroup layout;
        for (int i = 0; i < level; i++)
        {
            nodes = NodesPerLevelOfTree[i];
            layout = Instantiate(_horizontalLayoutGroupPrefab, Vector3.zero, Quaternion.identity, _taskNodeContainer);
            foreach (var node in nodes)
            {
                taskNodeView = Instantiate(_taskNodeViewPrefab, Vector3.zero, Quaternion.identity, layout.gameObject.transform);
                taskNodeView.UpdateView(node.Ticket);
            }
        }
    }

    private void AddChildrenToTree(IReadOnlyCollection<ITicket> tickets, int indexOfTicket, int offset, NodeTree curNode)
    {
        var ticket = tickets.ElementAt(indexOfTicket);
        if (ticket.CountOfChildren == 0)
        {
            return;
        }

        int indexOfNewParentNode = 0;
        for (int i = 0; i < ticket.CountOfChildren; i++)
        {
            indexOfNewParentNode = indexOfTicket + i + offset + 1;
            var ticket2 = tickets.ElementAt(indexOfNewParentNode);
            AddChildrenToTree(tickets, indexOfNewParentNode, ticket.CountOfChildren - i - 1, curNode.AddNode(ticket2));
        }
    }
}
