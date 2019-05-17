using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/State")]
public class State : ScriptableObject
{
    [SerializeField] Action[] actions = null;
    [SerializeField] Transition[] transitions = null;
    public Color sceneGizmoColor = Color.grey;

    public void UpdateState(StateController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    private void DoActions(StateController controller)
    {
        foreach (Action action in actions)
        {
            action.Act(controller);
        }
    }

    private void CheckTransitions(StateController controller)
    {
        foreach (Transition transition in transitions)
        {
            bool decisionSucceeded = transition.decisionToMake.MakeDecision(controller);

            // If the decision is made transition to next state
            if (decisionSucceeded)
            {
                controller.TransitionToState(transition.stateIfTrue);
            }
            else
            {
                controller.TransitionToState(transition.stateIfFalse);
            }
        }
    }
}
