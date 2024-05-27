using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFlicker : MonoBehaviour
{

    //****************************** JOHANNES CODE START ****************************//
    public float maxFlickerFrequency = 5f;
    private SphereCollider sphereCollider;

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LightsFlicker lightsFlicker = other.GetComponentInChildren<LightsFlicker>();
            if (lightsFlicker != null && other.transform.Find("Flashlight") != null)
            {
                Debug.Log("Flashlight detected in range.");
                float distance = Vector3.Distance(transform.position, other.transform.position);
                float frequency = Mathf.Lerp(maxFlickerFrequency, 1f, distance/ GetComponent<SphereCollider>().radius);
                lightsFlicker.SetFlickerFrequency(frequency);
                lightsFlicker.StartFlickering();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LightsFlicker lightsFlicker = other.GetComponentInChildren<LightsFlicker>();
            if (lightsFlicker != null && other.transform.Find("Flashlight") != null) 
            {
                Debug.Log("Flashlight out of range.");
                lightsFlicker.StopFlickering();
            }
        }
    }

    //******************* JOHANNES CODE END *******************************
 
}
