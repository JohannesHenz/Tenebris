using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemConditionedInteractable : MonoBehaviour, IInteractable
{
    // item that the player has to hold in order to successfully interact with the interactable
    public GameObject ItemCondition;
    public string ItemConditionName;

    private ICallAction action;

    [SerializeField] private string _Prompt;
    public string InteractionPrompt => _Prompt; // getter for field returns prompt

    void Start()
    {
        ItemConditionName = ItemCondition.name;
        action = gameObject.GetComponent<ICallAction>();
    }

    public bool Interact(Interacter interacter)
    {
        // 3 cases
        // - no condition (item in condition is "NoItem") - always successful
        // - condition met - successful
        // - condition not met - unsuccessful

        if (ItemConditionName == "NoItem")
        {
            Debug.Log("Successfully interacted with: " + gameObject.name + ", which does not require an item");
            action.CallAction();
            return true;
        } 
        else if (interacter.HeldItem == "")
        {
            Debug.Log("Failed to interact with: " + gameObject.name + ", you dont have the right item");
            return false;
        }
        else if (ItemConditionName == interacter.HeldItem)
        {
            Debug.Log("Successfully interacted with: " + gameObject.name + ", item used: " + interacter.HeldItem);

            // this "consumes" player held item
            // delete item from inventory
            interacter.HeldItem = "";
            // set itemcondition to noitem
            ItemConditionName = "NoItem";

            action.CallAction();
            return true;
        }
        Debug.Log("Failed to interact with " + gameObject.name);
        return false;
    }
}
