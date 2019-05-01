using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackZombieBehaviour : Behaviour
{
    ZombieSettings settings;
    Zombie owner;
    Scanner ownerScanner;
    bool isReadyToAttack;
    ZombieAnimationScript animationEvent;
    Animator animator;
    float attackDelay = 1.0f;

    NavMeshAgent agent;

    public override void DoBehaviour()
    {
        if (ownerScanner.ObjectsInRange.Count > 0)
        {
            StopCoroutine(AttackClosestEnemy());
            StartCoroutine(AttackClosestEnemy());
        }
    }

    private void Awake()
    {
        owner = GetComponent<Zombie>();
        agent = owner.Agent;
        settings = owner.Settings;
        ownerScanner = owner.HumanScanner;
        animator = owner.Anim;
        isReadyToAttack = true;
        animationEvent = GetComponentInChildren<ZombieAnimationScript>();
    }

    IEnumerator AttackClosestEnemy()
    {
        GameObject ToKill = ownerScanner.GetClosestTargetInRange();

        // If it exists
        if (ToKill)
        {
            Vector3 enemyPosition = ToKill.transform.position;

            // If the closest Enemy is in range
            while (Vector3.Distance(enemyPosition, transform.position) > settings.AttackRange)
            {
                yield return null;
            }

            // Attack if ready to attack
            if (isReadyToAttack)
            {
                // Kill the zombie
                isReadyToAttack = false;
                print("Attacking");
                animationEvent.SetClosestEnemy(ToKill);
                animator.SetTrigger("Attack");
            }

            yield return null;
        }

        

        yield return null;
    }

    // To call from event script
    public void AttackCoolDown()
    {
        isReadyToAttack = true;
    }

}
