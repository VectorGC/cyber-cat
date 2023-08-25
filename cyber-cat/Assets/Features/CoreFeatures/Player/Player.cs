using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField] private HUDController _hud;

    public InteractPosibilityHandler InteractPosibility { get; private set; }

    private const float _moveSpeed = 4f;

    private NavMeshAgent _navMeshAgent;


    private void Awake()
    {
        TryGetComponent(out _navMeshAgent);

        var interactables = FindObjectsOfType<Interactable>();
        InteractPosibility = new InteractPosibilityHandler(interactables, _hud, transform);
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

        InteractPosibility.OnUpdate();
    }
}