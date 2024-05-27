using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsFlicker : MonoBehaviour
{
    private Light myLight;
    public float maxInterval = 1.0f; // Default value for max interval
    public float maxFlicker = 0.2f;  // Default value for max flicker duration

    float defaultIntensity;
    bool isOn;
    float timer;
    float delay;
    private bool shouldFlicker = false;

    private void Start()
    {
        myLight = GetComponent<Light>();
        defaultIntensity = myLight.intensity;
    }


    public void Flicker()
    {
        isOn = !isOn;

        if (isOn)
        {
            myLight.intensity = defaultIntensity;
            delay = UnityEngine.Random.Range(0, maxInterval);
        }
        else
        {
            myLight.intensity = 0;
            delay = UnityEngine.Random.Range(0, maxFlicker);
        }

        timer = 0;
    }

    public void SetFlickerFrequency(float frequency)
    {
        maxInterval = frequency;
        maxFlicker = frequency * 0.1f; // Adjust this multiplier to fine-tune flicker behavior
    }

    public void StartFlickering()
    {
        shouldFlicker = true;
    }

    public void StopFlickering()
    {
        shouldFlicker = false;
        myLight.intensity = defaultIntensity; // Ensure the light returns to its default state
    }
}
