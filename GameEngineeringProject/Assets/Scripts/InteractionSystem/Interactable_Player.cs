using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Player : MonoBehaviour, IInteractable
{
    private bool isReviving = false;
    private float reviveTimer = 0.0f;
    private const float requiredReviveTime = 3.0f;

    [SerializeField] private string _Prompt;

    public string InteractionPrompt => _Prompt; // getter for field returns prompt

    
    public bool Interact(Interacter interacter)
    {
        gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().Revive();
        return false;
    }

}
