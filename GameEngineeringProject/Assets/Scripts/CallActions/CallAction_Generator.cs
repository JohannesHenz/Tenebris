using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAction_Generator : MonoBehaviour, ICallAction
{
    public GameObject Target;    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, Target.transform.position);
    }
    public bool CallAction()
    {
        Debug.Log("Generator active");
        Target.GetComponent<ICallAction>().CallAction();
        return true;
    }

}
