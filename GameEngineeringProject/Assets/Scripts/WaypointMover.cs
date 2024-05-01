using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointMover : MonoBehaviour
{
    // Stores a reference to the waypoints system
    [SerializeField] private Waypoints waypoints;
    public NavMeshAgent agent;

    private Transform currentWaypoint;

    public float distanceThreshold = 1.9f;

    private void Update()
    {

        // If object is close enough to destination than choose the next one
        if (agent.remainingDistance <= distanceThreshold && GetComponent<MonsterController>().currentState == State.Roaming)
        {
            // Set the next waypoint target
            currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
            MoveObject();
        }
    }


    //Start to roam around
    public void StartRoaming()
    {
        // Set position to start waypoint
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        transform.position = currentWaypoint.position;

        // Set the next waypoint target
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        MoveObject();
    }

    private void MoveObject()
    {
        // Move Object along the path
        agent.SetDestination(currentWaypoint.position);
    }
}
