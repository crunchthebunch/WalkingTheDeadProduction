using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decision/EnemiesNearby")]
public class EnemiesNearbyDecision : Decision
{
    public override bool MakeDecision(StateController controller)
    {
        return AreEnemiesAround(controller);
    }

    private bool AreEnemiesAround(StateController controller)
    {
        // Check whether any enemies are in range
        if (controller.Scanner.ObjectsInRange.Count > 0)
        {
            return true;
        }
        return false;
    }
}
