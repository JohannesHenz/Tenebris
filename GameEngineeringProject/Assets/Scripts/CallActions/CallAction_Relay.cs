using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CallAction_Relay : MonoBehaviour, ICallAction
{
    public GameObject[] Senders; // list of sender

    public GameObject[] Targets; // list of targets

    public float Time = 0.0f; // time in seconds - 0.0 is indefinite

    private void Start()
    {
        foreach (var sender in Senders)
        {

        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawGUITexture(transform.position, );
        Gizmos.color = Color.blue; // outgoing color
        foreach (var target in Targets)
        {
            Gizmos.DrawLine(transform.position, target.transform.position);
        }
    }
    public bool CallAction()
    {
        foreach (var target in Targets)
        {
            target.GetComponent<ICallAction>().CallAction();
        }

        Debug.Log("CallAction_Relay called");
        return true;
    }
}
