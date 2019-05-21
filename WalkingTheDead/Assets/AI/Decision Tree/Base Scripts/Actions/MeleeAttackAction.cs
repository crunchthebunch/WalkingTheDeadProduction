using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Action/MeleeAttack")]
public class MeleeAttackAction : Action
{
    public override void Act(StateController controller)
    {
        controller.AttackBehaviour.DoBehaviour();
    }
}
