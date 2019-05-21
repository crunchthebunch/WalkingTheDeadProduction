using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Action/MoveBack")]
public class MoveBackAction : Action
{
    public override void Act(StateController controller)
    {
        controller.MoveBackBehaviour.DoBehaviour();
    }
}
