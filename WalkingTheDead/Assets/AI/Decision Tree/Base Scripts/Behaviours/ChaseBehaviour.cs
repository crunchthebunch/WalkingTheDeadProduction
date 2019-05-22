using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseBehaviour : Behaviour
{
    AISettings settings;
    NavMeshAgent agent;
    Scanner scanner;
    Animator animator;
    float chaseDistance;

    float chaseSpeed = 0;
    float maxRandom = 0.2f;

    public float ChaseDistance { get => chaseDistance; }

    // Start is called before the first frame update
    void Awake()
    {
        chaseDistance = 10.0f;
        agent = GetComponent<NavMeshAgent>();
        scanner = GetComponentInChildren<Scanner>();
        animator = GetComponentInChildren<Animator>();
    }

    public override void DoBehaviour()
    {
        animator.speed = chaseSpeed / settings.RunSpeed;
        if (scanner.ObjectsInRange.Count > 0)
        {
            StopCoroutine(ChaseClosestTarget());
            StartCoroutine(ChaseClosestTarget());
        }
    }

    public override void SetupBehaviour(AISettings settings)
    {
        this.settings = settings;
        SetupSpeed();
    }

    IEnumerator ChaseClosestTarget()
    {
        // Find the closest target
        GameObject closestTarget = scanner.GetClosestTargetInRange();

        // If there is a target in range
        if (closestTarget)
        {
            Vector3 lastSeenPosition = closestTarget.transform.position;
            Vector3 destination = Vector3.Lerp(transform.position, lastSeenPosition, 0.9f);

            transform.LookAt(lastSeenPosition);

            // Draw line between closest target and myself
            Debug.DrawLine(transform.position, destination, Color.red);

            // If the target changed position, follow it
            if (agent.destination != destination)
            {
                agent.destination = destination;
                agent.speed = chaseSpeed;
                agent.isStopped = false;
            }
            //TODO: stop calculating new path every frame
        }
        yield return null;
    }

    void SetupSpeed()
    {
        chaseSpeed = settings.RunSpeed;
        chaseSpeed = Randomize(chaseSpeed);
    }

    float Randomize(float value)
    {
        return Random.Range(1 - maxRandom, 1 + maxRandom) * value;
    }


}
