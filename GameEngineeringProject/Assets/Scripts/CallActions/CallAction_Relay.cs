using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.UI;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CallAction_Relay : MonoBehaviour, ICallAction
{
    public GameObject[] Senders; // list of sender
    public GameObject[] Targets; // list of targets

    public Dictionary<int, bool> CallTimes = new Dictionary<int, bool>();

    public float Timer = 0.0f; // time in seconds - 0.0 is indefinite - 0.1 is fast 0.3 is slow
    public bool Concurrent = false; // switch to true if all senders have to be active at the same time

    private bool TimeWindowOpen = false;

    public bool FireOnce = false;

    private void Start()
    {
        if (FireOnce)
        {
            this.CallAction(gameObject);
        }

        if (Concurrent)
        {
            // if concurrency of all callActions is required get all Senders unique id
            foreach (var sender in Senders)
            {
                CallTimes.Add(sender.GetInstanceID(), false);
            }
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawGUITexture(transform.position, );
        Gizmos.color = Color.yellow; // outgoing color
        if (Concurrent) {Gizmos.color = Color.blue;} // overload for concurrent
        
        foreach (var target in Targets)
        {
            Gizmos.DrawLine(transform.position, target.transform.position);
        }
    }

    public bool CallAction(GameObject sender)
    {
        if (Concurrent)
        {
            Debug.Log("CallAction called with id: " + sender.GetInstanceID());

            CallTimes[sender.GetInstanceID()] = true;

            if (!TimeWindowOpen && Timer > 0.0f)
            {
                TimeWindowOpen = true;
                // only reset if timer is on a positive number
                Invoke("ResetTimer", Timer);
            }

            if (CheckConcurrency())
            {
                foreach (var target in Targets)
                {
                    target.GetComponent<ICallAction>().CallAction(gameObject);
                }
            }
        }
        else
        {
            foreach (var target in Targets)
            {
                target.GetComponent<ICallAction>().CallAction(gameObject);
            }
        }
        return true;
    }

    private bool CheckConcurrency()
    {
        foreach (var entry in CallTimes.Keys.ToList())
        {
            if (!CallTimes[entry]) return false;
        }
        return true;
    }

    private void ResetTimer()
    {
        foreach (var entry in CallTimes.Keys.ToList())
        {
            CallTimes[entry] = false;
        }
        Debug.Log("CallTimes reset");
        TimeWindowOpen = false;
    }
}
