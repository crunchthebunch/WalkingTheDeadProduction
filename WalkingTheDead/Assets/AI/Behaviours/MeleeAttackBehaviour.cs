using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeAttackBehaviour : Behaviour
{
    AISettings settings;
    Scanner scanner;
    bool isReadyToAttack;
    float attackDelay = 1.0f;
    GameManager gameManager;

    NavMeshAgent agent;

    public float AttackDelay { get => attackDelay; set => attackDelay = value; }

    public override void DoBehaviour()
    {
        if (scanner.ObjectsInRange.Count > 0)
        {
            StopCoroutine(AttackClosestEnemy());
            StartCoroutine(AttackClosestEnemy());
        }
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        scanner = GetComponentInChildren<Scanner>();
        isReadyToAttack = true;
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    IEnumerator AttackClosestEnemy()
    {
        GameObject toKill = scanner.GetClosestTargetInRange();

        // If it exists
        if (toKill)
        {
            Vector3 enemyPosition = toKill.transform.position;

            // If the closest Enemy is in range
            while (Vector3.Distance(enemyPosition, transform.position) > settings.MeleeAttackRange)
            {
                yield return null;
            }

            // Attack if ready
            if (isReadyToAttack)
            {
                // Kill the enemy
                isReadyToAttack = false;
                yield return new WaitForSeconds(attackDelay);

                toKill = scanner.GetClosestTargetInRange();
                if (toKill)
                {
                    KillEnemy(toKill);
                }
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

    public override void SetupBehaviour(AISettings settings)
    {
        throw new System.NotImplementedException();
    }

    void KillEnemy(GameObject toKill)
    {
        Villager villager = toKill.GetComponent<Villager>();
        if(villager)
        {
            villager.Die();
            return;
        }

        Zombie zombie = toKill.GetComponent<Zombie>();
        if (zombie)
        {
            zombie.Die();
            return;
        }

        MeleeSoldier soldier = toKill.GetComponent<MeleeSoldier>();
        if(soldier)
        {
            soldier.Die();
            return;
        }

        if (toKill.CompareTag("Necromancer"))
        {
            gameManager.DecreaseHealth();
        }
        return;
    }
}
