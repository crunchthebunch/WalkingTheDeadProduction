using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeAttackBehaviour : Behaviour
{
    AISettings settings;
    Scanner scanner;

    GameManager gameManager;
    Animator animator;

    NavMeshAgent agent;
    GameObject toKill;

    SoundScript soundPlayer;

    bool attackComplete = true;
    float delayCurrent = 0f;
    float delayMax = 0f;
    float maxRandom = 0.2f;

    float attackRange = 0;

    public bool AttackComplete { get => attackComplete; set => attackComplete = value; }

    public override void DoBehaviour()
    {
        if (delayCurrent <= 0)
        {
            toKill = scanner.GetClosestTargetInRange();
            // If it exists
            if (toKill)
            {
                Vector3 enemyPosition = toKill.transform.position;

                // If the closest Enemy is in range
                if (Vector3.Distance(enemyPosition, transform.position) < attackRange)
                {
                    soundPlayer.PlayAttackClip();
                    animator.SetTrigger("Attack");
                    delayCurrent = delayMax;
                }
            }
        }
        else if (delayCurrent > 0)
        {
            delayCurrent -= Time.deltaTime;
            agent.speed = 0;
        }
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        scanner = GetComponentInChildren<Scanner>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        animator = GetComponentInChildren<Animator>();
        soundPlayer = GetComponent<SoundScript>();


    }

    public override void SetupBehaviour(AISettings settings)
    {
        this.settings = settings;
        SetupValues();
        AttackScript attack = GetComponentInChildren<AttackScript>();
        if (attack)
        {
            attack.SetupAttack(settings);
        }
    }

    void SetupValues()
    {
        delayMax = settings.AttackDelay;
        delayMax = Randomize(delayMax);

        attackRange = settings.MeleeAttackRange;
        attackRange = Randomize(attackRange);
    }

    float Randomize(float value)
    {
        return Random.Range(1 - maxRandom, 1 + maxRandom) * value;
    }
}
