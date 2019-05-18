﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

struct AnimationID
{
    public void SetupAnimation(string name)
    {
        this.name = name;
        this.hashID = Animator.StringToHash(name);
    }

    string name;
    int hashID;

    public string Name { get => name; }
    public int HashID { get => hashID; }
}
public class Villager : MonoBehaviour
{
    [SerializeField] AISettings settings = null;
    [SerializeField] GameObject[] deadBodies = null;

    Scanner         scanner;
    NavMeshAgent    agent;
    AnimationID     idleAnimation;
    StateController controller;
    Animator        animator;

    public NavMeshAgent Agent { get => agent; }
    public Scanner ZombieScanner { get => scanner; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = settings.WalkingSpeed;

        animator = GetComponentInChildren<Animator>();

        // Create Scanner
        scanner = GetComponentInChildren<Scanner>();

        // Get the controller - TODO might want to add this component and set it up later on
        controller = GetComponent<StateController>();
        controller.SetupController(settings);
    }

    private void Start()
    {
        scanner.SetupScanner("Zombie", settings.VisionRange);
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
        if (agent.speed == settings.RunSpeed)
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isWalking", false);
        }
        else if (agent.speed == settings.WalkingSpeed)
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
        }

        if (agent.isStopped)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
        }
    }
}