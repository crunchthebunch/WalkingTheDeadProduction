using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decision/Arrived")]
public class ArrivedDecision : Decision
{
    public override bool MakeDecision(StateController controller)
    {
        return HaveIArrived(controller);
    }

    bool HaveIArrived(StateController controller)
    {
        if (Vector3.Distance(controller.transform.position, controller.MoveToBehaviour.Destination) < controller.Settings.WalkingSpeed)
        {
            return true;
        }
        return false;
    }
}
