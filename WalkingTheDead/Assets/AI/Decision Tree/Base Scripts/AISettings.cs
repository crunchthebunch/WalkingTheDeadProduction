using UnityEngine;

[CreateAssetMenu(menuName = "AI/Setting")]
public class AISettings : ScriptableObject
{
    [SerializeField] protected float visionRange      = 5.0f;
    [SerializeField] protected float walkSpeed        = 2.0f;
    [SerializeField] protected float runSpeed         = 8.0f;
    [SerializeField] protected float meleeAttackRange = 1.0f;
    [SerializeField] protected float attackDamage     = 1.0f;
    [SerializeField] protected float navigationRadius = 5.0f;
    [SerializeField] protected float attackDelay      = 1.0f;

    public float WalkingSpeed     { get => walkSpeed; }
    public float VisionRange      { get => visionRange; }
    public float RunSpeed         { get => runSpeed;  }
    public float MeleeAttackRange { get => meleeAttackRange; }
    public float AttackDamage     { get => attackDamage; }
    public float NavigationRadius { get => navigationRadius; }
    public float AttackDelay      { get => attackDelay; }
}
