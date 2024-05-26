using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights_DangerWubWub : MonoBehaviour
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
        SelfLight.intensity = Mathf.Sin(Time.frameCount * 0.3f) * 3.0f + 6;
    }
}
