using Bonsai.Core;
using UnityEngine;
using Zenject;

public class QuestGiver : Interactable, IViewInBonsaiTreeWindow
{
    [SerializeField] public BehaviourTree TreeBlueprint;

    public BehaviourTree Tree { get; private set; }
    private bool _isStarted;
    private DiContainer _container;

    public override bool CanInteract => Tree != null && !Tree.IsRunning();

    [Inject]
    private void Construct(DiContainer container)
    {
        _container = container;
    }

    void Awake()
    {
        if (TreeBlueprint)
        {
            Tree = BehaviourTree.Clone(TreeBlueprint);
            Tree.actor = gameObject;
            foreach (var node in Tree.Nodes)
            {
                _container.Inject(node);
            }
        }
        else
        {
            Debug.LogError("The behaviour tree is not set for " + gameObject);
        }
    }

    protected override void OnInteract()
    {
        Tree.Start();
        Tree.BeginTraversal();

        _isStarted = true;
    }

    void Update()
    {
        if (_isStarted)
        {
            Tree.Update();
        }
    }

    void OnDestroy()
    {
        Destroy(Tree);
    }
}