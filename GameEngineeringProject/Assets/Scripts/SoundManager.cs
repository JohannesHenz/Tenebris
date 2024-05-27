using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum SoundType { Task, Step, NoiseMaker};
public class SoundManager : MonoBehaviour
{
    [SerializeField] private GameObject stepSoundPrefab;
    [SerializeField] private GameObject noiseMakerSoundPrefab;
    [SerializeField] private GameObject taskSoundPrefab;

    [SerializeField] private GameObject SoundwaveBig;
    [SerializeField] private GameObject SoundwaveSmall;


    

    

    public void EmitSound(SoundType type)
    {
        if (type == SoundType.Task)
        {
            Instantiate(SoundwaveBig, transform.position, Quaternion.identity);
            Instantiate(taskSoundPrefab, transform.position, Quaternion.identity);
        }
        else if (type == SoundType.Step) 
        {
        
            Instantiate(SoundwaveSmall, transform.position, Quaternion.identity);
            Instantiate(stepSoundPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(SoundwaveBig, transform.position, Quaternion.identity);
            Instantiate(noiseMakerSoundPrefab, transform.position, Quaternion.identity);

        }
       
        /*
        Instantiate(soundPrefab, transform.position, Quaternion.identity);
        Instantiate(soundParticlePrefab, transform.position, Quaternion.identity);
        */

    }
}

