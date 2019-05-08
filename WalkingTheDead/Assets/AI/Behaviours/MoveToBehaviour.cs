using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToBehaviour : Behaviour
{
    AISettings settings;
    NavMeshAgent agent;
    Vector3 destination;

    public override void DoBehaviour()
    {
        MoveTo();
    }

    public override void SetupBehaviour(AISettings settings)
    {
        this.settings = settings;
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void MoveTo()
    {
        agent.SetDestination(destination);
        agent.speed = settings.WalkingSpeed;
        agent.isStopped = false;
    }

    //-----for recieving commands-----//
    void RecieveCommand(Vector3 position, bool followPlayer)
    {
        destination = position + Random.onUnitSphere * 2.0f;
        agent.SetDestination(destination);
    }
    private void OnEnable()
    {
        PlayerCommand.Click += RecieveCommand;
    }

    private void OnDisable()
    {
        PlayerCommand.Click -= RecieveCommand;
    }
    //--------------------------------//
}
