using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAction_PlaySound : MonoBehaviour, ICallAction
{
    public AudioClip Sound;
    [Range(0.0f, 1.0f)] public float Volume;

    public bool Loop = false;
    public bool CallAction(GameObject sender)
    {
        if (Loop)
        {
            InvokeRepeating("PlaySound", 0.0f, Sound.length);
        }
        else
        {
            Invoke("PlaySound", 0.0f);
        }
        return true;
    }

    private void PlaySound()
    {
        AudioSource.PlayClipAtPoint(Sound, transform.position, Volume);
    }



    //Funktioniert noch nicht
    public void StopSound()
    {
        CancelInvoke("PlaySound");

    }
}
