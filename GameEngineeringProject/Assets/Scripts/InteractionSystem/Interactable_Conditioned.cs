using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemConditionedInteractable : MonoBehaviour, IInteractable
{
    // item that the player has to hold in order to successfully interact with the interactable
    public List<GameObject> ItemConditions;
    public List<string> ItemConditionNames;

    private ICallAction action;

    [SerializeField] private string _Prompt;
    public string InteractionPrompt => _Prompt; // getter for field returns prompt

    void Start()
    {
        foreach (var ItemCondition in ItemConditions)
        {
            ItemConditionNames.Add(ItemCondition.name);
        }
            

        action = gameObject.GetComponent<ICallAction>();
    }

    public bool Interact(Interacter interacter)
    {
        // 3 cases
        // - no condition (item in condition is "NoItem") - always successful
        // - condition met - successful
        // - condition not met - unsuccessful

        if (ItemConditionNames[0] == "NoItem")
        {
            Debug.Log("Successfully interacted with: " + gameObject.name + ", which does not require an item");
            action.CallAction();
            return true;
        }
        else if (ItemConditionNames[0] == "RemoteActivated")
        {
            Debug.Log("Failed to interact with: " + gameObject.name + ", this item needs to be activated remotely");
            return false;
        }
        else if (interacter.HeldItem == "")
        {
            Debug.Log("Failed to interact with: " + gameObject.name + ", you dont have the right item");
            return false;
        }
        else
        {


            for (int i = 0; i < ItemConditionNames.Count; i++)
            {
                if (ItemConditionNames[i] == interacter.HeldItem)
                {
                    Debug.Log("Successfully interacted with: " + gameObject.name + ", item used: " + interacter.HeldItem);
                    ItemConditionNames.RemoveAt(i);
                    // this "consumes" player held item
                    // delete item from inventory
                    interacter.HeldItem = "";
                    // set itemcondition to noitem
                    if (ItemConditionNames.Count == 0)
                    {
                        ItemConditionNames.Add("NoItem");
                        action.CallAction();
                    }
                    return true;
                }
            } 
        }
        Debug.Log("Failed to interact with " + gameObject.name);
        return false;
    }
}
