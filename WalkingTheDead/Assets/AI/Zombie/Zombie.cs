using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    [SerializeField] ZombieSettings settings = null;
    [SerializeField] GameObject[] deadBodies = null;

    public AudioSource zombeAudioSource;
    
    //-----for randomisation
    float maxRandom = 0.25f;
    float walkSpeedModifier = 0.0f;


    Scanner humanScanner;
    NavMeshAgent agent;
    ChaseZombieBehaviour chaseBehaviour;
    WanderZombieBehaviour wanderBehaviour;
    MoveToZombieBehaviour moveToBehaviour;
    AttackZombieBehaviour attackBehaviour;


    Vector3 desiredPosition;
    bool commandGiven;
    bool followPlayer;
    GameObject player;
    GameManager gameManager;
    

    public ZombieSettings Settings { get => settings; }
    public Scanner HumanScanner { get => humanScanner; }
    public NavMeshAgent Agent { get => agent; }
    public ChaseZombieBehaviour ChaseBehaviour { get => chaseBehaviour; }
    public Vector3 DesiredPosition { get => desiredPosition; set => desiredPosition = value; }
    public WanderZombieBehaviour WanderBehaviour { get => wanderBehaviour;}
    public bool CommandGiven { get => commandGiven; set => commandGiven = value; }
    public MoveToZombieBehaviour MoveToBehaviour { get => moveToBehaviour;}
    public bool FollowPlayer { get => followPlayer; set => followPlayer = value; }
    public GameObject Player { get => player;}
    public AttackZombieBehaviour AttackBehaviour { get => attackBehaviour;}
    public Animator Anim { get => anim; set => anim = value; }
    public float WalkSpeedModifier { get => walkSpeedModifier; set => walkSpeedModifier = value; }

    ZombieStateController controller;
    Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        player = GameObject.Find("PlayerCharacter");
        desiredPosition = transform.position;
        commandGiven = false;

        //zombeAudioSource.Play();

        RandomiseModifiers();

        // Find Game Manager
        gameManager = FindObjectOfType<GameManager>();

        // Setup Navmesh
        agent = GetComponent<NavMeshAgent>();
        agent.speed = settings.WalkingSpeed;

        // Add Scanner
        humanScanner = transform.Find("HumanScanner").GetComponent<Scanner>();

        // Add Chase Behaviour
        chaseBehaviour = gameObject.AddComponent<ChaseZombieBehaviour>();

        // Add wander behaviour
        wanderBehaviour = gameObject.AddComponent<WanderZombieBehaviour>();

        // Add moveto Behaviour
        moveToBehaviour = gameObject.AddComponent<MoveToZombieBehaviour>();

        // Add Attack Behaviour
        attackBehaviour = gameObject.AddComponent<AttackZombieBehaviour>();
    }

    private void Start()
    {
        humanScanner.SetupScanner("Human", settings.Vision);
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer) desiredPosition = player.transform.position;

        // Charging
        if (agent.speed == settings.ChaseSpeed)
        {
            anim.SetBool("Charging", true);
            anim.SetBool("Walking", true);
        }
        // Walking
        else
        {
            anim.SetBool("Charging", false);
            anim.SetBool("Walking", true);
        }

        // Idle
        if (agent.velocity.magnitude < settings.WalkingSpeed / 2.0f)
        {
            anim.SetBool("Charging", false);
            anim.SetBool("Walking", false);
        }
    }



    private void OnEnable()
    {
        PlayerCommand.Click += RecieveCommand;
    }

    private void OnDisable()
    {
        PlayerCommand.Click -= RecieveCommand;
    }

    public void Die()
    {
        // Spawn a random dead body - Currently has 1
        int bodyIndex = Random.Range(0, deadBodies.Length);
        Vector3 deadPosition = transform.position;

        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.y = 90.0f;

        Instantiate(deadBodies[bodyIndex], deadPosition, Quaternion.Euler(rotation));

        gameManager.numberOFZombies--;

        // Kill yourself
        Destroy(gameObject);
    }

    void RecieveCommand(Vector3 position, bool followPlayer)
    {
        desiredPosition = position;
        agent.SetDestination(desiredPosition);
        commandGiven = true;
        this.followPlayer = followPlayer;
    }

    void RandomiseModifiers()
    {
        walkSpeedModifier =  1 + Random.Range(-maxRandom, maxRandom);
    }
}