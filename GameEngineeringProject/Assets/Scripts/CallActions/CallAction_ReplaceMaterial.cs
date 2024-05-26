using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAction_ReplaceMaterial : MonoBehaviour, ICallAction
{
    public List<Material> Materials = new List<Material>();
    private int MaterialIndex = 0;
    private MeshRenderer mr;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    public bool CallAction(GameObject sender)
    {
        MaterialIndex += 1;
        MaterialIndex %= Materials.Count;
        mr.material = Materials[MaterialIndex];
        return true;
    }
}
