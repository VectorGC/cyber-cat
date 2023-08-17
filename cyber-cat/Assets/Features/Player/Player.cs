using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField] private float _taskKeeperInteractDistance = 2;
    public float TaskKeeperInteractDistance => _taskKeeperInteractDistance;

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

        if (Input.GetKeyDown(KeyCode.F) && HackerVisionSingleton.Instance.Active)
        {
            var keepers = FindObjectsOfType<TaskKeeper>();
            foreach (var keeper in keepers)
            {
                if (Vector3.Distance(transform.position, keeper.transform.position) < _taskKeeperInteractDistance)
                {
                    CodeEditor.Open(keeper.Task.Id());
                }
            }
        }
    }
}