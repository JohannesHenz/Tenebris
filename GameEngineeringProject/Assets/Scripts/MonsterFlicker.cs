using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFlicker : MonoBehaviour
{

    //****************************** JOHANNES CODE START ****************************//
    public float maxFlickerFrequency = 5f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LightsFlicker lightsFlicker = other.GetComponentInChildren<LightsFlicker>();
            if (lightsFlicker != null)
            {
                float distance = Vector3.Distance(transform.position, other.transform.position);
                float frequency = Mathf.Lerp(maxFlickerFrequency, 1f, distance/ GetComponent<SphereCollider>().radius);
                lightsFlicker.SetFlickerFrequency(frequency);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LightsFlicker lightsFlicker = other.GetComponentInChildren<LightsFlicker>();
            if (lightsFlicker != null)
            {
                lightsFlicker.SetFlickerFrequency(1f);
            }
        }
    }

    //******************* JOHANNES CODE END *******************************
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FlickerLight"))
        {
            Debug.Log("Trigger triggered!");
        } else
        {
            Debug.Log("Trigger untriggered!");
        }
    }
}
