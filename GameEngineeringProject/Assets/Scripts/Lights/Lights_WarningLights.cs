using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights_WarningLights : MonoBehaviour
{
    public bool Active = true;
    // Update is called once per frame
    public float RotationSpeed = 360.0f;

    void Update()
    {
        if (Active)
        {
            transform.Rotate(Vector3.left, RotationSpeed * Time.deltaTime);
        }
    }
}
