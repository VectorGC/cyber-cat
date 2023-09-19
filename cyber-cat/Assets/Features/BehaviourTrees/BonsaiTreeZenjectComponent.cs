using Bonsai.Core;
using UnityEngine;
using Zenject;

public class BonsaiTreeZenjectComponent : MonoBehaviour, IViewInBonsaiTreeWindow
{
    [SerializeField] public BehaviourTree TreeBlueprint;

    public BehaviourTree Tree => _treeInstance;

    private BehaviourTree _treeInstance;
    private DiContainer _container;

    [Inject]
    private void Construct(DiContainer container)
    {
        _container = container;
    }

    void Awake()
    {
        if (TreeBlueprint)
        {
            _treeInstance = BehaviourTree.Clone(TreeBlueprint);
            _treeInstance.actor = gameObject;
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

    void Start()
    {
        _treeInstance.Start();
        _treeInstance.BeginTraversal();
    }

    void Update()
    {
        _treeInstance.Update();
    }

    void OnDestroy()
    {
        Destroy(_treeInstance);
    }
}