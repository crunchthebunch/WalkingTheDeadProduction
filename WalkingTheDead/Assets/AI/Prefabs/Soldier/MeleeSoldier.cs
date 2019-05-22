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
    ParticleSystem blood;

    float currentHealth = 0;
    float maxHealth = 0;
    float maxRandom = 0.2f;

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

        SetupHealth();

        blood = GetComponentInChildren<ParticleSystem>();
        blood.Stop();
    }

    private void Start()
    {
        scanner.SetupScanner("Zombie", settings.VisionRange);
        scanner.SetupScanner("Necromancer", settings.VisionRange);
    }

    public void TakeDamage(float amount)
    {
        blood.Play();
        currentHealth -= amount;
        if (currentHealth < 0) Die();
    }

    public void Die()
    {
        // Spawn a random dead body - Currently has 1
        int bodyIndex = Random.Range(0, deadBodies.Length);
        Vector3 deadPosition =new Vector3(transform.position.x, transform.position.y - 1.0f, transform.position.z);

        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.y = Random.Range(0, 360);

        Instantiate(deadBodies[bodyIndex], deadPosition, Quaternion.Euler(rotation));

        // Kill yourself
        Destroy(gameObject);
    }

    void SetupHealth()
    {
        maxHealth = settings.HealthMax;
        maxHealth = Randomize(maxHealth);
        currentHealth = maxHealth;
    }

    float Randomize(float value)
    {
        return Random.Range(1 - maxRandom, 1 + maxRandom) * value;
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
