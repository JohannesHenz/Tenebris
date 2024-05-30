using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_CallAction : MonoBehaviour
{
    // calls action of target when trigger is entered
    public ICallAction Target;

    private void OnTriggerEnter(Collider other)
    {
        Target.CallAction(gameObject);
    }
}
