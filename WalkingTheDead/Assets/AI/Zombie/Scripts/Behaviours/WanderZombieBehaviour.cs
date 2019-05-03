using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderZombieBehaviour : Behaviour
{
    ZombieSettings settings;
    Zombie owner;

    NavMeshAgent agent;
    bool startedWandering;
    
    public override void DoBehaviour()
    {
        if(!startedWandering)
        {
            Vector3 wanderPosition = owner.DesiredPosition + Random.onUnitSphere * settings.WalkRadius;
            wanderPosition = owner.DesiredPosition + Random.onUnitSphere * settings.WalkRadius;
            agent.SetDestination(wanderPosition);
            agent.speed = settings.WalkingSpeed * owner.WalkSpeedModifier;
            startedWandering = true;
        }
        else
        {
            agent.speed = settings.WalkingSpeed * owner.WalkSpeedModifier;
        }

        if (Random.value < settings.WanderChance)
        {
            Wander();
        }
    }

    private void Awake()
    {
        owner = GetComponent<Zombie>();
        agent = owner.Agent;
        settings = owner.Settings;
        startedWandering = false;
    }

    void Wander()
    {
        Vector3 wanderPosition = owner.DesiredPosition + Random.insideUnitSphere * settings.WalkRadius;
        agent.SetDestination(wanderPosition);
        agent.speed = settings.WalkingSpeed * owner.WalkSpeedModifier;
    }
}
