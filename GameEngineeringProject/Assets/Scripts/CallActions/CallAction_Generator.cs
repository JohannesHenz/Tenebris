using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAction_Generator : MonoBehaviour, ICallAction
{
    public bool CallAction()
    {
        Debug.Log("Generator active");
        return false;
    }

}
