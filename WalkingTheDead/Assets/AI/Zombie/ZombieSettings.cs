using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Settings/Zombie")]
public class ZombieSettings : AISettings
{
    [SerializeField] float walkRadius = 5.0f;
    [SerializeField] float wanderChance = 0.001f;
    [SerializeField] float chaseSpeed = 8.0f;
    [SerializeField] float attackRange = 2.0f;

    public float WalkRadius { get => walkRadius; }
    public float WanderChance { get => wanderChance; }
    public float ChaseSpeed { get => chaseSpeed; }
    public float AttackRange { get => attackRange; }
}
