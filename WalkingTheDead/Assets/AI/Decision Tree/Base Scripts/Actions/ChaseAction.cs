using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Action/Chase")]
public class ChaseAction : Action
{
    public override void Act(StateController controller)
    {
        controller.ChaseBehaviour.DoBehaviour();
    }
}
