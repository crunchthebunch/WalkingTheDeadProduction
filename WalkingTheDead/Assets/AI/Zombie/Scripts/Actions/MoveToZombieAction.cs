using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Zombie/Actions/MoveTo")]
public class MoveToZombieAction : Action
{
    public override void Act(StateController controller)
    {
        MoveTo(controller);
    }

    void MoveTo(StateController controller)
    {
        ZombieStateController zombieController = controller as ZombieStateController;

        zombieController.Owner.MoveToBehaviour.DoBehaviour();
    }
}
