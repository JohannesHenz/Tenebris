using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAmbienteDetail : MonoBehaviour
{
    private AudioSource m_AudioSource;
    private float Interval = 20.0f;
    private float MaxRange = 60.0f;
    private float MinRange = 30.0f;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!m_AudioSource.isPlaying && Interval < 0.0f)
        {
            m_AudioSource.Play();
            Interval = Random.Range(MinRange, MaxRange);

        } else
        {
            Interval -= Time.deltaTime;
        }
    }
}
