using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Pickup : MonoBehaviour, IInteractable
{
    [SerializeField] private string _Prompt;
    public string InteractionPrompt => _Prompt; // getter for field returns prompt
    
    public bool Interact(Interacter interacter)
    {
        // 2 cases
        // - if there is an item held do nothing
        // - if there is no item held, pick it up end destroy it in the world

        if (interacter.HeldItem != "") return false;
        else
        {
            interacter.HeldItem = gameObject.name;
            Debug.Log("Picked up " + gameObject.name);

            Destroy(gameObject);

            return true;
        }
    }
}
