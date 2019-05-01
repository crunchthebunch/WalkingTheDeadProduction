using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    // [SerializeField] HumanStats stats;
    [SerializeField] protected State currentState = null;
    [SerializeField] protected State remainState = null;

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
