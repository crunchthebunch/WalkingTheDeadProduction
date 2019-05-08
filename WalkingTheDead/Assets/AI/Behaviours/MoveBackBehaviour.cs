using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveBackBehaviour : Behaviour
{
    NavMeshAgent agent;
    AISettings settings;
    Vector3 navigationCenter = Vector3.down;
    bool isTravelling = false;

    public bool IsTravelling { get => isTravelling; set => isTravelling = value; }
    public Vector3 NavigationCenter { get => navigationCenter; set => navigationCenter = value; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        navigationCenter = transform.position;
    }

    public override void DoBehaviour()
    {
        // Go Back to origin
        agent.SetDestination(navigationCenter);
        agent.speed = settings.WalkingSpeed;
        agent.isStopped = false;
    }

    public override void SetupBehaviour(AISettings settings)
    {
        this.settings = settings;
    }
}
