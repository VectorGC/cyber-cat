using System;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField] private HUDController _hud;
    private const float _moveSpeed = 4f;
    private const float _interactDistance = 2;

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
        _interactHandler = new InteractHandler(_interactDistance, interactables, _hud, transform);
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