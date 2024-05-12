using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Player : MonoBehaviour, IInteractable
{
    [SerializeField] private string _Prompt;

    public string InteractionPrompt => _Prompt; // getter for field returns prompt
    [SerializeField] private Sprite _InteractIcon;
    public Sprite DisplayedInteractIcon => _InteractIcon;


    public bool Interact(Interacter interacter)
    {
        gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().Revive();
        return false;
    }

}
