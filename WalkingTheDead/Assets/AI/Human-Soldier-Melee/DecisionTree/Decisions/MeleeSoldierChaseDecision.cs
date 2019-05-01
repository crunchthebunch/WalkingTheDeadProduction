using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Human/MeleeSoldier/Decision/Chase")]
public class MeleeSoldierChaseDecision : Decision
{
    public override bool MakeDecision(StateController controller)
    {
        return AreZombiesInRange(controller);
    }

    private bool AreZombiesInRange(StateController controller)
    {
        MeleeSoldierStateController soldierController = controller as MeleeSoldierStateController;
        
        // Last seen Zombie
        if (soldierController.Owner.ZombieScanner.ObjectsInRange.Count > 0)
        {
            // Find the closest Enemy
            GameObject closestEnemy = soldierController.Owner.ZombieScanner.GetClosestTargetInRange();
            Vector3 closestEnemyLocation = closestEnemy.transform.position;

            // Distance for how long the  soldier can chase for
            float chaseDistance = soldierController.Owner.Settings.ChaseDistance;

            // Check whether it's Distance is bigger than the distance between all of it's patrol goals
            foreach (Vector3 patrolPoint in soldierController.Owner.PatrolBehaviour.PatrolPositions)
            {
                
                // If one of the areas is close enough to his patrol Point
                if (Vector3.Distance(patrolPoint, closestEnemyLocation) < chaseDistance)
                {
                    // Keep chasing
                    return true;
                }
            }
        }

        // If all of them are out of range, go back
        return false;
    }
}
