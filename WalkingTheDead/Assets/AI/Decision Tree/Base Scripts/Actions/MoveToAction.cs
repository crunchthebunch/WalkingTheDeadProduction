using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Action/MoveTo")]
public class MoveToAction : Action
{
    public override void Act(StateController controller)
    {
        controller.MoveToBehaviour.DoBehaviour();
    }
}
