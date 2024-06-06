using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable_Player : MonoBehaviour, IInteractable
{
    [SerializeField] private string _Prompt;

    public string InteractionPrompt => _Prompt; // getter for field returns prompt
    [SerializeField] private Sprite _InteractIcon;
    public Sprite DisplayedInteractIcon => _InteractIcon;

    public AudioSource HealSound;

    public bool Interact(Interacter interacter)
    {
        if (interacter.HeldItem == "Bandage")
        {
            gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().Revive();
            interacter.HeldItem = "";
            HealSound.Play();

            //----------- Delete Item in UI -------------
            string path = "Icons/ItemBlank";
            Image itemframeImage;

            if (interacter.getName() == "Player1")
            {
                itemframeImage = GameObject.Find("Player1_UI").transform.Find("Item").GetComponent<Image>();

                // Lade das Sprite aus den Ressourcen
                itemframeImage.sprite = Resources.Load<Sprite>(path);
            }
            else if (interacter.getName() == "Player2")
            {
                itemframeImage = GameObject.Find("Player2_UI").transform.Find("Item").GetComponent<Image>();
                itemframeImage.sprite = Resources.Load<Sprite>(path);
            }
            else
            {
                Debug.Log("Unknown Playername: " + interacter.getName());
            }
            //-----------------------------------------
        }
        return false;
    }

}
