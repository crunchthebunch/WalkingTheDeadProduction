using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decision/SoldierChase")]
public class MeleeSoldierChaseDecision : Decision
{
    public override bool MakeDecision(StateController controller)
    {
        return AreZombiesInRange(controller);
    }

    private bool AreZombiesInRange(StateController controller)
    {
        // Last seen Zombie
        if (controller.Scanner.ObjectsInRange.Count > 0)
        {
            // Find the closest Enemy
            GameObject closestEnemy = controller.Scanner.GetClosestTargetInRange();

            if (closestEnemy)
            {
                Vector3 closestEnemyLocation = closestEnemy.transform.position;

                // Distance for how long the  soldier can chase for
                float chaseDistance = controller.ChaseBehaviour.ChaseDistance;

                // Check whether it's Distance is bigger than the distance between all of it's patrol goals
                foreach (Vector3 patrolPoint in controller.PatrolBehaviour.PatrolPositions)
                {

                    // If one of the areas is close enough to his patrol Point
                    if (Vector3.Distance(patrolPoint, closestEnemyLocation) < chaseDistance)
                    {
                        // Keep chasing
                        return true;
                    }
                }
            }
        }

        // If all of them are out of range, go back
        return false;
    }
}
