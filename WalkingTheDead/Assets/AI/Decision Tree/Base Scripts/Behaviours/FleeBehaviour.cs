using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FleeBehaviour : Behaviour
{
    AISettings settings;
    NavMeshAgent agent;
    Scanner scanner;
    float fleeDistance;

    public float FleeDistance { get => fleeDistance;}

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        scanner = GetComponentInChildren<Scanner>();
        fleeDistance = 10.0f;
    }

    public override void DoBehaviour()
    {
        // Keep Calculating New Flee routes until there are enemies around
        if (scanner.ObjectsInRange.Count > 0)
        {
            StopCoroutine(FleeFromClosestEnemy());
            StartCoroutine(FleeFromClosestEnemy());
        }
    }

    public override void SetupBehaviour(AISettings settings)
    {
        this.settings = settings;
    }

    Vector3 GetFleeDirection()
    {
        // Get the closest enemy's location
        Vector3 fleeDirection = transform.forward;
        GameObject closestEnemy = scanner.GetClosestTargetInRange();

        if (closestEnemy)
        {
            // Get a vector pointing towards you
            fleeDirection = transform.position - closestEnemy.transform.position;
            fleeDirection.y = 0.0f;
            fleeDirection.Normalize();
        }

        return fleeDirection;
    }

    IEnumerator FleeFromClosestEnemy()
    {
        // Get the target position for the flee
        Vector3 fleePosition = transform.position + (GetFleeDirection() * settings.RunSpeed);

        Debug.DrawLine(transform.position, fleePosition, Color.red);

        // Avoid resetting the same destination
        if (agent.destination != fleePosition)
        {
            agent.destination = fleePosition;
            agent.speed = settings.RunSpeed;
            agent.isStopped = false;
        }

        // Only change directions every 1 seconds
        yield return null;
    }

    
}
