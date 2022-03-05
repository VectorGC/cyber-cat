using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    Shot shot;
    Cursor cursor;
    NavMeshAgent navMeshAgent;

    public Transform gunBarrel;

    public float moveSpeed;
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        shot = FindObjectOfType<Shot>();
        cursor = FindObjectOfType<Cursor>();       
        navMeshAgent = GetComponent<NavMeshAgent>();

        //navMeshAgent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        Vector3 directionVector = new Vector3(horizontal, 0, vertical);

        navMeshAgent.velocity = Vector3.ClampMagnitude(directionVector, 1) * moveSpeed;
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(directionVector), Time.deltaTime * rotationSpeed);

        
        if (Input.GetMouseButtonDown(0))
        {
            var from = gunBarrel.position;
            var target = cursor.transform.position;
            var to = new Vector3(target.x, from.y, target.z);

            var direction = (to - from).normalized;

            RaycastHit hit;
            if (Physics.Raycast(from, to - from, out hit, 100))
            {
                to = new Vector3(hit.point.x, from.y, hit.point.z);
                if (hit.transform != null)
                {
                    var zombie = hit.transform.GetComponent<Zombie>();
                    if (zombie != null)
                        zombie.Kill();
                }
            }
            else
                to = from + direction * 100;

            shot.Show(from, to);
        }
    }
}
