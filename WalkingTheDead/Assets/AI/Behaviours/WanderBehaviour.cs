using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderBehaviour : Behaviour
{
    NavMeshAgent agent;
    AISettings settings;
    bool isReadyToWander;
    Vector3 navigationCenter = Vector3.down;
    float navigationRadius = 2.0f;
    float maxIdleTime = 5.0f;                //in seconds, the longest priod of time the agent will wait at it's destination before picking a new direction
    float wanderChance = 0.025f;             // % chance of agent picking a new destination before reaching their target


    public bool IsReadyToWander { get => isReadyToWander; }
    public float NavigationRadius { set => navigationRadius = value; }
    public float WanderChance { set => wanderChance = value; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        navigationCenter = transform.position;
        isReadyToWander = true;
    }


    public override void DoBehaviour()
    {
        if (isReadyToWander)
        {
            StopCoroutine(WanderAround());
            StartCoroutine(WanderAround());
        }

        Debug.DrawLine(transform.position, agent.destination);

    }

    IEnumerator WanderAround()
    {
        // Set a random destination for wandering
        agent.SetDestination(GetRandomLocationInRadius());

        // Reset the wander time when due
        StopCoroutine(WaitForNextWander());
        StartCoroutine(WaitForNextWander());

        yield return null;
    }

    IEnumerator WaitForNextWander()
    {
        isReadyToWander = false;

        // Dont reset until the destination is reached
        while (Vector3.Distance(transform.position, agent.destination) > 1f || Random.value < wanderChance)
        {
            yield return null;
        }

        // Wait for the wander delay to be reset
        agent.isStopped = true;
        yield return new WaitForSeconds(Random.Range(0, maxIdleTime));

        // Reset the time
        isReadyToWander = true;
        agent.isStopped = false;
    }

    Vector3 GetRandomLocationInRadius()
    {
        // Get a random position to travel to
        Vector3 randomDirection = Random.insideUnitSphere * Random.Range(navigationRadius / 2f, navigationRadius);
        randomDirection += navigationCenter;

        NavMeshHit hit;

        Vector3 walkableTarget = transform.position;

        // Check whether  there is anything where he can travel to
        if (NavMesh.SamplePosition(randomDirection, out hit, navigationRadius, 1))
        {
            walkableTarget = hit.position;
        }

        return walkableTarget;
    }

    private void OnDrawGizmos()
    {
        if (navigationCenter == Vector3.down)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, navigationRadius);
        }
        else
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(navigationCenter, navigationRadius);
        }

    }

    public override void SetupBehaviour(AISettings settings)
    {
        this.settings = settings;
    }
}
