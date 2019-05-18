using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decision/Attack")]
public class AttackDecision : Decision
{
    public override bool MakeDecision(StateController controller)
    {
        return EnemyInAttackRange(controller);
    }

    bool EnemyInAttackRange(StateController controller)
    {
        if (controller.Scanner.ObjectsInRange.Count > 0)
        {
            if (Mathf.Abs(Vector3.Distance(controller.Scanner.GetClosestTargetInRange().transform.position, controller.transform.position)) < controller.Settings.MeleeAttackRange)
            {
                return true;
            }
        }
        return false;
    }
}
