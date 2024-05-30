using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAction_ParticleControl : MonoBehaviour, ICallAction
{
    public bool Playing = false;
    private ParticleSystem _particleSystem;

    public void Start()
    {
        _particleSystem = gameObject.GetComponent<ParticleSystem>();
        _particleSystem.Stop();
    }

    public bool CallAction(GameObject sender)
    {
        if (Playing)
        {
            _particleSystem.Stop();
            Playing = false;
        } else
        {
            _particleSystem.Play();
            Playing = true;
        }
        return true;
    }
}
