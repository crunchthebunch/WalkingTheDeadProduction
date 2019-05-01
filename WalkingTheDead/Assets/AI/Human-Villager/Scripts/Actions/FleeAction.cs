using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Human/Villager/Actions/Flee")]
public class FleeAction : Action
{
    public override void Act(StateController controller)
    {
        Flee(controller);
    }

    private void Flee(StateController controller)
    {
        // Access Villager State Controller functions
        VillagerStateController villagerController = controller as VillagerStateController;

        villagerController.Owner.FleeBehaviour.DoBehaviour();
    }
}
