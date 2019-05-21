using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    // [SerializeField] HumanStats stats;
    [SerializeField] private State currentState = null;
    [SerializeField] protected State remainState = null;
    [SerializeField] protected bool recieveCommands = false;
    private bool feared = false;
    public bool test = false;

    ChaseBehaviour       chaseBehaviour;
    WanderBehaviour      wanderBehaviour;
    MoveToBehaviour      moveToBehaviour;
    MeleeAttackBehaviour attackBehaviour;
    MoveBackBehaviour    moveBackBehaviour;
    FleeBehaviour        fleeBehaviour;
    PatrolBehaviour      patrolBehaviour;

    AISettings settings;
    Scanner scanner;

    bool hasCommand;
    bool isSetup = false;
    float fearTimer = 0;

    public ChaseBehaviour       ChaseBehaviour  { get => chaseBehaviour; }
    public WanderBehaviour      WanderBehaviour { get => wanderBehaviour; }
    public MoveToBehaviour      MoveToBehaviour { get => moveToBehaviour; }
    public MeleeAttackBehaviour AttackBehaviour { get => attackBehaviour; }
    public FleeBehaviour        FleeBehaviour   { get => fleeBehaviour; }
    public PatrolBehaviour      PatrolBehaviour { get => patrolBehaviour; }
    public AISettings           Settings        { get => settings; }
    public Scanner Scanner { get => scanner; }
    public MoveBackBehaviour MoveBackBehaviour { get => moveBackBehaviour; }
    public bool HasCommand { get => hasCommand; set => hasCommand = value; }
    protected State DefaultState { get => currentState; }
    public float FearTimer { get => fearTimer; set => fearTimer = value; }
    public bool Feared { get => feared; }



    // Mandatory to setup
    public void SetupController(AISettings settings)
    {
        this.settings = settings;
        scanner = GetComponentInChildren<Scanner>();
        SetupBehaviours();
        hasCommand = false;
        isSetup = true;
    }

    public void TransitionToState(State nextState)
    {
        // Change the state if the next state is not remaining in state
        if (nextState != remainState)
        {
            currentState = nextState;
        }
    }

    private void OnDrawGizmos()
    {
        if (currentState != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(transform.position, 1.0f);
        }
    }

    private void Update()
    {

        if (test)
        {
            BecomeFeared(5f);
            test = false;
        }

        if (isSetup)
        {
            currentState.UpdateState(this);
        }
    }

    void SetupBehaviours()
    {
        chaseBehaviour = gameObject.AddComponent<ChaseBehaviour>();
        chaseBehaviour.SetupBehaviour(this.settings);

        fleeBehaviour = gameObject.AddComponent<FleeBehaviour>();
        fleeBehaviour.SetupBehaviour(this.settings);

        attackBehaviour = gameObject.AddComponent<MeleeAttackBehaviour>();
        attackBehaviour.SetupBehaviour(this.settings);

        attackBehaviour = gameObject.AddComponent<MeleeAttackBehaviour>();
        attackBehaviour.SetupBehaviour(this.settings);

        moveBackBehaviour = gameObject.AddComponent<MoveBackBehaviour>();
        moveBackBehaviour.SetupBehaviour(this.settings);

        patrolBehaviour = gameObject.AddComponent<PatrolBehaviour>();
        patrolBehaviour.SetupBehaviour(this.settings);

        wanderBehaviour = gameObject.AddComponent<WanderBehaviour>();
        wanderBehaviour.SetupBehaviour(this.settings);
    }

    //-----for recieving commands-----//
    void RecieveCommand(Vector3 position)
    {
        hasCommand = true;
        Vector3 newCenter = position + Random.onUnitSphere * 2.0f;
        moveBackBehaviour.NavigationCenter = newCenter;
        wanderBehaviour.NavigationCenter = newCenter;
    }
    private void OnEnable()
    {
        if(recieveCommands) PlayerCommand.Click += RecieveCommand;
    }

    private void OnDisable()
    {
        if(recieveCommands) PlayerCommand.Click -= RecieveCommand;
    }
    //--------------------------------//

    public void BecomeFeared(float time)
    {
        if (!feared)
        {
            StopCoroutine(BecomeScared(time));
            StartCoroutine(BecomeScared(time));
        }
        
    }

    IEnumerator BecomeScared(float seconds)
    {
        feared = true;

        yield return new WaitForSeconds(seconds);

        feared = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fear Radius"))
        {
            BecomeFeared(5);
        }
    }
}
