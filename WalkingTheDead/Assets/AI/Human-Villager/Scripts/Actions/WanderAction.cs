using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Human/Villager/Actions/Wander")]
public class WanderAction : Action
{
    public override void Act(StateController controller)
    {
        Wander(controller);
    }

    private void Wander(StateController controller)
    {
        // Access Villager State Controller functions
        VillagerStateController villagerController = controller as VillagerStateController;

        villagerController.Owner.WanderBehaviour.DoBehaviour();
    }
}
