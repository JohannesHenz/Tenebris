using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAction_PlaySound : MonoBehaviour, ICallAction
{
    public AudioClip Sound;
    [Range(0.0f, 1.0f)] public float Volume;
    public bool CallAction(GameObject sender)
    {
        AudioSource.PlayClipAtPoint(Sound, transform.position, Volume);
        return true;
    }
}
