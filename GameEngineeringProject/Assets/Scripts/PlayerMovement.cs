using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using UnityEngine.UI;


[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public static List<GameObject> instantiatedPlayers = new List<GameObject>(); // List der Instanzierten Players

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

    private CharacterController controller;
    private Renderer _re;
    private Vector3 movementInput = Vector3.zero;
    private float stepSoundInterval = 0.8f;
    private bool isWalking = false;

    private AudioSource StepSound;
    private Interacter interacter;
    public AudioSource DeathSound;
    public AudioSource HeavyBreathing;
   


    public GameObject playerPrefab; //Zum Spawnen



    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        // Get the necessary Components
        soundManager = GetComponent<SoundManager>();
        //rb = GetComponent<Rigidbody>();
        _re = GetComponent<Renderer>();
        RigidBody = GetComponent<Rigidbody>();
        interacter = GetComponent<Interacter>();
        StepSound = GetComponent<AudioSource>();

        InvokeRepeating("HandleStepSound", 0f, stepSoundInterval);
    }

    void LateUpdate()
    {
        if (!isDown) //Don't Move or Make StepSound if player is down
        {
            //movementInput = new Vector3(Input.GetAxis(horizontalInputAxis), 0, Input.GetAxis(verticalInputAxis)).normalized;
            
            if (movementInput != Vector3.zero)
            {
                // Move Player
                isWalking = true;
                RigidBody.AddForce(movementInput * Acceleration, ForceMode.Force);
                // clamp speed to maxspeed
                if (RigidBody.velocity.magnitude > MaxSpeed)
                {
                    RigidBody.velocity = RigidBody.velocity.normalized * MaxSpeed;
                }
                // Align player to face the direction of movement
                RigidBody.rotation = Quaternion.LookRotation(movementInput);
            } else
            {
                // if no input is given, decelerate

                //RigidBody.AddForce(RigidBody.velocity * -Deceleration, ForceMode.Force);
                RigidBody.velocity = Vector3.Lerp(RigidBody.velocity, Vector3.zero, Deceleration * Time.fixedDeltaTime);
                isWalking = false;
            } 
            
        }
          

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        movementInput = new Vector3(input.x, 0, input.y).normalized;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

            if (interacter != null)
            {
                interacter.HandleInteract();
            }

        }
    }

    public void OnNoiseMaker(InputAction.CallbackContext context)
    {
        if (context.performed)
        {


            soundManager.EmitSound(SoundType.NoiseMaker);
        }

    }

    public void OnSpawn (InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (instantiatedPlayers.Count < 2)
            {
                Vector3 spawnPosition = new Vector3(-6.75f, 0.5f, 17.75f); // Define your spawn position here
                GameObject newPlayer = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
                newPlayer.name = "Player" + (instantiatedPlayers.Count + 1);
                instantiatedPlayers.Add(newPlayer);
            }
            else
            {
                Debug.Log("Max Player Count Reached!");
            }
        }

    }

    private void HandleStepSound()
    {
        if (!isDown && isWalking) // Check again to prevent any sounds from being emitted if down
        {
            soundManager.EmitSound(SoundType.Step);
            StepSound.Play();
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
        _re.material = downedMaterial;  // Change the material to the downed state
        DeathSound.Play();
        NotifyDeathChecker(); //Checkt ob Bedingungen erfüllt sind um den Death Screen zu laden

        ChangeUI(); // Change Charactersheet to Dead Charactersheet if isDown = true
    }

    // Method to revive the player
    public void Revive()
    {

        gameObject.GetComponent<Interacter>().Deactivated = false;
        isDown = false;
        _re.material = normalMaterial;  // Revert the material to the normal state
        reviveCollider.SetActive(false);

        ChangeUI(); // Change Charactersheet to Alive Charactersheet if isDown = false

    }

    public bool getIsDown()
    {
        return isDown;
    }

    private void NotifyDeathChecker()
    {
        DeathChecker deathChecker = FindObjectOfType<DeathChecker>();
        
        deathChecker.CheckPlayersStatus();
        
    }

    private void ChangeUI()
    {
        string path = "Icons/";
        Image characterSheet = null;

        //---- Declaring which Player is changing UI ----
        if (gameObject.name == "Player1")
        {
            characterSheet = GameObject.Find("Player1_UI").transform.Find("CharacterSheet").GetComponent<Image>();
        }
        else if (gameObject.name == "Player2")
        {
            characterSheet = GameObject.Find("Player2_UI").transform.Find("CharacterSheet").GetComponent<Image>();
        }
        else
        {
            Debug.Log("Unknown Playername: " + gameObject.name);
        }
        //------------------------------------------------

        //Checking if Player is Dead or Alive
        if (isDown == true)
        {
            path = path += gameObject.name + "CharacterSheetDead";
        }
        else
        {
            path = path + gameObject.name + "CharacterSheet";
        }

        //Load Correct CharacterSheet
        characterSheet.sprite = Resources.Load<Sprite>(path);

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Test: " + collision.gameObject);
    }

}


