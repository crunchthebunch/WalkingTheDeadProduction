using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveBackBehaviour : Behaviour
{
    NavMeshAgent agent;
    AISettings settings;
    Animator animator;
    Vector3 navigationCenter = Vector3.down;
    bool isTravelling = false;

    float returnSpeed = 0;
    float maxRandom = 0.2f;

    public bool IsTravelling { get => isTravelling; set => isTravelling = value; }
    public Vector3 NavigationCenter { get => navigationCenter; set => navigationCenter = value; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        navigationCenter = transform.position;
    }

    public override void DoBehaviour()
    {
        // Go Back to origin
        animator.speed = returnSpeed / settings.WalkingSpeed;
        agent.SetDestination(navigationCenter);
        agent.speed = returnSpeed;
        agent.isStopped = false;
    }

    public override void SetupBehaviour(AISettings settings)
    {
        this.settings = settings;
        SetupSpeed();
    }

    void SetupSpeed()
    {
        returnSpeed = settings.WalkingSpeed;
        returnSpeed = Randomize(returnSpeed);
    }

    float Randomize(float value)
    {
        return Random.Range(1 - maxRandom, 1 + maxRandom) * value;
    }
}
