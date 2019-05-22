using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decision/EndAttack")]
public class EndAttackDecision : Decision
{
    public override bool MakeDecision(StateController controller)
    {
        if (controller.AttackBehaviour.AttackComplete == true)
        {
            controller.AttackBehaviour.AttackComplete = false;
            return true;
        }
        else return false;
    }
}
