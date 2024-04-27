using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interacter : MonoBehaviour
{
    [SerializeField] private Transform _InteractionPoint;
    [SerializeField] private float _InteractionPointRadius = 0.5f;
    [SerializeField] private LayerMask _InteractableMask;

    private readonly Collider[] _Colliders = new Collider[3];
    [SerializeField] private int _NumFound;

    [SerializeField] private UI_Interactable _uiInteractable;

    public GameObject HeldItem = null;

    private IInteractable _Interactable;

    private void Update()
    {
        // find everything in radius that is part of mask
        _NumFound = Physics.OverlapSphereNonAlloc(_InteractionPoint.position, _InteractionPointRadius, _Colliders, _InteractableMask);

        if (_NumFound > 0 )
        {
            _Interactable = _Colliders[0].GetComponent<IInteractable>();

            if (_Interactable != null)
            {
                if (!_uiInteractable.IsDisplayed) _uiInteractable.SetUp(); // does all the setup for ui display no action yet

                if (Input.GetButtonDown("Jump")) _Interactable.Interact(this); // action when button is pressed
            }
        } else
        {
            if (_Interactable != null) _Interactable = null;
            if (_uiInteractable.IsDisplayed) _uiInteractable.Close();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_InteractionPoint.position, _InteractionPointRadius);
    }
}
