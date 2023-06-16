using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : StateMachineBehaviour
{
    AttackSpawner spawner;
    int a_Attack = Animator.StringToHash("Attack");

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        spawner = animator.GetComponentInParent<AttackSpawner>();
        Debug.Log(name + "'s spawner is " + spawner);

        spawner.Spawn();

        animator.SetBool(a_Attack, false);
    }
}
