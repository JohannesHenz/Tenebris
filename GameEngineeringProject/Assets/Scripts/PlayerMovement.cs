using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float speed = 20.0f;
    public bool isDown = false;  // Track whether the player is incapacitated
    public string horizontalInputAxis = "Horizontal";
    public string verticalInputAxis = "Vertical";
    public string soundButton = "";
    public string downButton = "";
    public string interactButton = "";
    private Rigidbody rb;
    public GameObject wavePrefab;
    public Material normalMaterial;
    public Material downedMaterial;
    private Renderer renderer;
    private CharacterController controller;
    private float playerSpeed = 4.0f;
    public GameObject reviveCollider;


    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();

        //rb = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();  // Make sure your player GameObject has a Renderer component

       
    }

    void Update()
    {
        if (!isDown)
        {

        Vector3 move = new Vector3(Input.GetAxis(horizontalInputAxis), 0, Input.GetAxis(verticalInputAxis));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }


        }
              // Skip movement updates if the player is down
        if (Input.GetButtonDown(soundButton))
        {
            EmitWave();
        }
       
        
    }

    void EmitWave()
    {
        GameObject wave = Instantiate(wavePrefab, transform.position, Quaternion.identity);
    }
    
    public void GetDown()
    {
        gameObject.GetComponent<Interacter>().Deactivated = true;
        reviveCollider.SetActive(true);
        isDown = true;
        renderer.material = downedMaterial;  // Change the material to the downed state
    }

    // Method to revive the player
    public void Revive()
    {
        gameObject.GetComponent<Interacter>().Deactivated = false;
        isDown = false;
        renderer.material = normalMaterial;  // Revert the material to the normal state
        reviveCollider.SetActive(false);

    }

}


