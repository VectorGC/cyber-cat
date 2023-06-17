using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;

    public float moveSpeed;

    private void Awake()
    {
        TryGetComponent(out _navMeshAgent);
        Time.timeScale = 1f;
    }

    private void Update()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        var directionVector = new Vector3(horizontal, 0, vertical);
        _navMeshAgent.velocity = Vector3.ClampMagnitude(directionVector, 1) * moveSpeed;
    }
}