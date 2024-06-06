using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_CallAction : MonoBehaviour
{
    // calls action of target when trigger is entered
    public GameObject Target;
    private bool isTriggerActive = true;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, Target.transform.position);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster") && isTriggerActive)
        {
            Target.GetComponent<ICallAction>().CallAction(gameObject);
            Debug.Log("Triggered by " + other);

            isTriggerActive = false;

            Invoke("ActivateTrigger", 60.0f);
        }
    }

    private void ActivateTrigger()
    {
        isTriggerActive = true;
    }
}
