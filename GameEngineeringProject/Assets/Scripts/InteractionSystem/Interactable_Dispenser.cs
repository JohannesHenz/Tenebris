using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable_Dispenser : MonoBehaviour, IInteractable
{
    [SerializeField] private string _Prompt;
    public string InteractionPrompt => _Prompt; // getter for field returns prompt
    [SerializeField] private Sprite _InteractIcon;
    public Sprite DisplayedInteractIcon => _InteractIcon;

    private GameObject oldItem;

    public string DispensedItem;

    private bool Cooldown;

    

    public bool Interact(Interacter interacter)
    {
        // 2 cases
        // - if there is an item held, exchange it with the wanted item and place old item where new item was
        // - if there is no item held, pick it up end destroy it in the world
        if (!Cooldown)
        {
            Cooldown = true;

            if (interacter.HeldItem != "")
            {
                //Save oldItemName down
                string oldItemName = interacter.HeldItem;

                interacter.HeldItem = DispensedItem;
                Debug.Log("Picked up " + DispensedItem);

                //construct path and load prefab from ressources
                string path = "Prefabs/Items/" + oldItemName;

                oldItem = Resources.Load<GameObject>(path);

                //Use transform form this pickup to instantiate old prefab
                GameObject instantiatedObject = Instantiate(oldItem, interacter.transform.position + Vector3.back, transform.rotation);
                instantiatedObject.name = oldItemName;



            }
            else
            {
                interacter.HeldItem = DispensedItem;
                Debug.Log("Picked up " + DispensedItem);


            }


            //----------- Change Item in UI -------------
            if (interacter.getName() == "Player1")
            {
                Image itemframeImage = GameObject.Find("Player1_UI").transform.Find("Item").GetComponent<Image>();
                itemframeImage.sprite = _InteractIcon;
            }
            else if (interacter.getName() == "Player2")
            {
                Image itemframeImage = GameObject.Find("Player2_UI").transform.Find("Item").GetComponent<Image>();
                itemframeImage.sprite = _InteractIcon;
            }
            else
            {
                Debug.Log("Unknown Playername: " + interacter.getName());
            }
            //-----------------------------------------

            Invoke("CooldownTimer", 30f); //Hier Cooldown Zeit für Revives anpassen


        }
            return true;
      }

    private void CooldownTimer()
    {
        Cooldown = false;
    }

}
