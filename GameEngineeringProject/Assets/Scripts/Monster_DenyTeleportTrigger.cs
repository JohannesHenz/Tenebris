using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Monster_DenyTeleportTrigger : MonoBehaviour
{
    public NavMeshAgentIdleTime Idler;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            other.GetComponent<NavMeshAgentIdleTime>().ableToTeleport = false; ;
            Debug.Log("Monster teleport denied");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            other.GetComponent<NavMeshAgentIdleTime>().ableToTeleport = true; ;
            Debug.Log("Monster teleport activated");
        }
    }
}
