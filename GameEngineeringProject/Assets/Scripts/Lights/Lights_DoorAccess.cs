using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights_DoorAccess : MonoBehaviour
{
    public InteractableConditioned DoorTarget;
    private MeshRenderer mr;
    public Material AccessGrantedMaterial;
    public Material AccessDeniedMaterial;
    private bool AccessGranted = false;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        InvokeRepeating("CheckAccess", 1.0f, 0.2f);
    }

    private void CheckAccess()
    {
        if (!AccessGranted)
        {
            // checks if access to door has been granted
            if (DoorTarget.GetComponent<InteractableConditioned>().ItemConditionNames[0] == "NoItem")
            {
                ChangeMaterial();
                AccessGranted = true;
            }
        }
    }

    private void ChangeMaterial()
    {
        mr.material = AccessGrantedMaterial;
    }
}
