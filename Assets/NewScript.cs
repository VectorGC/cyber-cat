using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewScript : MonoBehaviour
{
    public Trigger startTrigger;
    public KeyCode a_activated;
    public Trigger w_trigger;

    // Start is called before the first frame update
    void Start()
    {
     // if (startTrigger.Activated)

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(a_activated))
        {
           // w_trigger.
        }
    }
}
