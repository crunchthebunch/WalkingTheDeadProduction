using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Zombie/Decision/Chase")]
public class ChaseZombieDecision : Decision
{
    public override bool MakeDecision(StateController controller)
    {
        return AreHumansInRange(controller);
    }

    private bool AreHumansInRange(StateController controller)
    {
        // Access Villager State Controller functions
        ZombieStateController zombieController = controller as ZombieStateController;

        // Check whether any zombies are in range
        if (zombieController.Owner.HumanScanner.ObjectsInRange.Count > 0)
        {
            return true;
        }
        return false;
    }
}
