using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Action/Patrol")]
public class PatrolAction : Action
{
    public override void Act(StateController controller)
    {
        controller.PatrolBehaviour.DoBehaviour();
    }
}
