using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemConditionedInteractable : MonoBehaviour, IInteractable
{
    // item that the player has to hold in order to successfully interact with the interactable
    public GameObject ItemCondition;
    public string ItemConditionName;

    [SerializeField] private string _Prompt;
    public string InteractionPrompt => _Prompt; // getter for field returns prompt

    void Start()
    {
        ItemConditionName = ItemCondition.name;
    }

    public bool Interact(Interacter interacter)
    {
        // 3 cases
        // - no condition (item in condition is "NoItem") - always successful
        // - condition met - successful
        // - condition not met - unsuccessful

        if (ItemConditionName == "NoItem")
        {
            Debug.Log("Successfully interacted with: " + gameObject.name + ", which does not require a key");
            return true;
        } 
        else if (interacter.HeldItem == "")
        {
            Debug.Log("Failed to interact with: " + gameObject.name + ", you dont have the right item");
            return false;
        }
        else if (ItemConditionName == interacter.HeldItem)
        {
            // delete item from inventory if used
            interacter.HeldItem = "";

            Debug.Log("Successfully interacted with: " + gameObject.name + ", item used: " + interacter.HeldItem);
            return true;
        }
        Debug.Log("Failed to interact with " + gameObject.name);
        return false;
    }
}
