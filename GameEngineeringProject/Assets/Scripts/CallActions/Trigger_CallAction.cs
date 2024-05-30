using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_CallAction : MonoBehaviour
{
    // calls action of target when trigger is entered
    public GameObject Target;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, Target.transform.position);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Target.GetComponent<ICallAction>().CallAction(gameObject);
            Debug.Log("Triggered by " + other);
        }
    }
}
