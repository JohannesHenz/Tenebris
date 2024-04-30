using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    
    public NavMeshAgent agent;

    //Teest variables
    public GameObject noiseMaker;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            StartCoroutine("MakeSound");
        }
        
    }

    //Check if monster is able to hear the made sound
    private void OnTriggerEnter(Collider other)
    {
        //If monster ever collide with triggerSound it will pursuite the noise source
        if (other.gameObject.CompareTag("NoiseMaker"))
        {
            agent.SetDestination(other.gameObject.GetComponent<Transform>().position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player dead");
            collision.gameObject.GetComponent<PlayerMovement>().GetDown();
        }
    }


    //TestSound: Generate Sound Function - just implemented for testing purposes
    public IEnumerator MakeSound()
    {
        //Player generates Noice
        noiseMaker.SetActive(true);
        yield return new WaitForSeconds(1);
        noiseMaker.SetActive(false);
    }
}
