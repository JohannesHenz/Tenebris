using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum State { Roaming, Chasing, Checkout}

public class MonsterController : MonoBehaviour
{
    // Variables 
    public NavMeshAgent agent;
    private float roamingSpeed = 4.5f;
    private float chasingCheckoutSpeed = 6.0f;
    private GameObject target;
     public SphereCollider hearingCollider; 
    [SerializeField] public State currentState;

    //Layer that count as obstacle
    public LayerMask obstacleLayer;
    private float checkingValidPathTimer = 2.0f;


    // Start is called before the first frame update
    void Start()
    {
        currentState = State.Roaming;
        GetComponent<WaypointMover>().StartRoaming();

        //To handle pyhsic engine and nav mesh
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; 
        }
    }

    // Update is called once per frame
    void Update()
    {

        // ************ Define states ****************
        // The monster will checkout the source of the made noice
        if (currentState == State.Checkout && agent.remainingDistance <= GetComponent<WaypointMover>().distanceThreshold)
        {
            Debug.Log("Monster is returning to pathway");
            ReturnRoaming();
        }
        // Monster is retreates to lates waypoint if player is to far away
        else if (currentState == State.Chasing && agent.remainingDistance > hearingCollider.radius)
        {
            Debug.Log("Monster is bored");
            ReturnRoaming();
        }
        // If player is in hearing range - chase the player
        else if (currentState == State.Chasing)
        {
            if(target != null)
            {
                TrySetDestination(target.GetComponent<Transform>().position);
                //agent.SetDestination();
                Debug.Log("chase");
            }            
        }

        // *******************************************

    }

    // ******************* React to different kinds of sound *******************
    // Checks if monster is able to hear specific sound
    private void OnTriggerStay(Collider other)
    {

        // If monster ever collide with triggerSound it will pursuite the noise source

        if (other.gameObject.CompareTag("NoiseMaker"))
        {
            // First priority is the Noice Maker to leave player option to distract monster
            if (TrySetDestination(other.gameObject.GetComponent<Transform>().position))
            {
                //agent.SetDestination();
                currentState = State.Checkout;
                agent.speed = chasingCheckoutSpeed;
            }
            
        }
        else if (other.gameObject.CompareTag("TaskSound") && currentState != State.Checkout)
        {
            //Second Priority are Tasks - monster wants to check them out and destroy them
            if (TrySetDestination(other.gameObject.GetComponent<Transform>().position))
            {
                //agent.SetDestination();
                currentState = State.Checkout;
                agent.speed = chasingCheckoutSpeed;
            }
        }
        else if (other.gameObject.CompareTag("StepSound") && currentState != State.Checkout)
        {
            //Third Priority are steps - due to the fact that the monster is blind and steps aren't very loud
            currentState = State.Chasing;
            agent.speed = chasingCheckoutSpeed;
            target = other.gameObject;
        }

        // Cecks if it collides with something breakable
        if (other.gameObject.CompareTag("Breakable"))
        {
            if (!other.gameObject.GetComponent<BreakSelf>().isDestroyed)
            {
                other.gameObject.GetComponent<BreakSelf>().BreakObject();
            }
            
        }
    }
    // *******************************************************************

    // Check if monster collided with player
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player dead");
            collision.gameObject.GetComponent<PlayerMovement>().GetDown();
            
            StartCoroutine("PostponeSetRoaming");

        }
    }


    // Postpone setting state to roaming to handle stepSound disappearing
    private IEnumerator PostponeSetRoaming()
    {
        yield return new WaitForSeconds(0.5f);
        ReturnRoaming();

    }

    private void ReturnRoaming()
    {
        agent.ResetPath();
        currentState = State.Roaming;
        agent.speed = roamingSpeed;
    }

    //Calculates if set Destination is even possible
    public bool TrySetDestination(Vector3 destination )
    {
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(destination, path);

        if (path.status == NavMeshPathStatus.PathComplete && CheckPathForehandIfWalkable(path))
        {
            //Check if Path is still valid while opening and closing doors
            StartCoroutine(IsPathObstructed(path));

            agent.SetDestination(destination);

            return true;
            //Debug.Log("Valid path set");
        }
        else
        {
            //Debug.Log("Invalid path set");
            return false;
        }
    }

    //Currently not able to solve problem diffrently
    private bool CheckPathForehandIfWalkable(NavMeshPath path){
        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Vector3 start = path.corners[i];
            Vector3 end = path.corners[i + 1];

            if (Physics.Raycast(start, (end - start).normalized, out RaycastHit hit, Vector3.Distance(start, end), obstacleLayer))
            {
                //Path blocked by something / Monster returns roaming;
                Debug.Log("Break current path");

                return false;
            }
        }

        return true;
    }



    //Checks if Obstacle is on the given path which makes it impossible to reach destination
    IEnumerator IsPathObstructed(NavMeshPath path)
    {
        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Vector3 start = path.corners[i];
            Vector3 end = path.corners[i + 1];

            yield return new WaitForSeconds(checkingValidPathTimer);

            if (Physics.Raycast(start, (end - start).normalized, out RaycastHit hit, Vector3.Distance(start, end), obstacleLayer))
            {
                //Path blocked by something / Monster returns roaming;
                Debug.Log("Break current path");

                StopCoroutine(IsPathObstructed(path));
                ReturnRoaming();

                yield break;
            }
        }
        
    }



}
