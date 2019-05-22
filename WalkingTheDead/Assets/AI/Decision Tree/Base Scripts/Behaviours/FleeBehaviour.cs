using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FleeBehaviour : Behaviour
{
    AISettings settings;
    NavMeshAgent agent;
    Scanner scanner;
    Animator animator;
    SoundScript soundPlayer;
    float fleeDistance;
    float fleeSpeed = 0;
    float maxRandom = 0.2f;

    public float FleeDistance { get => fleeDistance;}
    public float FleeSpeed { get => fleeSpeed; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        scanner = GetComponentInChildren<Scanner>();
        fleeDistance = 15.0f;
        animator = GetComponentInChildren<Animator>();
        soundPlayer = GetComponent<SoundScript>();

    }

    public override void DoBehaviour()
    {
        // Keep Calculating New Flee routes until there are enemies around
        soundPlayer.PlayScreamClip();
        animator.speed = fleeSpeed / settings.RunSpeed;
        StopCoroutine(FleeFromClosestEnemy());
        StartCoroutine(FleeFromClosestEnemy());

    }

    public override void SetupBehaviour(AISettings settings)
    {
        this.settings = settings;
        SetupSpeed();
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
        Vector3 fleePosition = transform.position + (GetFleeDirection() * fleeSpeed);

        Debug.DrawLine(transform.position, fleePosition, Color.red);

        // Avoid resetting the same destination
        if (agent.destination != fleePosition)
        {
            agent.destination = fleePosition;
            agent.speed = fleeSpeed;
            agent.isStopped = false;
        }

        // Only change directions every 1 seconds
        yield return null;
    }

    void SetupSpeed()
    {
        fleeSpeed = settings.RunSpeed;
        fleeSpeed = Randomize(fleeSpeed);
    }

    float Randomize(float value)
    {
        return Random.Range(1 - maxRandom, 1 + maxRandom) * value;
    }
}
