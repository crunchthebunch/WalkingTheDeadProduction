using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decision/StopFlee")]
public class StopFleeDecision : Decision
{
    public override bool MakeDecision(StateController controller)
    {
        return IsInSafeDistance(controller);
    }

    private bool IsInSafeDistance(StateController controller)
    {
        // Check whether we travelled enough
        if (Vector3.Distance(controller.transform.position, controller.Scanner.LastKnownObjectLocation)
            > controller.FleeBehaviour.FleeDistance
            && controller.Scanner.ObjectsInRange.Count == 0)
        {
            return true;
        }
        return false;
    }
}
