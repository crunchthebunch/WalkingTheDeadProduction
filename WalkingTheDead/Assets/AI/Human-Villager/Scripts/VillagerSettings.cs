using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Settings/Villager")]
public class VillagerSettings : AISettings
{
    [SerializeField] float walkRadius = 10f;
    [SerializeField] float wanderDelay = 2.0f;
    [SerializeField] float fleeSpeed = 5.0f;
    [SerializeField] float fleeDistance = 10.0f;
    
    
    public float WalkRadius { get => walkRadius; }
    public float WanderDelay { get => wanderDelay; }
    public float FleeSpeed { get => fleeSpeed; }
    public float FleeDistance { get => fleeDistance; }
}
