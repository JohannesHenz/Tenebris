using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caller : MonoBehaviour
{
    public GameObject Target;
    public float Timer;

    // Start is called before the first frame update
    void Start()
    {
        if (Timer > 0.0f)
        {
            Invoke("CallTarget", Timer);
        }
        else
        {
            CallTarget();
        }
    }

    private void CallTarget()
    {
        Target.GetComponent<ICallAction>().CallAction(gameObject);
        //gameObject.SetActive(false);
    }
}
