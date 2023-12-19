using ApiGateway.Client.Application;
using Bonsai.Core;
using UnityEngine;
using Zenject;

public class QuestGiver : Interactable, IViewInBonsaiTreeWindow
{
    [SerializeField] public BehaviourTree TreeBlueprint;
    [SerializeField] private TaskType _mustSolveTaskForInteract;

    public BehaviourTree Tree { get; private set; }
    private bool _isStarted;
    private DiContainer _container;
    private ApiGatewayClient _client;

    public override bool CanInteract => Tree != null && !Tree.IsRunning() && (_mustSolveTaskForInteract == TaskType.Tutorial || IsTaskSolved(_mustSolveTaskForInteract));

    [Inject]
    private void Construct(DiContainer container, ApiGatewayClient client)
    {
        _client = client;
        _container = container;
    }

    private bool IsTaskSolved(TaskType taskType)
    {
        if (_client.Player == null)
        {
            var verdict = _client.VerdictHistoryService.GetBestOrLastVerdict(taskType.Id());
            return verdict?.IsSuccess ?? false;
        }

        var task = _client.Player.Tasks[taskType.Id()];
        return task.IsComplete;
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