using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Zombie/Decision/Arrived")]
public class ArrivedZombieDecision : Decision
{
    public override bool MakeDecision(StateController controller)
    {
        return HaveIArrived(controller);
    }

    bool HaveIArrived(StateController controller)
    {
        ZombieStateController zombieController = controller as ZombieStateController;

        if (Vector3.Distance(zombieController.Owner.transform.position, zombieController.Owner.DesiredPosition) < zombieController.Settings.WalkRadius)
        {
            return true;
        }
        return false;
    }
}
