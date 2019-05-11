using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Inner class for animation
struct AnimationHashIDs
{
    public int isChasingID;
    public int isWalkingID;
    public int attackID;
}

public class MeleeSoldier : MonoBehaviour
{
    [SerializeField] MeleeSoldierSettings settings = null;
    [SerializeField] List<GameObject> additionalPatrolpositions = new List<GameObject>();

    [SerializeField] GameObject [] deadBodies = null;

    AudioSource soldierAudioSource;

    GameManager gameManager;
    NavMeshAgent agent;
    Scanner zombieScanner;
    Animator animator;
    AnimationHashIDs animationIDs;

    // Behaviours
    PatrolMeleeSoldierBehaviour patrolBehaviour;
    ChaseMeleeSoldierBehaviour chaseBehaviour;
    AttackMeleeSoldierBehaviour attackBehaviour;

    public MeleeSoldierSettings Settings { get => settings; }
    public NavMeshAgent Agent { get => agent; }

    // Behaviours
    public PatrolMeleeSoldierBehaviour PatrolBehaviour { get => patrolBehaviour; }
    public ChaseMeleeSoldierBehaviour ChaseBehaviour { get => chaseBehaviour; }
    public AttackMeleeSoldierBehaviour AttackBehaviour { get => attackBehaviour; }

    public List<GameObject> AdditionalPatrolpositions { get => additionalPatrolpositions; }
    public Scanner ZombieScanner { get => zombieScanner; }
    public GameManager GameManager { get => gameManager; }
    public Animator Animator { get => animator;  }
    internal AnimationHashIDs AnimationIDs { get => animationIDs; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = settings.WalkingSpeed;

        gameManager = FindObjectOfType<GameManager>();

        soldierAudioSource = GetComponent<AudioSource>();

        // Setup animations
        animator = GetComponentInChildren<Animator>();
        animationIDs.isChasingID = Animator.StringToHash("isChasing");
        animationIDs.isWalkingID = Animator.StringToHash("isWalking");
        animationIDs.attackID = Animator.StringToHash("attack");

        // Add Scanner
        zombieScanner = transform.Find("EnemyScanner").GetComponent<Scanner>();

        // Add patrol behaviour
        patrolBehaviour = gameObject.AddComponent<PatrolMeleeSoldierBehaviour>();

        // Add chase behaviour
        chaseBehaviour = gameObject.AddComponent<ChaseMeleeSoldierBehaviour>();

        // Add attack behaviour
        attackBehaviour = gameObject.AddComponent<AttackMeleeSoldierBehaviour>();

        
    }

    // Start is called before the first frame update
    void Start()
    {
        // Add 2 tags
        zombieScanner.SetupScanner("Zombie", settings.Vision);
        zombieScanner.SetupScanner("Necromancer", settings.Vision);
    }

    public void Die()
    {
        // Spawn a random dead body
        int bodyIndex = Random.Range(0, deadBodies.Length);
        Vector3 deadPosition = transform.position;
        deadPosition.y = transform.position.y - transform.localScale.y;

        Instantiate(deadBodies[bodyIndex], deadPosition, transform.rotation);

        soldierAudioSource.Play();
        // Kill yourself
        Destroy(gameObject);
    }

    void UpdateAnimations()
    {
        // Chasing State
        if (agent.speed == settings.ChaseSpeed)
        {
            animator.SetBool(animationIDs.isChasingID, true);
            animator.SetBool(animationIDs.isWalkingID, false);
        }
        // Walking Animation
        else if (agent.speed == settings.WalkingSpeed)
        {
            animator.SetBool(animationIDs.isWalkingID, true);
            animator.SetBool(animationIDs.isChasingID, false);
        }

        // Idle
        if (agent.velocity.magnitude < settings.WalkingSpeed / 2.0f)
        {
            animator.SetBool(animationIDs.isWalkingID, false);
            animator.SetBool(animationIDs.isChasingID, false);
        }
    }

    private void Update()
    {
        UpdateAnimations();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Vector3 eyePosition = transform.position;
        eyePosition.y = transform.position.y + transform.localScale.y / 2f;

        Gizmos.DrawRay(eyePosition, transform.forward * settings.PatrolDistance);
    }

}
