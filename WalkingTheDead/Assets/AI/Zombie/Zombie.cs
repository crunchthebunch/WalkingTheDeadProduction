using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    //-----Components
    Scanner                       scanner;
    NavMeshAgent                  agent;
    Animator                      animator;
    ZombieStateController zombieController;
    //TODO Get all of these from a resources folder, programatically
    [SerializeField] AISettings settings = null;
    [SerializeField] GameObject[] deadBodies = null;

    //-----Behaviours
    ChaseBehaviour       chaseBehaviour;
    WanderBehaviour      wanderBehaviour;
    MoveToBehaviour      moveToBehaviour;
    MeleeAttackBehaviour attackBehaviour;
   
    //-----Getters/Setters
    //Behaviours
    public ChaseBehaviour ChaseBehaviour { get => chaseBehaviour; }
    public WanderBehaviour WanderBehaviour { get => wanderBehaviour; }
    public MoveToBehaviour MoveToBehaviour { get => moveToBehaviour; }
    public MeleeAttackBehaviour AttackBehaviour { get => attackBehaviour; }
    public AISettings Settings { get => settings; }

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        // Setup Navmesh
        agent = GetComponent<NavMeshAgent>();
        agent.speed = settings.WalkingSpeed;

        zombieController = GetComponent<ZombieStateController>();
        zombieController.SetupController(settings);

        // Add Scanner
        scanner = transform.Find("HumanScanner").GetComponent<Scanner>();
        scanner.SetupScanner("Human", settings.VisionRange);

        // Add Chase Behaviour
        chaseBehaviour = gameObject.AddComponent<ChaseBehaviour>();
        chaseBehaviour.SetupBehaviour(settings);

        // Add wander behaviour
        wanderBehaviour = gameObject.AddComponent<WanderBehaviour>();
        wanderBehaviour.SetupBehaviour(settings);

        // Add moveto Behaviour
        moveToBehaviour = gameObject.AddComponent<MoveToBehaviour>();
        moveToBehaviour.SetupBehaviour(settings);

        // Add Attack Behaviour
        attackBehaviour = gameObject.AddComponent<MeleeAttackBehaviour>();
        attackBehaviour.SetupBehaviour(settings);
    }

    // Update is called once per frame
    void Update()
    {
        // Running
        if (agent.speed == settings.RunSpeed)
        {
            animator.SetBool("Charging", true);
            animator.SetBool("Walking", true);
        }
        // Walking
        else
        {
            animator.SetBool("Charging", false);
            animator.SetBool("Walking", true);
        }

        // Idle
        if (agent.velocity.magnitude < settings.WalkingSpeed / 2.0f)
        {
            animator.SetBool("Charging", false);
            animator.SetBool("Walking", false);
        }
    }
    public void Die()
    {
        // Spawn a random dead body - Currently has 1
        int bodyIndex = Random.Range(0, deadBodies.Length);
        Vector3 deadPosition = transform.position;

        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.y = 90.0f;

        Instantiate(deadBodies[bodyIndex], deadPosition, Quaternion.Euler(rotation));

        // Kill yourself
        Destroy(gameObject);
    }
}