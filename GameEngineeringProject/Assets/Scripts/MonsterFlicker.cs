using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFlicker : MonoBehaviour
{
    public float flickerInterval;
    public float minFlickerInterval = 0.2f; 
    public float maxFlickerInterval = 0.4f;
    public float flickerDuration = 0.02f; // Duration for which the light is off during a flicker
    public float radius = 10f; // Radius of the sphere collider

    private SphereCollider sphereCollider;
    private Light flashlight;
    private float defaultIntensity;
    private bool shouldFlicker = false;
    private float timer = 0f;
    private bool isOn = true;

    public AudioSource HeavyBreathing;

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if (shouldFlicker)
        {
            timer += Time.deltaTime;
            if (isOn && timer >= flickerInterval)
            {
                flashlight.intensity = 0;
                isOn = false;
                timer = 0f;

            }
            else if (!isOn && timer >= flickerDuration)
            {
                flashlight.intensity = defaultIntensity;
                isOn = true;
                timer = 0f;
                flickerInterval = Random.Range(minFlickerInterval, maxFlickerInterval); // Set new random flicker interval
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform flashlightTransform = other.transform.Find("Flashlight");
            if (flashlightTransform != null)
            {
                flashlight = flashlightTransform.GetComponent<Light>();
                if (flashlight != null)
                {
                    Debug.Log("Flashlight detected in range.");
                    shouldFlicker = true;
                    HeavyBreathing.Play();
                    defaultIntensity = flashlight.intensity;
                    flickerInterval = Random.Range(minFlickerInterval, maxFlickerInterval); // Initialize with random interval
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && flashlight != null)
        {
            StopFlickering();
        }
    }

    private void Flicker()
    {
        isOn = !isOn;

        if (isOn)
        {
            flashlight.intensity = defaultIntensity;
            timer = 0f;
        }
        else
        {
            flashlight.intensity = 0;
            timer = -flickerDuration; // Subtract flickerDuration to ensure the light stays off for the correct duration
        }

    }

    private void StopFlickering()
    {
        shouldFlicker = false;
        if (flashlight != null)
        {
            flashlight.intensity = defaultIntensity; // Ensure the light returns to its default state
        }
    }
}
