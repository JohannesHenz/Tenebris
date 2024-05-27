using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProximityFlicker : MonoBehaviour
{
    public GameObject monster; // Assign the monster GameObject in the inspector
    private LightsFlicker lightsFlicker;
    private Light myLight;
    public float maxDistance = 5.0f; // Maximum distance for full flicker effect

    private void Start()
    {
        lightsFlicker = GetComponent<LightsFlicker>();
        myLight = GetComponent<Light>();
    }

    private void Update()
    {
        if (monster != null)
        {
            float distance = Vector3.Distance(monster.transform.position, transform.position);
            float frequency = Mathf.Lerp(5.0f, 1.0f, distance / maxDistance); // Closer = more frequent flicker
            lightsFlicker.SetFlickerFrequency(frequency);
        }
    }
}
