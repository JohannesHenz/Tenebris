using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAction_Breakable : MonoBehaviour, ICallAction
{
    public GameObject InteractableObject;
    public GameObject TriggerObject;

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

        isDestroyed = true;
        InteractableObject.SetActive(true);
        TriggerObject.SetActive(false);

        // Activate Oxygen Event
    }

    public void RepairObject()
    {
        // *** play repairing Sound ?  ***

        isDestroyed = false;
        InteractableObject.SetActive(false);
        TriggerObject.SetActive(true);
        // Deactivate Oxygen Event
    }
}
