using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum FollowTarget
{
    PLAYER,
    HUMANS,
    COMMAND
}

public class ZombieScript : MonoBehaviour
{
    public Animator anim;

    float wanderRadius = 5.0f;
    float wanderChance = 0.001f;
    float wanderSpeed = 1;
    bool wandering;

    

    float moveSpeed = 2.0f;
    
    Vector3 desiredPosition;
    Vector3 commandPosition;
    Vector3 wanderPosition = Vector3.negativeInfinity;

    GameObject player;
    NavMeshAgent agent;
    
    List<Collider> humansInRange = new List<Collider>();
    Collider closestHuman;

    // SphereCollider detectionRange; - Moved into the Scanner
    Scanner humanScanner;
    CapsuleCollider attackRange;

    Camera mainCamera;
    FollowTarget target = FollowTarget.PLAYER;

    

    private void Awake()
    {
        player = GameObject.Find("PlayerCharacter");
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        attackRange = GetComponent<CapsuleCollider>();
        mainCamera = GameObject.Find("PlayerCharacter/Camera").GetComponent<Camera>();
        
        // Get the Scanner
        humanScanner = GetComponentInChildren<Scanner>();
       

    }

    private void Start()
    {
        humanScanner.SetupScanner("Human", 10f); // TODO change this based on the zombie setting
    }

    // TODO Make this into a decision (in the zombie state controller)
    private void SearchForHumans()
    {
        // Check whether there are any humans in range
        if (humanScanner.ObjectsInRange.Count > 0)
        {
            // If there are make them targets
            closestHuman = humanScanner.GetClosestTargetInRange().GetComponent<CapsuleCollider>();
            target = FollowTarget.HUMANS;
            wandering = false;
        }
        else
        {
            if (commandPosition == Vector3.negativeInfinity) target = FollowTarget.PLAYER;
            else target = FollowTarget.COMMAND;
        }
    }

    private void Update()
    {
        ProcessInput();
        SearchForHumans();
        SetDesiredPosition();
        SetWandering();
        Move();

        if (agent.speed == wanderSpeed || agent.speed == moveSpeed)
        {
            anim.SetBool("isZombieWalking", true);
        }
        else
        {
            anim.SetBool("isZombieWalking", false);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Human"))
        {
            humansInRange.Add(other);
            GetClosestHuman();
            target = FollowTarget.HUMANS;
            wandering = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Human"))
        {
            for (int i = 0; i < humansInRange.Count; i++)
            {
                if (humansInRange[i] == other)
                {
                    humansInRange.RemoveAt(i);
                    if (humansInRange.Count == 0)
                    {
                        if (commandPosition == Vector3.negativeInfinity) target = FollowTarget.PLAYER;
                        else target = FollowTarget.COMMAND;
                    }
                    break;
                }
            }
        }
    }

    void SetDesiredPosition()
    {
        if (target == FollowTarget.PLAYER)
        {
            if (player.transform.position != desiredPosition)
            {
                wandering = false;
                desiredPosition = player.transform.position;
            }
        }
        else if (target == FollowTarget.COMMAND)
        {
            desiredPosition = commandPosition;
        }
        else if (target == FollowTarget.HUMANS)
        {
            desiredPosition = closestHuman.transform.position;
        }
    }

    void GetClosestHuman()
    {
        closestHuman = humansInRange[0];
        
        for (int i = 1; i < humansInRange.Count; i++)
        {
            if (Vector3.Distance(transform.position, humansInRange[i].transform.position) < Vector3.Distance(transform.position, closestHuman.transform.position))
            {
                closestHuman = humansInRange[i];
            }
        }
    }

    void ProcessInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            wandering = false;

            GetCommandPosition();
            target = FollowTarget.COMMAND;
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            wandering = false;
            target = FollowTarget.PLAYER;
            commandPosition = Vector3.negativeInfinity;
        }
    }

    void GetCommandPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            commandPosition = hitInfo.point;

            //if (particleEffectActive == false)
            //{
                //clickSystemEffect.transform.position = hitInfo.point;
                //clickSystemEffect.Play();
                //var emission = clickSystemEffect.emission;
                //emission.enabled = true;
                //particleEffectActive = true;
            //}
            
        }
        else commandPosition = Vector3.negativeInfinity;
    }

    //Added
    void Wander()
    {
        if (wanderPosition == Vector3.negativeInfinity || Random.value < wanderChance)
        {
            wanderPosition = desiredPosition + Random.insideUnitSphere * wanderRadius;
        }

        agent.SetDestination(wanderPosition);
    }

    //Added
    void SetWandering()
    {
        if (Mathf.Abs(Vector3.Distance(transform.position, desiredPosition)) < wanderRadius && target != FollowTarget.HUMANS && wandering == false)
        {
            wandering = true;
            wanderPosition = desiredPosition + Random.onUnitSphere * wanderRadius;
        }

        agent.speed = wanderSpeed;
    }

    void Move()
    {
        if (wandering)
        {
            Wander();
        }
        else
        {
            agent.speed = moveSpeed;
            agent.SetDestination(desiredPosition);
            
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;
        Gizmos.color = new Color(0, 0, 1, 0.1f);
        Gizmos.color = new Color(1, 0.8f, 0.016f, 0.1f);
        Gizmos.DrawSphere(desiredPosition, 2f);
        Gizmos.DrawSphere(desiredPosition, wanderRadius);
        if (humansInRange.Count > 0)
        {
            Gizmos.color = Color.grey;
            for (int i = 0; i < humansInRange.Count; i++)
            {
                Gizmos.DrawLine(transform.position, humansInRange[i].transform.position);
            }

            if (closestHuman != null)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawLine(transform.position, closestHuman.transform.position);
            }
        }
        
    }
}
