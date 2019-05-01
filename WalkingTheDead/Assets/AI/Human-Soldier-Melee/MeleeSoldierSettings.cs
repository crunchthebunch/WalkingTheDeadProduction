using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Settings/MeleeSoldier")]
public class MeleeSoldierSettings : AISettings
{
    [SerializeField] private float chaseSpeed = 4.0f;
    [SerializeField] private float chaseDistance = 20.0f;
    [SerializeField] private float patrolDelay = 5.0f;
    [SerializeField] private float patrolDistance = 5.0f;
    [SerializeField] private float attackDistance = 3.0f;
    [SerializeField] private float attackDelay = 0.5f;

    public float PatrolDelay { get => patrolDelay; }
    public float ChaseSpeed { get => chaseSpeed; }
    public float ChaseDistance { get => chaseDistance; }
    public float PatrolDistance { get => patrolDistance; }
    public float AttackDistance { get => attackDistance; }
    public float AttackDelay { get => attackDelay; }
}
