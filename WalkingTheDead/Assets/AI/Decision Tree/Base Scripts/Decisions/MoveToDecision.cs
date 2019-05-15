using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decision/MoveTo")]
public class MoveToDecision : Decision
{
    public override bool MakeDecision(StateController controller)
    {
        return NotAtDestination(controller);
    }

    bool NotAtDestination(StateController controller)
    {
        if (Vector3.Distance(controller.transform.position, controller.MoveToBehaviour.Destination) > controller.Settings.NavigationRadius)
        {
            return true;
        }
        return false;
    }
}
