using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decision/HasCommand")]
public class HasCommandDecision : Decision
{
    public override bool MakeDecision(StateController controller)
    {
        if (controller.HasCommand) return true;
        else return false;
    }
}
