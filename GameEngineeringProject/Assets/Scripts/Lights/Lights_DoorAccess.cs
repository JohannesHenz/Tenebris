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
    private enum State { Denied, Closed, Open};
    private State state = State.Denied;

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
            state = State.Open;
        } else if (!DoorCallAction.Opened && DoorInteractable.ItemConditionNames[0] == "NoItem")
        {
            state = State.Closed;
        } else
        {
            state = State.Denied;
        }
        ChangeMaterial();
    }

    private void ChangeMaterial()
    {
        if (state == State.Open)
        {
            mr.material = AccessGrantedOpenMaterial;
        }
        else if (state == State.Closed)
        {
            mr.material = AccessGrantedClosedMaterial;
        }
        else if (state == State.Denied)
        {
            mr.material = AccessDeniedMaterial;
        }
    }
}
