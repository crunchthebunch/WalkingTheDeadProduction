using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseMeleeSoldierBehaviour : Behaviour
{
    MeleeSoldierSettings settings;
    MeleeSoldier owner;
    NavMeshAgent agent;
    Scanner enemyScanner;
    Animator animator;

    public override void DoBehaviour()
    {
        // Chase zombies as long as we are in chase mode
        if (owner.ZombieScanner.ObjectsInRange.Count > 0)
        {
            StopCoroutine(ChaseClosestZombie()); // See if this needs to be a Coroutine
            StartCoroutine(ChaseClosestZombie());
        }
    }

    private void Awake()
    {
        owner = GetComponent<MeleeSoldier>();
        agent = owner.Agent;
        settings = owner.Settings;
        enemyScanner = owner.ZombieScanner;
        animator = owner.Animator;
    }

    IEnumerator ChaseClosestZombie()
    {
        // Find the closest Zombie
        GameObject closestEnemy = enemyScanner.GetClosestTargetInRange();


        // If there is a Zombie in range
        if (closestEnemy)
        {
            Vector3 lastSeenPosition = closestEnemy.transform.position;

            // Draw line between closest Zombie and the soldier
            Debug.DrawLine(transform.position, lastSeenPosition, Color.red);

            // If the Zombie changed position, follow it
            if (agent.destination != lastSeenPosition)
            {
                agent.destination = lastSeenPosition;
                agent.speed = settings.ChaseSpeed;
                agent.isStopped = false;
            }
        }
        yield return null;
    }

}
