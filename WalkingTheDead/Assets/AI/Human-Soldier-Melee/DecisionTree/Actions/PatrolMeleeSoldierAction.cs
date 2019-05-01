using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Human/MeleeSoldier/Actions/Patrol")]
public class PatrolMeleeSoldierAction : Action
{
    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    private void Patrol(StateController controller)
    {
        MeleeSoldierStateController soldierController = controller as MeleeSoldierStateController;

        soldierController.Owner.PatrolBehaviour.DoBehaviour();
    }
}
