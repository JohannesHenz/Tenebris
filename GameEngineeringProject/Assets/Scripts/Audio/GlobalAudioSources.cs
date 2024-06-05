using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAudioSources : MonoBehaviour
{
    public static GlobalAudioSources Instance { get; private set; }
    public AudioSource SoundAmbienteLooped;
    public AudioSource SoundAmbienteDetail;
    public AudioSource SoundAlarm;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
