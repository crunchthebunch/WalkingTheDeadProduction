using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Zombie/Decision/Attack")]
public class AttackZombieDecision : Decision
{
    public override bool MakeDecision(StateController controller)
    {
        return VillagerInRange(controller);
    }

    bool VillagerInRange(StateController controller)
    {
        ZombieStateController zombieController = controller as ZombieStateController;

        if (zombieController.Owner.HumanScanner.ObjectsInRange.Count > 0)
        {
            if (Mathf.Abs(Vector3.Distance(zombieController.Owner.HumanScanner.GetClosestTargetInRange().transform.position, zombieController.Owner.transform.position)) < zombieController.Settings.AttackRange)
            {
                
                return true;
            }
        }
        return false;
    }
}
