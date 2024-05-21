using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights_DoorAccess : MonoBehaviour
{
    public InteractableConditioned DoorInteractable;
    public CallAction_Door DoorCallAction;
    private MeshRenderer mr;
    public Material AccessGrantedClosedMaterial;
    public Material AccessGrantedOpenMaterial;
    public Material AccessDeniedMaterial;
    private bool AccessGranted = false;
    private string state = "Denied";

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        InvokeRepeating("CheckAccess", 1.0f, 0.2f);
    }

    private void CheckAccess()
    {
        // checks if access to door has been granted
        if (DoorCallAction.Opened)
        {
            state = "Open";
        } else if (!DoorCallAction.Opened && DoorInteractable.ItemConditionNames[0] == "NoItem")
        {
            state = "Closed";
        } else
        {
            state = "Denied";
        }
        ChangeMaterial();
    }

    private void ChangeMaterial()
    {
        if (state == "Open")
        {
            mr.material = AccessGrantedOpenMaterial;
        }
        else if (state == "Closed")
        {
            mr.material = AccessGrantedClosedMaterial;
        }
        else if (state == "Denied")
        {
            mr.material = AccessDeniedMaterial;
        }
    }
}
