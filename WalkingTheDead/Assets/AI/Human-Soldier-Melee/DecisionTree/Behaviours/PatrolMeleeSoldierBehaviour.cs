using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PatrolMeleeSoldierBehaviour : Behaviour
{
    MeleeSoldierSettings settings;
    MeleeSoldier owner;
    NavMeshAgent agent;
    bool isReadyToPatrol;
    public int nextPatrolPositionIndex;
    Animator animator;

    List<Vector3> patrolPositions;
    List<GameObject> additionalPatrolpositions = null;

    public List<Vector3> PatrolPositions { get => patrolPositions;  }

    private void Awake()
    {
        owner = GetComponent<MeleeSoldier>();
        agent = owner.Agent;
        settings = owner.Settings;
        animator = owner.Animator;

        isReadyToPatrol = true;
        patrolPositions = new List<Vector3>();
        nextPatrolPositionIndex = 0;

        // Setup the base patrol position
        patrolPositions.Add(transform.position);
        patrolPositions.Add(transform.position + (transform.forward * settings.PatrolDistance));

        // Check if there are additional patrol positions
        additionalPatrolpositions = owner.AdditionalPatrolpositions;

        // If there are add it to the patrol list
        if (additionalPatrolpositions.Count > 0)
        {
            foreach (GameObject additionalPatrolPosition in additionalPatrolpositions)
            {
                // Store the position and don't update it with the gameObject
                Vector3 newPatrolPosition = additionalPatrolPosition.transform.position;

                patrolPositions.Add(newPatrolPosition);
            }
        }
    }


    public override void DoBehaviour()
    {
        if (isReadyToPatrol)
        {
            StopCoroutine(Patrol());
            StartCoroutine(Patrol());
        }

        Debug.DrawLine(transform.position, agent.destination);
    }

    IEnumerator Patrol()
    {
        // Set a patrol destination
        agent.SetDestination(GetNextPatrolPosition());
        agent.speed = settings.WalkingSpeed;

        // Reset the wander time when due
        StopCoroutine(WaitForNextPatrol());
        StartCoroutine(WaitForNextPatrol());

        yield return null;
    }

    IEnumerator WaitForNextPatrol()
    {
        isReadyToPatrol = false;

        // Dont reset until the destination is reached
        while (Vector3.Distance(transform.position, agent.destination) > 2f)
        {
            yield return null;
        }

        agent.isStopped = true;
        
        yield return new WaitForSeconds(settings.PatrolDelay);

        // Reset the time
        isReadyToPatrol = true;
        agent.isStopped = false;
    }

    Vector3 GetNextPatrolPosition()
    {
        Vector3 nextPatrolPosition = patrolPositions[nextPatrolPositionIndex];

        // Set the next Patrol Position
        nextPatrolPositionIndex = (nextPatrolPositionIndex + 1) % patrolPositions.Count;

        return nextPatrolPosition;
    }
}
