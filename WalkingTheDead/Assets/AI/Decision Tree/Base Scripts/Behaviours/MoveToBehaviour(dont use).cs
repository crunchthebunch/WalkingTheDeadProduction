﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToBehaviour : Behaviour
{
    AISettings settings;
    NavMeshAgent agent;
    Vector3 destination;

    public Vector3 Destination { get => destination; }

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
        destination = this.transform.position;
    }

    void MoveTo()
    {
        agent.SetDestination(destination);
        agent.speed = settings.WalkingSpeed;
        agent.isStopped = false;
    }
}
