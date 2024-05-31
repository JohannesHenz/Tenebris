using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointMover : MonoBehaviour
{
    // Stores a reference to the waypoints system
    [SerializeField] private Waypoints waypoints;
    public NavMeshAgent agent;

    private Transform currentWaypoint = null;

    public float distanceThreshold = 1f;

    private void Update()
    {


        // If object is close enough to destination than choose the next one
        if (GetComponent<MonsterController>().currentState == State.Roaming && agent.remainingDistance <= distanceThreshold)
        {
            // Set the next waypoint target
            currentWaypoint = waypoints.GetRandomWaypoint(currentWaypoint);
            MoveObject();
        }
    }


    //Start to roam around
    public void StartRoaming()
    {
        // Set position to start waypoint
        currentWaypoint = waypoints.GetRandomWaypoint(currentWaypoint);
        transform.position = currentWaypoint.position;

        // Set the next waypoint target
        currentWaypoint = waypoints.GetRandomWaypoint(currentWaypoint);
        MoveObject();
    }

    private void MoveObject()
    {
        // Move Object along the path
        GetComponent<MonsterController>().TrySetDestination(currentWaypoint.position);
        //agent.SetDestination(currentWaypoint.position);
    }
}
