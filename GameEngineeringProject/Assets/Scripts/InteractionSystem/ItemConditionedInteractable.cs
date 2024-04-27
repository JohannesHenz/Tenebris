using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemConditionedInteractable : MonoBehaviour, IInteractable
{
    // item that the player has to hold in order to successfully interact with the interactable
    public GameObject ItemCondition;

    [SerializeField] private string _Prompt;
    public string InteractionPrompt => _Prompt; // getter for field returns prompt

    public bool Interact(Interacter interacter)
    {
        // 3 cases
        // - no condition (item in condition is "NoCondition") - always successful
        // - condition met - successful
        // - condition not met - unsuccessful

        if (ItemCondition.name == "NoCondition")
        {
            Debug.Log("Successfully interacted with " + gameObject.name);
            return true;
        } else if (ItemCondition.name == interacter.HeldItem.name)
        {
            Debug.Log("Successfully interacted with " + gameObject.name);
            return true;
        }
        Debug.Log("Failed to interact with " + gameObject.name);
        return false;
    }
}
