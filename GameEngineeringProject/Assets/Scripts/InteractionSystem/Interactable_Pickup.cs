using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Pickup : MonoBehaviour, IInteractable
{
    [SerializeField] private string _Prompt;
    public string InteractionPrompt => _Prompt; // getter for field returns prompt
    [SerializeField] private Sprite _InteractIcon;
    public Sprite DisplayedInteractIcon => _InteractIcon;

    private GameObject oldItem;

    public bool Interact(Interacter interacter)
    {
        // 2 cases
        // - if there is an item held, exchange it with the wanted item and place old item where new item was
        // - if there is no item held, pick it up end destroy it in the world

        if (interacter.HeldItem != "")
        {
            //Save oldItemName down
            string oldItemName = interacter.HeldItem;

            interacter.HeldItem = gameObject.name;
            Debug.Log("Picked up " + gameObject.name);

            //construct path and load prefab from ressources
            string path = "Prefabs/Items/" + oldItemName;

            oldItem = Resources.Load<GameObject>(path);

            //Use transform form this pickup to instantiate old prefab
            GameObject instantiatedObject = Instantiate(oldItem, transform.position, transform.rotation);
            instantiatedObject.name = oldItemName;

            Destroy(gameObject);

            return false;
        }
        else
        {
            interacter.HeldItem = gameObject.name;
            Debug.Log("Picked up " + gameObject.name);

            Destroy(gameObject);

            return true;
        }
    }

}
