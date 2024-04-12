using UnityEngine;
using UnityEngine.AI;

public class MovementAnimator : MonoBehaviour
{
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] Animator animator;

    private Vector2 _velocity;
    private Vector2 _smoothDeltaPosition;

    private void Start()
    {
        navMeshAgent.updatePosition = false;
        navMeshAgent.updateRotation = true;
    }

    private void Update()
    {
        SynchronizationAnimatorAndAgent();
    }

    private void OnAnimatorMove()
    {
        var rootPosition = animator.rootPosition;
        rootPosition.y = navMeshAgent.nextPosition.y;
        transform.position = rootPosition;
        //transform.rotation = animator.rootRotation;
        navMeshAgent.nextPosition = rootPosition;
    }

    private void SynchronizationAnimatorAndAgent()
    {
        var worldDeltaPosition = transform.position - navMeshAgent.nextPosition;
        worldDeltaPosition.y = 0;

        var dx = Vector3.Dot(transform.right, worldDeltaPosition);
        var dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        var deltaPosition = new Vector2(dx * -1, dy * -1);

        var smooth = Mathf.Min(1f, Time.deltaTime / 0.1f);
        _smoothDeltaPosition = Vector2.Lerp(_smoothDeltaPosition, deltaPosition, smooth);

        _velocity = _smoothDeltaPosition / Time.deltaTime;
        /*
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            _velocity = Vector2.Lerp(Vector2.zero, _velocity, navMeshAgent.remainingDistance / navMeshAgent.stoppingDistance);
        }
        */

        animator.SetFloat("speed", navMeshAgent.velocity.magnitude);
        animator.SetFloat("velx", _velocity.x);
        animator.SetFloat("vely", _velocity.y);

        var deltaMagnitude = worldDeltaPosition.magnitude;
        if (deltaMagnitude > navMeshAgent.radius / 2)
        {
            transform.position = Vector3.Lerp(animator.rootPosition, navMeshAgent.nextPosition, smooth);
        }
    }
}