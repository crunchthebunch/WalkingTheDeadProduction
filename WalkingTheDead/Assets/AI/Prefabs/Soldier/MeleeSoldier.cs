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
    [SerializeField] AISettings settings = null;
    [SerializeField] List<GameObject> additionalPatrolpositions = new List<GameObject>();
    [SerializeField] GameObject [] deadBodies = null;

    AudioSource soldierAudioSource;
    NavMeshAgent agent;
    Scanner scanner;
    Animator animator;
    AnimationHashIDs animationIDs;
    StateController controller;

    public List<GameObject> additionalPatrolPositions { get => additionalPatrolpositions; }
    public Animator Animator { get => animator;  }
    internal AnimationHashIDs AnimationIDs { get => animationIDs; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = settings.WalkingSpeed;

        soldierAudioSource = GetComponent<AudioSource>();

        // Add Scanner
        scanner = GetComponentInChildren<Scanner>();

        //Add controller
        controller = gameObject.GetComponent<StateController>();
        controller.SetupController(settings);
        controller.PatrolBehaviour.SetupPatrolPositions(additionalPatrolPositions);

        // Setup animations
        animator = GetComponentInChildren<Animator>();
        animationIDs.isChasingID = Animator.StringToHash("isChasing");
        animationIDs.isWalkingID = Animator.StringToHash("isWalking");
        animationIDs.attackID = Animator.StringToHash("attack");
    }

    private void Start()
    {
        scanner.SetupScanner("Zombie", settings.VisionRange);
        scanner.SetupScanner("Necromancer", settings.VisionRange);
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
        if (agent.speed == settings.RunSpeed)
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
}
