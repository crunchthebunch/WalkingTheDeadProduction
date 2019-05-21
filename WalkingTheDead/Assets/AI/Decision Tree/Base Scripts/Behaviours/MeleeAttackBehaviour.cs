using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeAttackBehaviour : Behaviour
{
    AISettings settings;
    Scanner scanner;
    bool readyToAttack;
    float attackTimer;
    GameManager gameManager;
    Animator animator;

    NavMeshAgent agent;

    GameObject toKill;

    public override void DoBehaviour()
    {
        if (readyToAttack)
        {
            toKill = scanner.GetClosestTargetInRange();
            // If it exists
            if (toKill)
            {
                Vector3 enemyPosition = toKill.transform.position;

                // If the closest Enemy is in range
                if (Vector3.Distance(enemyPosition, transform.position) < settings.MeleeAttackRange)
                {
                    animator.SetTrigger("Attack");
                    readyToAttack = false;
                    attackTimer = settings.AttackDelay;
                }
            }
        }
        else
        {
            if (attackTimer <= 0)
            {
                readyToAttack = true;
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
        }
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        scanner = GetComponentInChildren<Scanner>();
        readyToAttack = true;
        gameManager = GameObject.FindObjectOfType<GameManager>();
        animator = GetComponentInChildren<Animator>();
    }

    public override void SetupBehaviour(AISettings settings)
    {
        this.settings = settings;
    }
}
