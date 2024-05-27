using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.AI;

public class CallAction_Door : MonoBehaviour, ICallAction
{
    public bool Opened = false;
    private Vector3 EndPosition;
    private Vector3 StartPosition;
    
    public Vector3 Direction = Vector3.zero; // direction to move in
    public float Duration = 0.5f; // speed

    private NavMeshObstacle nmObstacle;

    void Start()
    {
        nmObstacle = GetComponent<NavMeshObstacle>();
        StartPosition = transform.localPosition;
        EndPosition = StartPosition - Direction;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.3f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position - Direction, 0.3f);
    }

    public bool CallAction(GameObject sender)
    {
        if (!Opened)
        {
            StartCoroutine(Open());
        } else
        {
            StartCoroutine(Close());
        }

        Opened = !Opened;
        return true;
    }

    private IEnumerator Open()
    {
        float timeElapsed = 0;
        
        while (timeElapsed < Duration) {
            float t = timeElapsed / Duration;
            transform.localPosition = Vector3.Lerp(StartPosition, EndPosition, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        if (timeElapsed > Duration)
        {
            transform.localPosition = EndPosition;
            nmObstacle.enabled = false;
        }
    }

    private IEnumerator Close()
    {
        float timeElapsed = 0;
        nmObstacle.enabled = true;
        while (timeElapsed < Duration)
        {
            float t = timeElapsed / Duration;
            transform.localPosition = Vector3.Lerp(EndPosition, StartPosition, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        if (timeElapsed > Duration)
        {
            transform.localPosition = StartPosition;
        }
    }
}
