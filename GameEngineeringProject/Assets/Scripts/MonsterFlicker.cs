using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFlicker : MonoBehaviour
{
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
