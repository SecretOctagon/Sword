using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : StateMachineBehaviour
{
    protected MobMovement mobMovement;

    protected Vector3 direction = Vector3.zero;
    protected bool lateral = true;
    [SerializeField] float XMaxAngle;

    protected int a_jump = Animator.StringToHash("Jump");
    protected int a_X = Animator.StringToHash("DirectionX");
    protected int a_Z = Animator.StringToHash("DirectionZ");

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mobMovement = animator.GetComponentInParent<MobMovement>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //move character
        Vector3 initialForward = mobMovement.transform.forward;
        Vector3 intialPosition = mobMovement.transform.position;
        mobMovement.Move(direction, lateral, false, out float dot);

        //calculate anim direction
        switch (direction == Vector3.zero)
        {
            case true:
                animator.SetFloat(a_X, 0);
                animator.SetFloat(a_Z, 0);
                break;
            case false:
                animator.SetFloat(a_Z, dot);
                float x = Vector3.SignedAngle(initialForward, direction, Vector3.up) / XMaxAngle;
                animator.SetFloat(a_X, x);
                break;
        }

        //jump transition
        animator.SetBool(a_jump, !mobMovement.isGrounded);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
