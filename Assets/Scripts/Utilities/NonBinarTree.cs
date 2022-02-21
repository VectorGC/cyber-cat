using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonBinarTree
{
    private List<NodeTree> _nodesBuffer;

    public NodeTree RootNode
    {
        get;
        private set;
    }

    public NodeTree AddNodeAfterNode(ITaskTicket ticket, NodeTree node)
    {
        return node.AddNode(ticket);
    }

    public NodeTree FindNodeOfId(int id)
    {
        if (id < 0)
        {
            return null;
        }
        return RootNode.FindNodeOfIdInChildren(id);
    }

    private NonBinarTree()
    {
    }

    public NonBinarTree(ITaskTicket ticket)
    {
        RootNode = new NodeTree(ticket);
    }

    public List<NodeTree> GetNodesOfLevel(int i)
    {
        _nodesBuffer = new List<NodeTree>();
        FindNodesOfLevel(i, 0, RootNode);
        return _nodesBuffer;
    }

    private void FindNodesOfLevel(int i, int curLevel, NodeTree curNode)
    {
        if (curLevel == i)
        {
            _nodesBuffer.Add(curNode);
            return;
        }

        for (int j = 0; j < curNode.NextNodes.Count; j++)
        {
            FindNodesOfLevel(i, curLevel + 1, curNode.NextNodes[j]);
        }
    }
}

public class NodeTree
{
    public ITaskTicket Ticket
    {
        get;
        private set;
    }

    public List<NodeTree> NextNodes
    {
        get;
        private set;
    }

    public NodeTree AddNode(ITaskTicket ticket)
    {
        var node = new NodeTree(ticket);
        NextNodes.Add(node);
        return node;
    }

    public NodeTree FindNodeOfIdInChildren(int i)
    {
        if (Ticket.Id == i)
        {
            return this;
        }
        foreach (var node in NextNodes)
        {
            var n = node.FindNodeOfIdInChildren(i);
            if (n != null)
            {
                return n;
            }
        }
        return null;
    }

    public NodeTree(ITaskTicket ticket)
    {
        Ticket = ticket;
        NextNodes = new List<NodeTree>();
    }
}
