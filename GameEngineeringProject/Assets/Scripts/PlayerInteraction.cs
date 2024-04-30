using UnityEngine;
using System.Collections;

public class PlayerInteraction : MonoBehaviour
{
    public Transform otherPlayer;  // Reference to the other player
    private bool isReviving = false;
    private float reviveTimer = 0.0f;
    private const float requiredReviveTime = 3.0f;  // Time required to hold the button
    public string interactButton = "Button1";


    void Update()
    {
        if (Input.GetKeyDown(interactButton) && IsNearby(otherPlayer) && otherPlayer.GetComponent<PlayerMovement>().isDown)
        {
            isReviving = true;
            reviveTimer = 0.0f;
        }

        if (isReviving && Input.GetKey(interactButton))
        {
            reviveTimer += Time.deltaTime;
            if (reviveTimer >= requiredReviveTime)
            {
                otherPlayer.GetComponent<PlayerMovement>().Revive();
                isReviving = false;
            }
        }

        if (Input.GetKeyUp(interactButton))
        {
            isReviving = false;
        }
    }

    // Check if the other player is within revive range
    private bool IsNearby(Transform other)
    {
        return Vector3.Distance(transform.position, other.position) <= 2.0f;  // Assuming 2 meters is the interaction range
    }
}
