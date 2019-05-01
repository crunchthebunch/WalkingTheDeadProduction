using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Zombie/Actions/Wander")]
public class WanderZombieAction : Action
{
    public override void Act(StateController controller)
    {
        Wander(controller);
    }

    void Wander(StateController controller)
    {
        ZombieStateController zombieController = controller as ZombieStateController;
        zombieController.Owner.WanderBehaviour.DoBehaviour();
    }
}
