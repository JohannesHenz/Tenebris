using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class UI_Interactable : MonoBehaviour
{
    public GameObject Target;
    private Vector3 Offset = new Vector3(0, 3, 2);
    public GameObject UiElement;
    public bool IsDisplayed = false;

    // Start is called before the first frame update
    void Start()
    {
        UiElement.SetActive(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        gameObject.transform.rotation = Quaternion.LookRotation(Vector3.down);
        gameObject.transform.position = Target.transform.position + Offset;
    }

    public void SetUp()
    {
        // TODO: prompt text can be added later https://www.youtube.com/watch?v=THmW4YolDok&ab_channel=DanPos
        UiElement.SetActive(true);
        IsDisplayed = true;
    }

    public void Close()
    {
        IsDisplayed = false;
        UiElement.SetActive(false);
    }
}
