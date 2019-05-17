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
    float maxIdleTime = 5.0f;                    //in seconds, the longest priod of time the agent will wait at it's destination before picking a new direction
    float wanderTime = 0;                    
    [SerializeField] float minWanderTime = 0.5f;
    [SerializeField] float maxWanderTime = 10;

    public bool IsReadyToWander { get => isReadyToWander; }
    public Vector3 NavigationCenter { get => navigationCenter; set => navigationCenter = value; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        SetWanderTime();
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
        while (Vector3.Distance(agent.transform.position, agent.destination) > 1.0f)
        {
            if (wanderTime < 0.0f)
            {
                SetWanderTime();
                agent.SetDestination(GetRandomLocationInRadius());
            }
            else
            {
                wanderTime -= Time.deltaTime;
            }

            yield return null;
        }

        // Wait for the wander delay to be reset
        agent.isStopped = true;
        yield return new WaitForSeconds(Random.Range(0.1f, maxIdleTime));

        // Reset the time
        isReadyToWander = true;
        agent.isStopped = false;
    }

    Vector3 GetRandomLocationInRadius()
    {
        // Get a random position to travel to
        Vector3 randomDirection = Random.insideUnitSphere * Random.Range(settings.NavigationRadius / 2f, settings.NavigationRadius);
        randomDirection += navigationCenter;

        NavMeshHit hit;

        Vector3 walkableTarget = transform.position;

        // Check whether  there is anything where he can travel to
        if (NavMesh.SamplePosition(randomDirection, out hit, settings.NavigationRadius, 1))
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
            Gizmos.DrawWireSphere(transform.position, settings.NavigationRadius);
        }
        else
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(navigationCenter, settings.NavigationRadius);
        }

    }

    public override void SetupBehaviour(AISettings settings)
    {
        this.settings = settings;
    }

    public void SetWanderTime()
    {
        wanderTime = Random.Range(minWanderTime, maxWanderTime);
    }
}
