using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Human/Villager/Decision/FledEnough")]
public class InSafeDistanceDecision : Decision
{
    public override bool MakeDecision(StateController controller)
    {
        return IsInSafeDistance(controller);
    }

    private bool IsInSafeDistance(StateController controller)
    {
        // Access Villager State Controller functions
        VillagerStateController villagerController = controller as VillagerStateController;

        // Check whether we travelled enough
        if (Vector3.Distance(villagerController.Owner.transform.position, villagerController.Owner.ZombieScanner.LastKnownObjectLocation) 
            > villagerController.Settings.FleeDistance - 2.0f
            && villagerController.Owner.ZombieScanner.ObjectsInRange.Count == 0)
        {
            return true;
        }
        return false;
    }
}
