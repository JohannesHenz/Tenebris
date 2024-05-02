using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private GameObject stepSoundPrefab;
    [SerializeField] private GameObject noiseMakerSoundPrefab;
    [SerializeField] private GameObject taskSoundPrefab;

    public void EmitStepSound(float radius)
    {
        GameObject sound = EmitSound(stepSoundPrefab, radius);
        sound.GetComponent<SoundWaveExpansion>().maxRadius = radius;  // Set the max radius
    }

    public void EmitNoiseMakerSound(float radius)
    {
        GameObject sound = EmitSound(noiseMakerSoundPrefab, radius);
        sound.GetComponent<SoundWaveExpansion>().maxRadius = radius;  // Set the max radius
    }

    public void EmitTaskSound(float radius)
    {
        GameObject sound = EmitSound(taskSoundPrefab, radius);
        sound.GetComponent<SoundWaveExpansion>().maxRadius = radius;  // Set the max radius
    }

    private GameObject EmitSound(GameObject soundPrefab, float radius)
    {
        //Instantiate the right SoundPrefab based on which Emit_Sound method is called
        GameObject sound = Instantiate(soundPrefab, transform.position, Quaternion.identity);
        return sound;
    }
}

