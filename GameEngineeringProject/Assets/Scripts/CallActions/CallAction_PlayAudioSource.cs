using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class CallAction_PlayAudioSource : MonoBehaviour, ICallAction
{
    public GameObject Target;
    private AudioSource _AS;

    public bool Active = false;

    void Start()
    {
        _AS = Target.GetComponent<AudioSource>();
    }

    public bool CallAction(GameObject sender)
    {
        if (_AS.loop)
        {
            if (Active)
            {
                _AS.Stop();
                Active = false;
            }
            else
            {
                _AS.Play();
                Active = true;
            }
        } else
        {
            _AS.Play();
        }
        return true;
    }
}
