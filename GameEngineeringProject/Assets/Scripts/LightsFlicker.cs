using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsFlicker : MonoBehaviour
{
    private Light myLight;
    private float maxInterval = 30;
    public float maxFlicker = 0.2f;

    float defaultIntensity;
    bool isOn;
    float timer;
    float delay;

    private void Start()
    {
        myLight = GetComponent<Light>();
        defaultIntensity = myLight.intensity;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > delay)
        {
            Flicker();
        }
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
}
