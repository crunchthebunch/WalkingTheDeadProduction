using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Action/Wander")]
public class WanderAction : Action
{
    public override void Act(StateController controller)
    {
        controller.WanderBehaviour.DoBehaviour();
    }
}
