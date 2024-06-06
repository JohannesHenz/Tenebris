using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CallAction_Delay : MonoBehaviour, ICallAction
{
    public GameObject Target;
    public float DelayTime = 0.0f;

    public bool CallAction(GameObject sender)
    {
        Invoke("Delay", DelayTime);
        return true;
    }

    public void Delay()
    {
        Target.GetComponent<ICallAction>().CallAction(gameObject);
    }
}
