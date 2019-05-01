using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Zombie/Decision/MoveTo")]
public class MoveToZombieDecision : Decision
{
    public override bool MakeDecision(StateController controller)
    {
        return CommandRecieved(controller);
    }

    bool CommandRecieved(StateController controller)
    {
        ZombieStateController zombieController = controller as ZombieStateController;

        if (zombieController.Owner.CommandGiven || (Mathf.Abs(Vector3.Distance(zombieController.Owner.transform.position, zombieController.Owner.DesiredPosition)) > zombieController.Settings.WalkRadius && zombieController.Owner.FollowPlayer))
        {

            return true;

        }

        return false;
    }
}
