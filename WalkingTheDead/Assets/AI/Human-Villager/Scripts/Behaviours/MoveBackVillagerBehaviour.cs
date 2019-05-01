using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveBackVillagerBehaviour : Behaviour
{
    Villager owner;
    NavMeshAgent agent;
    VillagerSettings settings;
    Vector3 navigationCenter = Vector3.down;
    bool isTravelling = false;

    public bool IsTravelling { get => isTravelling; set => isTravelling = value; }
    public Vector3 NavigationCenter { get => navigationCenter; }

    private void Awake()
    {
        owner = GetComponent<Villager>();
        agent = owner.Agent;
        navigationCenter = transform.position;
        settings = owner.Settings;
    }

    public override void DoBehaviour()
    {
        // Go Back to origin
        agent.SetDestination(navigationCenter);
        agent.speed = settings.WalkingSpeed;
        agent.isStopped = false;
    }

}
