using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField] private HUDController _hud;

    public bool IsPressInteract { get; private set; }
    private float _pressTimer;

    private const float _interactDistance = 2;
    private const float _moveSpeed = 4f;

    private NavMeshAgent _navMeshAgent;


    private void Awake()
    {
        TryGetComponent(out _navMeshAgent);
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

        if (Input.GetKeyDown(KeyCode.F))
        {
            IsPressInteract = true;
            _pressTimer = 1;
        }

        if (_pressTimer > 0)
            _pressTimer -= Time.deltaTime;
        else
            IsPressInteract = false;
    }

    public bool CanInteract(Interactable interactable)
    {
        if (!interactable.CanInteract)
        {
            return false;
        }

        return Vector3.Distance(transform.position, interactable.transform.position) <= _interactDistance;
    }
}