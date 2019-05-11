using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Villager : MonoBehaviour
{
    [SerializeField] VillagerSettings settings = null;
    [SerializeField] GameObject[] deadBodies = null;

    Scanner zombieScanner;
    NavMeshAgent agent;
    GameManager gameManager;

    WanderVillagerBehaviour wanderBehaviour;
    FleeVillagerBehaviour fleeBehaviour;
    MoveBackVillagerBehaviour moveBackBehaviour;

    

    VillagerStateController controller;
    Animator anim;

    public NavMeshAgent Agent { get => agent; }
    public VillagerSettings Settings { get => settings; }
    public Scanner ZombieScanner { get => zombieScanner; }
    public WanderVillagerBehaviour WanderBehaviour { get => wanderBehaviour; }
    public FleeVillagerBehaviour FleeBehaviour { get => fleeBehaviour; }
    public MoveBackVillagerBehaviour MoveBackBehaviour { get => moveBackBehaviour; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = settings.WalkingSpeed;

        anim = GetComponentInChildren<Animator>();
        gameManager = FindObjectOfType<GameManager>();

        // Create Scanner
        zombieScanner = GetComponentInChildren<Scanner>();

        // Add Wander Component
        wanderBehaviour = gameObject.AddComponent<WanderVillagerBehaviour>();

        // Add Flee Behaviour
        fleeBehaviour = gameObject.AddComponent<FleeVillagerBehaviour>();

        // Add Moving Back Behaviour
        moveBackBehaviour = gameObject.AddComponent<MoveBackVillagerBehaviour>();

        // Get the controller - TODO might want to add this component and set it up later on
        controller = GetComponent<VillagerStateController>();
    }

    private void Start()
    {
        zombieScanner.SetupScanner("Zombie", settings.Vision);
    }

    public void Die()
    {
        // Spawn a random dead body
        int bodyIndex = Random.Range(0, deadBodies.Length);
        Vector3 deadPosition = transform.position;
        deadPosition.y = transform.position.y - transform.lossyScale.y;

        Instantiate(deadBodies[bodyIndex], deadPosition, transform.rotation);

        // Kill yourself
        Destroy(gameObject);
    }

    private void Update()
    {
        if (agent.speed == settings.FleeSpeed)
        {
            anim.SetBool("isRunning", true);
            anim.SetBool("isWalking", false);
        }
        else if (agent.speed == settings.WalkingSpeed)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isRunning", false);
        }

        if (agent.isStopped)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
        }
    }
}