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
    StateController               controller;
    GameObject player;
    //TODO Get all of these from a resources folder, programatically
    [SerializeField] AISettings settings = null;
    [SerializeField] GameObject[] deadBodies = null;   

    public AISettings Settings { get => settings; }
    public Scanner Scanner { get => scanner; }

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        //Find Player
        player = GameObject.Find("PlayerCharacter");

        // Setup Navmesh
        agent = GetComponent<NavMeshAgent>();
        agent.speed = settings.WalkingSpeed;

        //Setup Controller
        controller = GetComponent<StateController>();
        controller.SetupController(settings);

        // Add Scanner
        scanner = GetComponentInChildren<Scanner>();
    }

    private void Start()
    {
        scanner.SetupScanner("Human", settings.VisionRange);
    }

    // Update is called once per frame
    void Update()
    {
        controller.MoveBackBehaviour.NavigationCenter = player.transform.position;

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