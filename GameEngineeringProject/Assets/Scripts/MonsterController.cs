using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum State { Roaming, Chasing, Checkout}

public class MonsterController : MonoBehaviour
{
    // Variables 
    public NavMeshAgent agent;
    private GameObject target;
    private float hearingRadius = 15.0f;
    [SerializeField] public State currentState;


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
        /*/------- PLayer Test ----------
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine("NoiseMaker");
        }
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine("TaskSound");
        }

        //---------------------------*/


        // ************ Define states ****************

        // The monster will checkout the source of the made noice
        if (currentState == State.Checkout && agent.remainingDistance <= GetComponent<WaypointMover>().distanceThreshold)
        {
            Debug.Log("Monster is returning to pathway");
            agent.ResetPath();
            currentState = State.Roaming;
        }
        // Monster is retreates to lates waypoint if player is to far away
        else if (currentState == State.Chasing && agent.remainingDistance > hearingRadius)
        {
            Debug.Log("Monster is bored");
            agent.ResetPath();
            currentState = State.Roaming;
        }
        // If player is in hearing range - chase the player
        else if (currentState == State.Chasing)
        {
            agent.SetDestination(target.GetComponent<Transform>().position);
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
            agent.SetDestination(other.gameObject.GetComponent<Transform>().position);
            currentState = State.Checkout;
        }
        else if (other.gameObject.CompareTag("TaskSound") && currentState != State.Checkout)
        {
            //Second Priority are Tasks - monster wants to check them out and destroy them
            agent.SetDestination(other.gameObject.GetComponent<Transform>().position);
            currentState = State.Checkout;
        }
        else if (other.gameObject.CompareTag("StepSound") && currentState != State.Checkout)
        {
            //Third Priority are steps - due to the fact that the monster is blind and steps aren't very loud
            currentState = State.Chasing;
            target = other.gameObject;
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
            //Get Down disbales StepSound
            currentState = State.Roaming;
            agent.isStopped = true;
            agent.ResetPath();
        }
    }





    //----------------------------Player Test ---------------------
    /*public GameObject noiseMaker;
    public GameObject taskSound;
    IEnumerator NoiseMaker()
    {
        noiseMaker.SetActive(true);
        yield return new WaitForSeconds(1);
        noiseMaker.SetActive(false);
    }

    IEnumerator TaskSound()
    {
        taskSound.SetActive(true);
        yield return new WaitForSeconds(1);
        taskSound.SetActive(false);
    }*/
}
