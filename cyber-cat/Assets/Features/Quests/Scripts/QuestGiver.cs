using Bonsai.Core;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class QuestGiver : Interactable, IViewInBonsaiTreeWindow
{
    [SerializeField] private BehaviourTree _treeBlueprint;
    [SerializeField] private TaskType _task;

    public BehaviourTree Tree => _treeInstance;

    private BehaviourTree _treeInstance;

    public override bool CanInteract => true;

    void Awake()
    {
        if (_treeBlueprint)
        {
            _treeInstance = BehaviourTree.Clone(_treeBlueprint);
            _treeInstance.actor = gameObject;
        }
        else
        {
            Debug.LogError("The behaviour tree is not set for " + gameObject);
        }
    }

    void Start()
    {
        _treeInstance.blackboard.Set("task", _task);
        var keeper = TaskKeeper.FindKeeperForTask(_task);
        _treeInstance.blackboard.Set("task_keeper", keeper);
        var player = FindObjectOfType<Player>();
        _treeInstance.blackboard.Set("player", player);

        _treeInstance.Start();
        _treeInstance.BeginTraversal();
    }

    protected override UniTask OnInteract()
    {
        return UniTask.CompletedTask;
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