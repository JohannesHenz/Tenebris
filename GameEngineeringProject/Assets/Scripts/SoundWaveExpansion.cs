using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWaveExpansion : MonoBehaviour
{
    public float expansionRate = 15f;  // Speed at which the sphere expands
    public float maxRadius;  // Maximum radius to reach before self-destruction

    private Vector3 initialScale; // Initial scale of the sphere, assuming it starts at 1,1,1

    void Start()
    {
        initialScale = transform.localScale; // Store the initial scale (should be very small to start)
    }

    void Update()
    {
        if (transform.localScale.x < maxRadius)
        {
            float scale = expansionRate * Time.deltaTime;
            transform.localScale += new Vector3(scale, scale, scale);  // Uniformly scale the object

            // Correctly clamp the scale to prevent overshooting the maximum radius
            if (transform.localScale.x > maxRadius)
            {
                transform.localScale = new Vector3(maxRadius, maxRadius, maxRadius);  // Clamp to max size
            }
        }
        else
        {
            Destroy(gameObject);  // Destroy the object once it reaches the maximum radius
        }
    }
}
