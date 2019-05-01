using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Zombie/Actions/Attack")]
public class AttackZombieAction : Action
{
    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    void Attack(StateController controller)
    {
        ZombieStateController zombieController = controller as ZombieStateController;

        zombieController.Owner.AttackBehaviour.DoBehaviour();
    }
}
