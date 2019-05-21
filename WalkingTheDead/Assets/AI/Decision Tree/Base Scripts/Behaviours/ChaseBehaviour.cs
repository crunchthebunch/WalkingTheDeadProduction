using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseBehaviour : Behaviour
{
    AISettings settings;
    NavMeshAgent agent;
    Scanner scanner;
    float chaseDistance;

    public float ChaseDistance { get => chaseDistance; }

    // Start is called before the first frame update
    void Awake()
    {
        chaseDistance = 10.0f;
        agent = GetComponent<NavMeshAgent>();
        scanner = GetComponentInChildren<Scanner>();
    }

    public override void DoBehaviour()
    {
        if (scanner.ObjectsInRange.Count > 0)
        {
            StopCoroutine(ChaseClosestTarget());
            StartCoroutine(ChaseClosestTarget());
        }
    }

    public override void SetupBehaviour(AISettings settings)
    {
        this.settings = settings;
    }

    IEnumerator ChaseClosestTarget()
    {
        // Find the closest target
        GameObject closestTarget = scanner.GetClosestTargetInRange();

        // If there is a target in range
        if (closestTarget)
        {
            Vector3 lastSeenPosition = closestTarget.transform.position;

            // Draw line between closest target and myself
            Debug.DrawLine(transform.position, lastSeenPosition, Color.red);

            // If the target changed position, follow it
            if (agent.destination != lastSeenPosition)
            {
                agent.destination = lastSeenPosition;
                agent.speed = settings.RunSpeed;
                agent.isStopped = false;
            }
            //TODO: stop calculating new path every frame
        }
        yield return null;
    }

}
