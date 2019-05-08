using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Human/Villager/Actions/MoveBack")]
public class MoveBackAction : Action
{
    public override void Act(StateController controller)
    {
        MoveBackToNavigationCenter(controller);
    }

    private void MoveBackToNavigationCenter(StateController controller)
    {
        controller.MoveBackBehaviour.DoBehaviour();
    }
}
