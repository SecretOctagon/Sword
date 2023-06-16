using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : WalkState
{
    PlayerMovement movement;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        movement = PlayerMovement.active;
        mobMovement = PlayerMovement.active;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        direction = movement.GetDirection();
        direction.Normalize();

        movement.StartJump();
        lateral = true;
        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }
}
