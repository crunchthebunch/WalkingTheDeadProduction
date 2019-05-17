using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Action/Flee")]
public class FleeAction : Action
{
    public override void Act(StateController controller)
    {
        controller.FleeBehaviour.DoBehaviour();
    }
}
