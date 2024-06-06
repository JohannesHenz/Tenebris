using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class CheckForMonsterTrigger : MonoBehaviour, ICallAction
{
    public GameObject Monster;
    public GameObject DummyMonster;
    private Rigidbody DummyMonster_rb;
    public GameObject Target;
    private Vector3 TargetPosition;
    public bool MonsterPresent = false;
    public bool SpacingInProgress = false;

    private void Start()
    {
        DummyMonster_rb = DummyMonster.GetComponent<Rigidbody>();
        TargetPosition = Target.transform.position;
    }

    public bool CallAction(GameObject sender)
    {
        if (MonsterPresent)
        {
            SpacingInProgress = true;
            DummyMonster.SetActive(true);
            DummyMonster.transform.position = Monster.transform.position;
            Monster.SetActive(false);
        }
        return true;
    }


    private void FixedUpdate()
    {
        if (SpacingInProgress)
        {
            DummyMonster_rb.AddForce((TargetPosition - DummyMonster.transform.position), ForceMode.Force);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            MonsterPresent = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            MonsterPresent = false;
        }
    }
}
