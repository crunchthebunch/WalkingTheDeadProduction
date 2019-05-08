using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    // [SerializeField] HumanStats stats;
    [SerializeField] protected State currentState = null;
    [SerializeField] protected State remainState = null;

    ChaseBehaviour chaseBehaviour;
    WanderBehaviour wanderBehaviour;
    MoveToBehaviour moveToBehaviour;
    MeleeAttackBehaviour attackBehaviour;
    FleeBehaviour fleeBehaviour;

    AISettings settings;

    public ChaseBehaviour ChaseBehaviour { get => chaseBehaviour; }
    public WanderBehaviour WanderBehaviour { get => wanderBehaviour; }
    public MoveToBehaviour MoveToBehaviour { get => moveToBehaviour; }
    public MeleeAttackBehaviour AttackBehaviour { get => attackBehaviour; }
    public FleeBehaviour FleeBehaviour { get => fleeBehaviour; }

    // Mandatory to setup
    public void SetupController(AISettings settings)
    {
        this.settings = settings;

        // Add behaviours
        chaseBehaviour = gameObject.AddComponent<ChaseBehaviour>();
        chaseBehaviour.SetupBehaviour(this.settings);

        wanderBehaviour = gameObject.AddComponent<WanderBehaviour>();
        wanderBehaviour.SetupBehaviour(this.settings);

        moveToBehaviour = gameObject.AddComponent<MoveToBehaviour>();
        moveToBehaviour.SetupBehaviour(this.settings);

        attackBehaviour = gameObject.AddComponent<MeleeAttackBehaviour>();
        attackBehaviour.SetupBehaviour(this.settings);

        fleeBehaviour = gameObject.AddComponent<FleeBehaviour>();
        fleeBehaviour.SetupBehaviour(this.settings);

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
}
