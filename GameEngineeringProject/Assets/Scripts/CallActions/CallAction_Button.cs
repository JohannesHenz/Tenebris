using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAction_Button : MonoBehaviour, ICallAction
{
    public GameObject Target;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, Target.transform.position);
    }
    public bool CallAction(string objectid)
    {
        Target.GetComponent<ICallAction>().CallAction("");
        return true;
    }
}
