using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    private Animator _animator = null;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            _animator.SetBool("IsOpen", true);
            //Debug.Log("The door was open.");
        }     
    }
   
    public void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            _animator.SetBool("IsOpen", false);
            //Debug.Log("The door was close.");
        }
    }
}
