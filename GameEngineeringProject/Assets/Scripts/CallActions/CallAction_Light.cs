using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAction_Light : MonoBehaviour, ICallAction
{
    private Light SelfLight;
    void Start()
    {
        SelfLight = GetComponent<Light>();
    }

    public bool CallAction(string objectid)
    {
        if (SelfLight.enabled)
        {
            SelfLight.enabled = false;
        } else
        {
            SelfLight.enabled = true;
        }
        return true;
    }
}
