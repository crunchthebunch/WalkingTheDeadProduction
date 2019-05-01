using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Human/MeleeSoldier/Actions/Chase")]
public class ChaseMeleeSoldierAction : Action
{
    public override void Act(StateController controller)
    {
        Chase(controller);
    }

    private void Chase(StateController controller)
    {
        MeleeSoldierStateController soldierController = controller as MeleeSoldierStateController;

        soldierController.Owner.ChaseBehaviour.DoBehaviour();
    }
}
