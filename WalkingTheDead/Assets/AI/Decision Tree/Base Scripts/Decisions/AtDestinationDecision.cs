using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decision/AtDestination")]
public class AtDestinationDecision : Decision
{
    public override bool MakeDecision(StateController controller)
    {
        return AtDestination(controller);
    }

    private bool AtDestination(StateController controller)
    {
        // Check whether the object has arrived to it's original location
        if (Vector3.Distance(controller.transform.position, controller.MoveBackBehaviour.NavigationCenter)
            < controller.Settings.NavigationRadius)
        {
            controller.HasCommand = false;
            return true;
        }
        return false;
    }
}
