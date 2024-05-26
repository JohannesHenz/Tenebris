using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights_Wubwub : MonoBehaviour
{
    private Light SelfLight;
    // does the wubwub
    void Start()
    {
        SelfLight = gameObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        SelfLight.range = Mathf.Sin(Time.frameCount * 0.05f) * 0.5f + 6;
    }
}
