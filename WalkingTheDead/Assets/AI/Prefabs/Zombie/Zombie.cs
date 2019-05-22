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
    SoundScript                 soundPlayer;
    //TODO Get all of these from a resources folder, programatically
    [SerializeField] AISettings settings = null;
    [SerializeField] GameObject[] deadBodies = null;

    float maxHealth = 0;
    float maxRandom = 0.2f;
    float currentHealth = 0;


    public AISettings Settings { get => settings; }
    public Scanner Scanner { get => scanner; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        // Setup Navmesh
        agent = GetComponent<NavMeshAgent>();
        agent.speed = settings.WalkingSpeed;

        //Setup Controller
        controller = GetComponent<StateController>();
        controller.SetupController(settings);

        // Add Scanner
        scanner = GetComponentInChildren<Scanner>();

        //Get Sound Player
        soundPlayer = GetComponent<SoundScript>();
        soundPlayer.PlayRaiseClip();

        SetupHealth();
    }

    private void Start()
    {
        scanner.SetupScanner("Human", settings.VisionRange);
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

    public void TakeDamage(float amount)
    {
        soundPlayer.PlayDamageClip();
        currentHealth -= amount;
        if (currentHealth < 0) Die();
    }

    public void Die()
    {
        soundPlayer.PlayDeathSound();

        // Spawn a random dead body - Currently has 1
        int bodyIndex = Random.Range(0, deadBodies.Length);
        Vector3 deadPosition = transform.position;

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
}