using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Player : MonoBehaviour
{
    [SerializeField] private HUDController _hud;

    public InteractHandler Interact { get; private set; }

    private const float _moveSpeed = 4f;

    private NavMeshAgent _navMeshAgent;


    private void Awake()
    {
        TryGetComponent(out _navMeshAgent);

        var interactables = FindObjectsOfType<Interactable>();
        Interact = new InteractHandler(interactables, _hud, transform);
    }

    [Inject]
    public void Construct(Interactable interactable)
    {
        var t = 10;
    }

    private void Start()
    {
        _hud.HintText = string.Empty;
    }

    private void Update()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        var directionVector = new Vector3(horizontal, 0, vertical);
        _navMeshAgent.velocity = Vector3.ClampMagnitude(directionVector, 1) * _moveSpeed;

        Interact.OnUpdate();
    }
}