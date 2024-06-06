using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentIdleTime : MonoBehaviour
{
    public NavMeshAgent agent;
    private float idleTime = 0.0f;
    private bool isIdle = false;
    private float incrementInterval = 1.0f;
    private float maxIdleTime = 5.0f;
    private WaypointMover wayPointMover;
    public bool ableToTeleport = true;

    public AudioSource monsterScream;



    private void Start()
    {
        wayPointMover = GameObject.Find("Monster").GetComponent<WaypointMover>();
    }

    void Update()
    {
        // Check if  agent has a path and is moving
        if (agent.hasPath && agent.velocity.sqrMagnitude > 0.1f)
        {
            // Agent is moving, reset the idle timer
            isIdle = false;
            idleTime = 0.0f;
            CancelInvoke("IncrementIdleTime");
        }
        else if(agent.hasPath && agent.velocity.sqrMagnitude <=  0.1f && isIdle == false)
        {
            //Starting to count
            isIdle = true;

            InvokeRepeating("IncrementIdleTime", 1.0f, incrementInterval);
        }
    }


    public void IncrementIdleTime()
    {

        idleTime += incrementInterval;

        if (ableToTeleport && idleTime >= maxIdleTime)
        {
            wayPointMover.StartRoaming();
            monsterScream.Play();
        }
    }

    public void toggleTeleport()
    {
        ableToTeleport = !ableToTeleport;
    }
}

