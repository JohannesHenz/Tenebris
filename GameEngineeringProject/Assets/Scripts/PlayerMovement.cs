using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    public bool isDown = false;  // Track whether the player is incapacitated
    public string horizontalInputAxis = "Horizontal";
    public string verticalInputAxis = "Vertical";
    public string soundButton = "";
    public string downButton = "";
    public string interactButton = "";
    public GameObject wavePrefab;
    public Material normalMaterial;
    public Material downedMaterial;
    public GameObject reviveCollider;
    public SoundManager soundManager;
    public int reviveCount = 0;

    private Rigidbody rb;
    private float playerSpeed = 6.0f;
    private Renderer renderer;
    private CharacterController controller;
    private float noiseMakerRadius = 10.0f;
    private float stepRadius = 3.0f;
    private float taskRadius = 200.0f;
    private Vector3 movementInput;
    private float stepSoundTimer = 0f;
    private float stepSoundInterval = 0.2f;

    void Start()
    {
       /////////////////////// controller = gameObject.AddComponent<CharacterController>();
        soundManager = GetComponent<SoundManager>();

        rb = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();  // Make sure your player GameObject has a Renderer component

       
    }

    void Update()
    {
        if (!isDown)
        {
            movementInput = new Vector3(Input.GetAxis(horizontalInputAxis), 0, Input.GetAxis(verticalInputAxis)).normalized;
            if (movementInput != Vector3.zero)
            {
                MovePlayer();
                HandleStepSound();
            }

        }
             
        if (Input.GetButtonDown(soundButton))
        {
            soundManager.EmitNoiseMakerSound(noiseMakerRadius);  
           
        }
       
        
    }

    private void HandleStepSound()
    {
        if (!isDown) // Check again to prevent any sounds from being emitted if down
        {
            stepSoundTimer += Time.deltaTime;
            if (stepSoundTimer >= stepSoundInterval)
            {
                soundManager.EmitStepSound(stepRadius);
                stepSoundTimer = 0f; // Reset timer
            }
        }
    }


    private void MovePlayer()
    {
        Vector3 moveVector = movementInput * playerSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + moveVector);
        // Align player to face the direction of movement
        if (moveVector != Vector3.zero)
            rb.rotation = Quaternion.LookRotation(moveVector);
    }

    void EmitWave()
    {
        GameObject wave = Instantiate(wavePrefab, transform.position, Quaternion.identity);
    }
    
    public void GetDown()
    {
        reviveCount = 10;
        gameObject.GetComponent<Interacter>().Deactivated = true;
        rb.isKinematic = true;
        reviveCollider.SetActive(true);
        isDown = true;
        renderer.material = downedMaterial;  // Change the material to the downed state
    }

    // Method to revive the player
    public void Revive()
    {
        if (reviveCount > 0)
        {
            reviveCount--;
        }
        else
        {
            rb.isKinematic = false;
            gameObject.GetComponent<Interacter>().Deactivated = false;
            isDown = false;
            renderer.material = normalMaterial;  // Revert the material to the normal state
            reviveCollider.SetActive(false);
        }
    }

    public bool getIsDown()
    {
        return isDown;
    }

}


