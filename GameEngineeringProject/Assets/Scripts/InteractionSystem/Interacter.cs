using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Interacter : MonoBehaviour
{
    [SerializeField] private Transform _InteractionPoint;
    [SerializeField] private float _InteractionPointRadius = 1f;
    [SerializeField] private LayerMask _InteractableMask;

    public string interactButton = "";


    private readonly Collider[] _Colliders = new Collider[3];
    [SerializeField] private int _NumFound;

    [SerializeField] private UI_Interactable _uiInteractable;

    public String HeldItem = "";

    private IInteractable _Interactable;

    public bool Deactivated = false;

    private bool InputBlocked = false;
    private float InputDelay = 1.0f;

    public AudioSource InteractSound = null;

    /// Added by Johannes for the TaskSound
    private SoundManager soundManager;  // Reference to the SoundManager

    /// 

    void Start()
    {
        InputBlocked = false;
        soundManager = GetComponentInParent<SoundManager>();  // Ensure the sound manager is attached to the parent
    }

    private void Update()
    {
        if (!Deactivated) { 
        // find everything in radius that is part of mask
        _NumFound = Physics.OverlapSphereNonAlloc(_InteractionPoint.position, _InteractionPointRadius, _Colliders, _InteractableMask);

        if (_NumFound > 0 )
        {
            _Interactable = _Colliders[0].GetComponent<IInteractable>();

            if (_Interactable != null)
            {
                if (!_uiInteractable.IsDisplayed) _uiInteractable.SetUp(_Interactable.DisplayedInteractIcon); // does all the setup for ui display no action yet

                
            }
        } else
        {
            if (_Interactable != null) _Interactable = null;
            if (_uiInteractable.IsDisplayed) _uiInteractable.Close();
        }

        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_InteractionPoint.position, _InteractionPointRadius);
    }

    private void UnblockInput()
    {
        InputBlocked = false;
    }

    public void HandleInteract()
    {
        if (!Deactivated && _Interactable != null && !InputBlocked)
        {
            InteractSound.Play();
            InputBlocked = true;
            if (_Interactable.Interact(this))
            {
                /// Added by Johannes for the TaskSound
                soundManager.EmitSound(SoundType.Task);
            }
            
            /// 
            // start cooldown coroutine
            Invoke("UnblockInput", InputDelay);
        }
    }

    public String getName()
    {
        return gameObject.name;
    }

    }
