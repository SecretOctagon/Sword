using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPreAttackState : StateMachineBehaviour
{
    EnemyMovement enemyMovement;
    int a_Attack = Animator.StringToHash("Attack");

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyMovement = animator.GetComponentInParent<EnemyMovement>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(a_Attack, enemyMovement.InAttackRange());
    }
}
