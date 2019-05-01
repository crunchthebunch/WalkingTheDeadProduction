using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Zombie/Actions/Chase")]
public class ChaseZombieAction : Action
{
    public override void Act(StateController controller)
    {
        Chase(controller);
    }

    private void Chase(StateController controller)
    {
        ZombieStateController zombieController = controller as ZombieStateController;

        zombieController.Owner.ChaseBehaviour.DoBehaviour();
    }
}
