using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CallAction_DoorCondition : MonoBehaviour, ICallAction
{
    public GameObject Sender;
    public List<CallAction_Door> Doors;
    public GameObject Target;
    private AudioSource AccessDeniedSound;

    private void Start()
    {
        AccessDeniedSound = GetComponent<AudioSource>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, Target.transform.position);

        foreach (var door in Doors)
        {
            Gizmos.DrawLine(transform.position, door.transform.position);
        }
    }

    public bool CallAction(GameObject sender)
    {
        // checks if all doors are closed before sending callaction
        foreach (var door in Doors)
        {
            if (door.Opened)
            {
                AccessDeniedSound.Play();
                return true;
            }
        }

        Target.GetComponent<ICallAction>().CallAction(gameObject);
        return true;
    }
}
