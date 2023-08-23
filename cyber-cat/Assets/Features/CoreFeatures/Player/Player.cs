using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField] private HUDController _hud;
    public const float InteractDistance = 2;
    private const float _moveSpeed = 4f;

    private NavMeshAgent _navMeshAgent;
    private InteractHandler _interactHandler;


    private void Awake()
    {
        TryGetComponent(out _navMeshAgent);
    }

    private void Start()
    {
        _hud.HintText = string.Empty;

        var interactables = FindObjectsOfType<Interactable>();
        _interactHandler = new InteractHandler(InteractDistance, interactables, _hud, transform);
    }

    private void Update()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        var directionVector = new Vector3(horizontal, 0, vertical);
        _navMeshAgent.velocity = Vector3.ClampMagnitude(directionVector, 1) * _moveSpeed;

        _interactHandler.OnUpdate();
    }
}