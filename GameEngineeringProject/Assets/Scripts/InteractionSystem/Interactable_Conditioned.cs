using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class InteractableConditioned : MonoBehaviour, IInteractable
{
    // item that the player has to hold in order to successfully interact with the interactable
    public List<GameObject> ItemConditions;
    public List<string> ItemConditionNames;

    public bool RemoveCondition = true;

    private ICallAction action;
    
    [SerializeField] private string _Prompt;
    public string InteractionPrompt => _Prompt; // getter for field returns prompt
    [SerializeField] private Sprite _InteractIcon;
    public Sprite DisplayedInteractIcon => _InteractIcon;

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
            action.CallAction(gameObject);
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
                    if (RemoveCondition)
                    {
                        ItemConditionNames.RemoveAt(i);
                    }
                    // this "consumes" player held item
                    
                    //Save oldItemName down
                    string oldItemName = interacter.HeldItem;

                    //construct path and load prefab from ressources
                    string path = "Prefabs/Items/" + oldItemName;

                    GameObject oldItem = Resources.Load<GameObject>(path);

                    //Use transform form this pickup to instantiate old prefab
                    GameObject instantiatedObject = Instantiate(oldItem, interacter.transform.position + Vector3.back, transform.rotation);
                    instantiatedObject.name = oldItemName;

                    // delete item from inventory
                    interacter.HeldItem = "";

                    //----------- Delete Item in UI -------------
                    Debug.Log("If Blank item is not  working look in Interactable_Conditioned");
                    path = "Prefabs/Icons/ItemBlank";

                    if (interacter.getName() == "Player1")
                    {
                        Image itemframeImage = GameObject.Find("Player1_UI").transform.Find("Item").GetComponent<Image>();
                        itemframeImage.sprite = Resources.Load<Sprite>(path);
                    }
                    else if (interacter.getName() == "Player2")
                    {
                        Image itemframeImage = GameObject.Find("Player2_UI").transform.Find("Item").GetComponent<Image>();
                        itemframeImage.sprite = Resources.Load<Sprite>(path);
                    }
                    else
                    {
                        Debug.Log("Unknown Playername: " + interacter.getName());
                    }
                    //-----------------------------------------


                    // set itemcondition to noitem
                    if (ItemConditionNames.Count == 0)
                    {
                        ItemConditionNames.Add("NoItem");
                        action.CallAction(gameObject);
                    }
                    return true;
                }
            } 
        }
        Debug.Log("Failed to interact with " + gameObject.name);
        return false;
    }
}
