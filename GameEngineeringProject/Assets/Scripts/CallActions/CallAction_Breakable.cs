using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAction_Breakable : MonoBehaviour, ICallAction
{
    public GameObject InteractableObject;
    public GameObject TriggerObject;
    public GameObject EffectsObject;
    [SerializeField] private UI_Interactable _uiInteractable;
    public Sprite SpriteInput; // its a hack

    //Every Breakable could have an individual impact on the oxygen timer
    public float oxygenLeakWeight = 1;
    private Timer OxygenTimer;

    public bool isDestroyed = false;

    private void Start()
    {
        InteractableObject.SetActive(false);
        _uiInteractable.Close();
        OxygenTimer = GameObject.Find("GameManager").GetComponent<Timer>();
    }

    public bool CallAction(GameObject sender)
    {
        if (!isDestroyed)
        {
            BreakObject();
        } else
        {
            RepairObject();
        }
        EffectsObject.GetComponent<ICallAction>().CallAction(gameObject);
        return true;
    }

    public void BreakObject()
    {
        // *** play breaking Sound ***
        
        OxygenTimer.increaseTimerSpeed(oxygenLeakWeight);

        isDestroyed = true;
        InteractableObject.SetActive(true);
        TriggerObject.SetActive(false);
        _uiInteractable.SetUp(SpriteInput);

        // Activate Oxygen Event
    }

    public void RepairObject()
    {
        // *** play repairing Sound ?  ***

        OxygenTimer.decreaseTimerSpeed(oxygenLeakWeight);

        isDestroyed = false;
        InteractableObject.SetActive(false);
        TriggerObject.SetActive(true);
        _uiInteractable.Close();
        // Deactivate Oxygen Event
    }
}
