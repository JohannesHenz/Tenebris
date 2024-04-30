using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float speed = 20.0f;
    public bool isDown = false;  // Track whether the player is incapacitated
    public string horizontalInputAxis = "Horizontal";
    public string verticalInputAxis = "Vertical";
    public string soundButton = "Button2";
    public string downButton = "Button3";
    public string interactButton = "Button1";
    private Rigidbody rb;
    public GameObject wavePrefab;
    public Material normalMaterial;
    public Material downedMaterial;
    private Renderer renderer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();  // Make sure your player GameObject has a Renderer component
    }

    void Update()
    {
        if (isDown) return;  // Skip movement updates if the player is down
        // Get input from different axes
        float horizontal = Input.GetAxis(horizontalInputAxis);
        float vertical = Input.GetAxis(verticalInputAxis);
        Vector3 movement = new Vector3(horizontal, 0.0f, vertical);

        // Move the player
        rb.MovePosition(transform.position + movement * speed * Time.deltaTime);

        if (Input.GetButtonDown(soundButton))
        {
            EmitWave();
        }
        if (Input.GetButtonDown(downButton))
        {
            GetDown();
        }
    }

    void EmitWave()
    {
        GameObject wave = Instantiate(wavePrefab, transform.position, Quaternion.identity);
    }

    public void GetDown()
    {
        isDown = true;
        rb.velocity = Vector3.zero;  // Optionally stop any current movement
        renderer.material = downedMaterial;  // Change the material to the downed state
    }

    // Method to revive the player
    public void Revive()
    {
        isDown = false;
        renderer.material = normalMaterial;  // Revert the material to the normal state
    }
}


