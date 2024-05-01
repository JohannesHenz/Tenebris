using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public float expansionRate = 3f;
    public float maxRadius = 15f;

    void Start()
    {
        StartCoroutine(ExpandAndFade());
    }

    IEnumerator ExpandAndFade()
    {
        float currentRadius = 0f;
        while (currentRadius < maxRadius)
        {
            currentRadius += expansionRate * Time.deltaTime;
            transform.localScale = new Vector3(currentRadius, transform.localScale.y, currentRadius);
            yield return null;
        }
        Destroy(gameObject);
    }
    //transform.localScale.y
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            StopCoroutine(ExpandAndFade());
            // Optionally make the wave disappear or change appearance
            Destroy(gameObject);
        }
    }
}
