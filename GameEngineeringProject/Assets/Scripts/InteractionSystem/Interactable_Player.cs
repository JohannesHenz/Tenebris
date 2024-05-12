using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Player : MonoBehaviour, IInteractable
{
    [SerializeField] private string _Prompt;

    public string InteractionPrompt => _Prompt; // getter for field returns prompt
    public Sprite DisplayedInteractIcon { get; set; }


    public bool Interact(Interacter interacter)
    {
        gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().Revive();
        return false;
    }

}
