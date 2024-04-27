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

    private void Update()
    {
        // find everything in radius that is part of mask
        _NumFound = Physics.OverlapSphereNonAlloc(_InteractionPoint.position, _InteractionPointRadius, _Colliders, _InteractableMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_InteractionPoint.position, _InteractionPointRadius);
    }
}
