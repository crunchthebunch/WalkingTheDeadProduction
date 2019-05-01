using UnityEngine;

public class AISettings : ScriptableObject
{
    [SerializeField] protected float vision = 5.0f;
    [SerializeField] protected float walkingSpeed = 2.0f;

    public float WalkingSpeed { get => walkingSpeed; }
    public float Vision { get => vision; }
}
