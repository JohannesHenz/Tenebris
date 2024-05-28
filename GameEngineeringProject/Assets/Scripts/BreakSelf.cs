using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakSelf : MonoBehaviour
{
    public MeshRenderer mesh;

    public bool isDestroyed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BreakObject()
    {
        // *** play breaking Sound ***
        //...

        mesh.enabled = false;
        isDestroyed = true;

        // Activate Oxygen Event
    }

    public void RepairObject()
    {
        // *** play repairing Sound ?  ***
        //...

        mesh.enabled = true;
        isDestroyed = false;

        // Deactivate Oxygen Event
    }
}
