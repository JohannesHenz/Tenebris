using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAction_Breakable : MonoBehaviour, ICallAction
{
    public MeshRenderer mesh;
    public GameObject InteractableObject;

    public bool isDestroyed = false;

    private void Start()
    {
        InteractableObject.SetActive(false);
    }

    public bool CallAction(GameObject sender)
    {
        if (!isDestroyed)
        {
            BreakObject();
        } else
        {
            RepairObject();
        }
        return true;
    }

    public void BreakObject()
    {
        // *** play breaking Sound ***
        //...

        mesh.enabled = false;
        isDestroyed = true;

        // Activate Oxygen Event
    }

    public void RepairObject()
    {
        // *** play repairing Sound ?  ***
        //...

        mesh.enabled = true;
        isDestroyed = false;

        // Deactivate Oxygen Event
    }
}
