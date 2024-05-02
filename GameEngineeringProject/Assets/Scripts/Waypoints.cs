using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    private float waypointSize = 1f;
    
    // Draw a waypoint on the scene
    private void OnDrawGizmos()
    {
        // Create the wired sphere on every child position
        foreach(Transform t in transform)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(t.position, waypointSize);
        }

        // Draw a line between every child in ascending order
        Gizmos.color = Color.yellow;
        for(int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i+1).position);
        }

        Gizmos.DrawLine(transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position);
    }

    // Get the next waypoint after current waypoint
    public Transform GetNextWaypoint(Transform currentWaypoint)
    {
        //Starting position or repeat cycle again
        if (currentWaypoint == null || currentWaypoint.GetSiblingIndex() == transform.childCount-1)
        {
            return transform.GetChild(0);
        }
        // If it's not null than choose the next sibling in line - exclude the last
        else 
        {
            return transform.GetChild(currentWaypoint.GetSiblingIndex() + 1);
        }

    }
}
