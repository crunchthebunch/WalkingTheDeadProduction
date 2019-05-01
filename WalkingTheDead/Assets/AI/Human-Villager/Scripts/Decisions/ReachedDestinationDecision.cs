using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Human/Villager/Decision/MoveBack")]
public class ReachedDestinationDecision : Decision
{
    public override bool MakeDecision(StateController controller)
    {
        return HasReachedDestination(controller);
    }

    private bool HasReachedDestination(StateController controller)
    {
        // Access Villager State Controller functions
        VillagerStateController villagerController = controller as VillagerStateController;

        // Check whether the object has arrived to it's original location
        if (Vector3.Distance(villagerController.Owner.transform.position, villagerController.Owner.MoveBackBehaviour.NavigationCenter)
            < 1.0f)
        {
            return true;
        }
        return false;
    }
}
