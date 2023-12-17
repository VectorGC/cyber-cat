using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Zenject;

public class Player : MonoBehaviour
{
    public static bool CanInput
    {
        get
        {
            var scene = SceneManager.GetActiveScene();
            return scene.name == "Game" || scene.name == "Tutorial";
        }
    }

    public PlayerInteractHandler Interact { get; private set; }

    private const float _moveSpeed = 4f;

    private NavMeshAgent _navMeshAgent;


    private void Awake()
    {
        TryGetComponent(out _navMeshAgent);
    }

    [Inject]
    public void Construct(PlayerInteractHandler interactHandler)
    {
        Interact = interactHandler;
    }

    private void Update()
    {
        if (!CanInput)
            return;

        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        var directionVector = new Vector3(horizontal, 0, vertical);
        _navMeshAgent.velocity = Vector3.ClampMagnitude(directionVector, 1) * _moveSpeed;

        Interact.OnUpdate();
    }
}