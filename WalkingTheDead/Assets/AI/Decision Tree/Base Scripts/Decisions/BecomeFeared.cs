using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decision/BecomeFeared")]
public class BecomeFeared : Decision
{
    public override bool MakeDecision(StateController controller)
    {
        if (controller.Feared)
        {
            return true;
        }
        return false;
    }
}
