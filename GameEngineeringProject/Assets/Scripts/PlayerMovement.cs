using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    public bool isDown = false;  // Track whether the player is incapacitated
    public string horizontalInputAxis = "Horizontal";
    public string verticalInputAxis = "Vertical";
    public string soundButton = "";
    public string interactButton = "";
    public GameObject wavePrefab;
    public Material normalMaterial;
    public Material downedMaterial;
    public GameObject reviveCollider;
    public SoundManager soundManager;
    public int reviveCount = 0;


    [SerializeField] private float Acceleration = 5.0f;
    [SerializeField] private float Deceleration = 5.0f;
    [SerializeField] private float MaxSpeed = 5.0f;
    private Rigidbody RigidBody;

    private Renderer renderer;
    private Vector3 movementInput;
    private float stepSoundTimer = 0f;
    private float stepSoundInterval = 0.5f;
    private bool isWalking = false;

    //private enum SoundType { Task, Step, NoiseMaker };

    void Start()
    {
        // Get the necessary Components
        soundManager = GetComponent<SoundManager>();
        //rb = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
        RigidBody = GetComponent<Rigidbody>();

        InvokeRepeating("HandleStepSound", 0f, stepSoundInterval);
    }

    void LateUpdate()
    {
        if (!isDown) //Don't Move or Make StepSound if player is down
        {
            movementInput = new Vector3(Input.GetAxis(horizontalInputAxis), 0, Input.GetAxis(verticalInputAxis)).normalized;
            if (movementInput != Vector3.zero)
            {
                // Move Player
                isWalking = true;
                RigidBody.AddForce(movementInput * Acceleration, ForceMode.Force);
                // clamp speed to maxspeed
                if (RigidBody.velocity.magnitude > MaxSpeed) RigidBody.velocity = RigidBody.velocity.normalized * MaxSpeed;
                // Align player to face the direction of movement
                RigidBody.rotation = Quaternion.LookRotation(movementInput);
            } else
            {
                // if no input is given, decelerate
                RigidBody.AddForce(RigidBody.velocity * -Deceleration, ForceMode.Force);
                isWalking = false;
            } 
        }
             
        if (Input.GetButtonDown(soundButton)) //NoisMakerSound 
        {
            soundManager.EmitSound(SoundType.NoiseMaker);  
        }        
    }

    private void HandleStepSound()
    {
        if (!isDown && isWalking) // Check again to prevent any sounds from being emitted if down
        {
            soundManager.EmitSound(SoundType.Step);
        }
    }

    void EmitWave()
    {
        GameObject wave = Instantiate(wavePrefab, transform.position, Quaternion.identity);
    }
    
    public void GetDown()
    {
        reviveCount = 10;
        gameObject.GetComponent<Interacter>().Deactivated = true;
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


