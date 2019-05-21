using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decision/EndFear")]
public class EndFear : Decision
{
    public override bool MakeDecision(StateController controller)
    {
        // return !controller.Feared;

        if (controller.Feared)
        { 
            return false;
        }
        return true;
    }
}
